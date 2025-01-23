import apiClient from "../components/common/apiClient";

export default {
  install(app) {
    app.config.globalProperties.$apiClient = apiClient;
  },
};