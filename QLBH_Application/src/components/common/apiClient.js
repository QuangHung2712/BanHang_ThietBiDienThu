import axios from "axios";

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
    return Promise.reject(error);
  }
);

apiClient.interceptors.response.use(
  (response) => response,
  (error) => {
    // Xử lý lỗi chung
    if (error.response?.status === 401) {
      console.error("Unauthorized! Please log in.");
    }
    return Promise.reject(error);
  }
);

export default apiClient;
