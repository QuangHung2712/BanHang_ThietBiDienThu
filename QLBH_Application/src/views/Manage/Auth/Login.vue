<style scoped>
    .auth-wrapper{
        background-image: url('../../../../public/images/background.jpg');
        background-size: cover;
        background-position: center;
        background-repeat: no-repeat;
    }
</style>
<script>
import Rightbar from "@/components/right-bar.vue"

export default {
    name: "LOGIN",
    components: {
        Rightbar
    },
    data(){
        return{
            login:{
                email: 'admin',
                password:'1',
            },
            remember: false,
            form: false,
            showPassword: false,
            errorMessage: '',
            loading: false,  // Trạng thái loading
        }
    },
    created() {
        const token = localStorage.getItem('token');
        if (token) {
            // Token tồn tại, chuyển hướng đến trang tower
            this.$router.push({ name: 'manage' });
        }
    },
    methods:{
        LogIn() {
            this.loading = true;
            
            this.$apiClient.post(`/Auth/Login`,this.login)
                .then(response=>{
                    this.$store.dispatch('login',response.data);
                    localStorage.setItem('token',response.data)
                    //localStorage.setItem('landlordId',response.data.userId)
                    //if(this.remember){}
                    this.errorMessage = ""
                    this.$router.push({ name: 'manage' });                    
                })
                .catch(error=>{
                    //this.$notify(error.response.data.Message,error.response.data.Errors.join('. '), "error");
                    this.errorMessage = error.response.data.Errors.join('. ');
                })
                .finally(() => {
                    this.loading = false;  // Kết thúc trạng thái loading
                });
        },
    }
}
</script>

<template>
    <div class="auth-main v2">
        <div class="bg-overlay bg-dark"></div>
        <div class="auth-wrapper">
            <div class="auth-sidecontent">

                <div class="auth-sidefooter">
                    <h4 class="text-white">QUẢN LÝ NHÀ TRỌ</h4>
                    <!-- <img src="" class="img-brand img-fluid" alt="images"> -->
                    <hr class="mb-3 mt-4">
                    <!-- <BRow class="row">
                        <BCol class="col-auto my-1">
                            <ul class="list-inline footer-link mb-0">
                                <li class="list-inline-item"><router-link to="/dashboard">Home</router-link></li>
                                <li class="list-inline-item"><a href="#"
                                        target="_blank">Documentation</a></li>
                                <li class="list-inline-item"><a href="#"
                                        target="_blank">Support</a></li>
                            </ul>
                        </BCol>
                    </BRow> -->
                </div>
            </div>
            <div class="auth-form">
                <div class="card my-5 mx-3">
                    <div class="card-body">
                        <h4 class="f-w-500 mb-1">Đăng nhập</h4>
                        <p class="mb-3">Bạn chưa có tài khoản? <router-link to="/register"
                                class="link-primary ms-1">Tạo tài khoản</router-link></p>
                        <v-form v-model="form" ref="form" @keyup.enter="LogIn()">
                            <div class="form-group mb-3">
                                <v-text-field 
                                    type="email" 
                                    label="Email"  
                                    variant="outlined" 
                                    placeholder="Email" 
                                    :rules="[rules.required]" 
                                    v-model="login.email"
                                    prepend-inner-icon="mdi-email-outline"
                                ></v-text-field>
                            </div>
                                <v-text-field 
                                    :type="showPassword ? 'text' : 'password'" 
                                    label="Mật khẩu"  
                                    variant="outlined"
                                    placeholder="Mật khẩu" 
                                    :rules="[required]" 
                                    v-model="login.password"
                                    prepend-inner-icon="mdi-lock-outline"
                                    :append-inner-icon="showPassword ? 'mdi-eye' : 'mdi-eye-off'"
                                    @click:append-inner="showPassword = !showPassword"
                                    ></v-text-field>
                            <div class="d-flex mt-1 justify-content-between align-items-center">
                                <div class="form-check">
                                    <input class="form-check-input input-primary" type="checkbox" id="customCheckc1" v-model="remember">
                                    <label class="form-check-label text-muted" for="customCheckc1">Nhớ mật khẩu</label>
                                </div>
                                <router-link to="/forgot">
                                    <h6 class="text-secondary f-w-400 mb-0">Quên mật khẩu?</h6>
                                </router-link>
                            </div>
                        </v-form>
                        <div class="d-grid mt-4">
                            <button type="button" @click="LogIn()" class="btn btn-primary" :disabled="!form" :loading="loading">Đăng nhập</button>

                        <!-- Biểu tượng loading khi đang xử lý -->
                        <v-progress-circular v-if="loading" indeterminate color="primary"></v-progress-circular>

                        <!-- Thông báo lỗi khi có lỗi xảy ra -->
                        <v-alert v-if="errorMessage" type="error" dismissible>
                        {{ errorMessage }}
                        </v-alert>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <Rightbar />
</template>