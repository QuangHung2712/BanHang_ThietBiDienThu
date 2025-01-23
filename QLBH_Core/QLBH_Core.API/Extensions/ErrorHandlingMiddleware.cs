using Microsoft.AspNetCore.Http;
using QLBH_Core.Commons.Output;
using System.Text.Json;


namespace QLBH_Core.API.Extensions
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException sqlEx)
            {

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 400;
                var errorResponse = new ErrorResponse
                {
                    Success = false,
                    Message = "Thất bại trong quá trình cập nhật vào cơ sở dữ liệu",
                    Errors = new List<string>
                    {
                    },
                };
                List<string> error = HandleSQLExeption(sqlEx);

                errorResponse.Errors.AddRange(error);

                await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 400;

                var errorResponse = new ErrorResponse
                {
                    Success = false,
                    Message = "Thất bại trong quá trình xử lý",
                    Errors = new List<string>
                    {
                            ex.Message
                    },
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
            }
        }

        private List<string> HandleSQLExeption(Microsoft.EntityFrameworkCore.DbUpdateException sqlEx)
        {
            var error = new List<string>();

            // Lặp qua tất cả các InnerException trong chuỗi
            Exception? currentEx = sqlEx.InnerException;
            while (currentEx != null)
            {
                error.Add(currentEx.Message.ToString());
                // Kiểm tra lỗi "String or binary data would be truncated"
                if (currentEx.Message.Contains("String or binary data would be truncated"))
                {
                    error.Add("Có một trường có độ dài vượt quá kích thước quy định");
                }

                // Kiểm tra lỗi liên quan đến khóa chính (Primary key constraint violation)
                if (currentEx.Message.Contains("Violation of PRIMARY KEY constraint"))
                {
                    error.Add("Lỗi vi phạm khóa chính. Dữ liệu đã tồn tại.");
                }

                // Kiểm tra lỗi liên quan đến khóa ngoại (Foreign key constraint violation)
                if (currentEx.Message.Contains("FOREIGN KEY constraint"))
                {
                    error.Add("Lỗi vi phạm ràng buộc khóa ngoại. Dữ liệu không hợp lệ.");
                }

                // Kiểm tra lỗi dữ liệu NULL không hợp lệ
                if (currentEx.Message.Contains("Cannot insert the value NULL"))
                {
                    error.Add("Không thể chèn giá trị NULL vào trường này.");
                }

                // Kiểm tra lỗi loại dữ liệu không phù hợp
                if (currentEx.Message.Contains("conversion failed"))
                {
                    error.Add("Dữ liệu không phù hợp với loại dữ liệu yêu cầu.");
                }

                // Kiểm tra lỗi timeout
                if (currentEx.Message.Contains("Timeout"))
                {
                    error.Add("Quá thời gian thực thi lệnh.");
                }

                // Chuyển sang InnerException tiếp theo
                currentEx = currentEx.InnerException;
            }

            // Nếu không có lỗi trong InnerException, có thể trả về lỗi chung
            if (error.Count == 0)
            {
                error.Add("Lỗi không xác định khi thực thi câu lệnh SQL.");

            }

            return error;
        }
    }
}
