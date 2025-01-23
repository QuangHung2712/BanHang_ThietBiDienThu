import common from "../components/common/JavaScripCommon";


export default {
    install(app) {
      app.config.globalProperties.$common = common;
    }
  };