using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QLBH_Core.Commons.CustomException;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;

using System.Text;
using System.Threading.Tasks;
using static QLBH_Core.Commons.Constants;
using static QLBH_Core.Commons.Enums;

namespace QLBH_Core.Commons
{
    public static class Functions
    {
        #region Hàm liên quan đến dữ liệu
        public static T GetAvailableById<T>(this DbSet<T> entity, long id) where T : BaseEntityIsDelete
        {
            var record = entity.Where(c => c.Id == id && !c.IsDelete).FirstOrDefault();
            if (record == null)
            {
                throw new NotFoundException(typeof(T).Name);
            }
            return record;
        }
        public static void IsGetAvailableById<T>(this DbSet<T> entity, long id) where T : BaseEntityIsDelete
        {
            var record = entity.Where(c => c.Id == id && !c.IsDelete).FirstOrDefault();
            if (record == null)
            {
                throw new NotFoundException(typeof(T).Name);
            }
        }
        public static T GetById<T>(this DbSet<T> entity, long id) where T : BaseEntity
        {
            var record = entity.Where(c => c.Id == id).FirstOrDefault();
            if (record == null)
            {
                throw new NotFoundException(typeof(T).Name);
            }
            return record;
        }
        public static void IsGetById<T>(this DbSet<T> entity, long id) where T : BaseEntity
        {
            var record = entity.Where(c => c.Id == id).FirstOrDefault();
            if (record == null)
            {
                throw new NotFoundException(typeof(T).Name);
            }
        }
        public static void Delete<T>(this DbSet<T> entity, long id, bool isPermanent = false) where T : BaseEntityIsDelete
        {
            var record = entity.GetAvailableById(id);
            if (isPermanent)
            {
                entity.Remove(record);
            }
            else
            {
                record.IsDelete = true;
                entity.Update(record);
            }
        }
        public static void DeletePermanent<T>(this DbSet<T> entity, long id) where T : BaseEntity
        {
            var record = entity.GetById(id);
            entity.Remove(record);
        }
        public static void IsAlreadyExists<T>(this DbSet<T> entity, string propertyName, object value,string exceptionName = "") where T : BaseEntity
        {
            if (entity.Any($"{propertyName} == @0", value))
            {
                throw new AlreadyExistsException(exceptionName ?? propertyName);
            }
        }
        #endregion
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = field.GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description ?? value.ToString();
        }
        public static List<string> SaveImgToDB(List<IFormFile> Imgs, string directoryPath)
        {
            try
            {
                //Xoá tất cả các file cũ trong đường dẫn
                if (Directory.Exists(directoryPath))
                {
                    Directory.Delete(directoryPath, true);
                }

                // Đường dẫn thư mục lưu ảnh
                if (!Directory.Exists(directoryPath))
                {
                    //Nếu không có thì tạo mới thư mục
                    Directory.CreateDirectory(directoryPath);
                }

                List<string> result = new List<string>();
                foreach (var img in Imgs)
                {
                    if (img.Length > 0)
                    {
                        // Đặt tên file duy nhất
                        var fileName = Guid.NewGuid() + Path.GetExtension(img.FileName);

                        // Đường dẫn đầy đủ của ảnh
                        var filePath = Path.Combine(directoryPath, fileName);

                        // Lưu file vào đường dẫn đã chỉ định
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            img.CopyTo(stream);
                        }
                        result.Add(filePath);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lưu file: {ex.Message}");
            }
        }
        public static string SaveImgToDB(IFormFile input,string nameFile,string directoryPath, string PathImgQROld = "")
        {
            if (!string.IsNullOrEmpty(PathImgQROld) && PathImgQROld != DefaultValue.DEFAULT_IMG_AVATAR)
            {
                File.Delete(PathImgQROld);
            }
            // Tạo tên ảnh
            string fileName = nameFile + Path.GetExtension(input.FileName);

            // Kiểm tra và tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Đường dẫn đầy đủ của ảnh
            string filePath = Path.Combine(directoryPath, fileName);

            // Lưu file vào đường dẫn đã chỉ định
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                input.CopyTo(stream);
            }

            // Trả về đường dẫn ảnh đã lưu
            return filePath;
        }
        public static string ConverPathIMG(string input)
        {
            if (input != null)
            {
                string path = Path.GetRelativePath(DefaultValue.DEFAULT_BASE_Directory_IMG, input);
                var result = path.Replace("\\", "/");
                return "/" + result;
            }
            return "";
        }
        public static List<string> ConverPathIMG(List<string> input)
        {
            List<string> result = new List<string>();
            foreach (var item in input)
            {
                result.Add(ConverPathIMG(item));
            }
            return result;
        }
        public static void CoppyFile(string OriginalPath, string NewPath)
        {
            try
            {
                // Copy ảnh từ sourcePath sang destinationPath
                File.Copy(OriginalPath, NewPath, overwrite: true); // `overwrite: true` cho phép ghi đè nếu file tồn tại
            }
            catch (IOException ex)
            {
                throw new Exception($"Lỗi khi coppy file: {ex.Message}");
            }
        }
    }
}
