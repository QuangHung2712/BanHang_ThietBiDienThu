import { reactive } from "vue";

const notificationState = reactive({
  visible: false,
  message: "",
  infoMessage:"",
  color: "info",
});

export default {
  show(message,infoMessage, color = "info", timeout = 10000) {
    notificationState.message = message;
    notificationState.color = color;
    notificationState.infoMessage = infoMessage;
    notificationState.visible = true;

    setTimeout(() => {
      notificationState.visible = false;
    }, timeout);
  },
  state: notificationState,
};