import notification from "../components/common/Notification";

export default {
  install(app) {
    app.config.globalProperties.$notify = notification.show;
  },
};