import axios from "axios";
import Swal from "sweetalert2";
import Store from "../../state/store";
import notification from "@/components/common/Notification"; 


// Tạo một instance của axios
const apiClient = axios.create({
  baseURL: "https://localhost:7254/api", // URL cơ bản của API
  timeout: 10000, // Thời gian chờ (ms)
  headers: {
    "Content-Type": "application/json",
  },
});

// Thêm interceptor nếu cần
apiClient.interceptors.request.use(
  (config) => {
    // Thêm token hoặc tùy chỉnh trước khi gửi request
    const token = localStorage.getItem("token");
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    console.log("Câu lệnh vẫn được thực hiện")
    return Promise.reject(error);
  }
);

apiClient.interceptors.response.use(
  (response) => response,
  (error) => {
    // Xử lý lỗi chung
    if (error.response?.status === 401) {
      const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: "btn btn-success",
            cancelButton: "btn btn-danger ml-2",
        },
        buttonsStyling: false,
      });

      swalWithBootstrapButtons
          .fire({
              title: "Bạn không có quyền!",
              text: `Bạn đang không có quyền thực hiện chức năng này hoặc phiên đăng nhập đã hết hạn. Bạn có muốn đăng nhập lại không`,
              icon: "warning",
              confirmButtonText: "Có!",
              cancelButtonText: "Không!",
              showCancelButton: true,
          })
          .then((confirm) => {
              if (confirm.value) {
                Store.dispatch("logout");
                window.location.href = "/login";
              } else if ( /* Read more about handling dismissals below */ confirm.dismiss === Swal.DismissReason.cancel) return
          });
        return new Promise(() => {}); 
    }
    if(error.response?.status === 400){
      notification.show(
        "Lỗi yêu cầu",
        "Dữ liệu gửi không hợp lệ. Vui lòng kiểm tra lại!",
        "error"
      );
      return new Promise(() => {}); 
    }
    if(error.response?.status === 500){
      notification.show(
        error.response.data.Message??"Đã xảy ra lỗi",error.response.data.Errors.join('. ')?? " ",
        "error"
      );
      return new Promise(() => {}); 
    }
    return Promise.reject(error);
  }
);

export default apiClient;
