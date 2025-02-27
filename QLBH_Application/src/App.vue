<script>
import notification from "@/components/common/Notification";

export default {
  name: 'App',
  components: {
  },
  computed: {
    notification() {
      return notification.state;
    },
  },
  created(){
    // this.$apiClient.get("/Server/ping")
    //   .catch(()=>{
    //     this.$router.push({ name: 'error'});
    //   })
  }
  
};
</script>


<template>
   <v-snackbar
      v-model="notification.visible"
      :color="notification.color"
      :timeout="10000"
      class="custom-snackbar"
    >
    <h5 style="color: white;">{{ notification.message }}</h5>
    {{ notification.infoMessage }}
    </v-snackbar>
  <router-view></router-view>
</template>
<style>
  .custom-snackbar {
    top: 15% !important; /* Vị trí từ trên xuống */
    right: 10% !important; /* Cách lề phải 10% */
    /* bottom: auto !important;  Đảm bảo không bị căn dưới  */
    left: auto !important; /* Loại bỏ căn lề trái nếu cần */
  }
  .v-snackbar__content{
    text-align: center;
  }
  p{
    font-size: 16px !important;
  }
</style>
