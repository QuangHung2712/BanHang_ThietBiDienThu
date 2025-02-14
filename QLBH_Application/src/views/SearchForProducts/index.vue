<style scoped>
    .color-text{
        color: azure;
    }
    .nav-item *{
        color: azure;
    }
    .custom-text-field  {
        color: white !important;
    }
    .footer{
        background-color: #326e51;
        color: white;
    }
    .floating-icons{
        position: fixed;
        right: 30px;
        bottom: 100px;
        display: flex;
        flex-direction: column;
        gap: 10px;
        z-index: 9999;
    }
    .icon {
        width: 50px;
        height: 50px;
        border-radius: 50%;
        overflow: hidden;
        display: flex;
        align-items: center;
        justify-content: center;
        background: white;
        box-shadow: 0px 4px 6px rgba(0, 0, 0, 0.1);
        transition: transform 0.2s ease-in-out;
        }

    .icon img {
        width: 100%;
        height: auto;
    }

    .icon:hover {
        transform: scale(1.1);
    }
    .logo{
        width: 70%;
    }
    @media (max-width: 768px) {
        .logo {
            width: 80%; /* Kích thước hình ảnh giảm xuống 50% khi màn hình nhỏ */
        }
    }
</style>

<script>
import Rightbar from "@/components/right-bar.vue"
import { Autoplay, A11y } from 'swiper/modules';

// import { ref } from 'vue';
import AOS from 'aos';
import 'aos/dist/aos.css';

// Import Swiper styles
import 'swiper/css';
import "swiper/css/autoplay";
import 'swiper/css/navigation';

export default {
    data() {
        return{
            isOpen: false,
            menu: true,
            searchProductName: null,
        }
    },
    name: "LANDING",
    components: {
        Rightbar,
    },
    created(){
        this.searchProductName = this.$route.query.productName ?? "";
    },
    watch: {
        // Theo dõi sự thay đổi của query parameter productName
        '$route.query.productName': function(newValue) {
            this.searchProductName = newValue?? null
        },
    },
    methods: {
        changeMode(mode) {
            this.currentMode = mode;
            if (mode == "dark") {
                document.body.setAttribute("data-pc-theme", "dark");
                document.body.setAttribute("data-topbar", "dark");
                document.body.classList.remove("mode-auto");
            } else if (mode == "auto") {
                document.body.setAttribute("data-pc-theme", "light");
                document.body.setAttribute("data-topbar", "light");
                document.body.classList.add("mode-auto");
            } else {
                document.body.setAttribute("data-pc-theme", "light");
                document.body.setAttribute("data-topbar", "light");
                document.body.classList.remove("mode-auto");
            }
        },
        toggleMenu() {
            const navbar = document.getElementById("navbarTogglerDemo01");
            navbar.classList.toggle("show");

            // Kiểm tra và thêm hoặc xóa sự kiện click bên ngoài
            if (navbar.classList.contains("show")) {
                this.menu = false;
                document.addEventListener("click", this.handleOutsideClick);
            } else {
                document.removeEventListener("click", this.handleOutsideClick);
            }
        },
        handleOutsideClick(event) {
            const navbar = document.getElementById("navbarTogglerDemo01");
            const toggler = document.querySelector(".navbar-toggler");

            // Kiểm tra nếu click nằm ngoài menu và nút toggle
            if ((!navbar.contains(event.target) && !toggler.contains(event.target)) || navbar.contains(event.target) && toggler.contains(event.target)) {
                navbar.classList.remove("show");
                this.menu = true;
                document.removeEventListener("click", this.handleOutsideClick);
            }
        },
        closeMenu() {
            this.menu = true;
            const navbar = document.getElementById("navbarTogglerDemo01");
            navbar.classList.remove("show");
        },
        FindProductName(){
            if(this.searchProductName == ""){
                this.$notify("Vui lòng nhập tên sản phẩm","","error");
                return;
            }
            this.$router.push({ 
                name: 'lstproduct', 
                query: { 
                    productName: this.searchProductName // Thêm query parameters vào URL
                } 
                });
        }
    },
    setup() {
        return {
            modules: [Autoplay, A11y],
        };
    },
    mounted() {
        AOS.init({
            easing: 'ease-in-out-sine',
            duration: 2000
        }); // Initialize AOS
            // document.body.setAttribute("data-pc-direction", "rtl");
            document.body.classList.add("landing-page");
    }
}
</script>

<template>
    <header id="home">
        <BNav style="background-color: #326e51; " class="navbar navbar-expand-md navbar-light default p-2">
            <div class="container">
                <BRow>
                    <BCol class="col-xl-3 col-6 col-md-4">
                        <a class="pc-navbar-brand" href="/" >
                            <img src="/images/logo.png" alt="" class="logo" >
                        </a>
                    </BCol>
                    
                    <button @click="toggleMenu" v-show="menu" class="navbar-toggler col-6" type="button" data-bs-toggle="collapse" data-bs-target="#navbarTogglerDemo01" aria-controls="navbarTogglerDemo01" aria-expanded="false" aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>
                    <button @click="closeMenu" v-show="!menu" class="navbar-toggler col-6" type="button" >
                        <v-icon>mdi-close</v-icon>
                    </button>
                    <div class="col-6 collapse navbar-collapse" id="navbarTogglerDemo01">
                        <ul class="navbar-nav ">
                            <li class="nav-item px-1">
                                <h4><router-link class="nav-link" to="/">TRANG CHỦ</router-link></h4>
                            </li>
                            <li class="nav-item px-1">
                                <h4><router-link class="nav-link" to="/lstproduct">SẢN PHẨM</router-link></h4>
                            </li>
                            <li class="nav-item px-1">
                                <h4><router-link to="/introduce" class="nav-link" >GIỚI THIỆU</router-link></h4>
                            </li>
                            <li class="nav-item px-1">
                                <h4><router-link to="/contact" class="nav-link"> LIÊN HỆ</router-link></h4>
                            </li>
                        </ul>
                    </div>
                    <BCol class="col-xl-3 col-12">
                        <v-text-field label="Tìm kiếm sản phẩm" v-model="searchProductName" hide-details append-icon="mdi-magnify"  @click:append="FindProductName()" @keyup.enter="FindProductName()"  class="custom-text-field"></v-text-field>
                    </BCol>
                </BRow>
            </div>

        </BNav>
    </header>
    <v-container style="min-height: 880px;">
        <router-view>
        </router-view>
    </v-container>
    <!-- [ layout-card ] end -->

    <Rightbar />
    <div class="floating-icons">
        <a href="tel:0359988934" target="_blank" class="icon messenger">
            <img src="https://tiemtraannhien.vn/wp-content/uploads/2023/04/phone.png" alt="Điện thoại">
        </a>
        <a href="https://m.me/thietbinhahanganchi" target="_blank" class="icon messenger">
            <img src="https://tiemtraannhien.vn/wp-content/uploads/2023/04/messenger-icon-free-png.webp" alt="Facebook">
        </a>
            <!-- Zalo -->
        <a href="https://zalo.me/0386125907" target="_blank" class="icon zalo">
            <img src="https://upload.wikimedia.org/wikipedia/commons/9/91/Icon_of_Zalo.svg" alt="Zalo">
        </a>
    </div>
    <div class="footer">
        <v-container >
            <h5 style="color: white;">CỬA HÀNG THIẾT BỊ NHÀ HÀNG AN CHI</h5>
            <p><v-icon>mdi-map-marker</v-icon> Địa chỉ: Lai xá Kim chung Hoài đức, Hanoi, Vietnam</p>
            <p><v-icon>mdi-email-outline</v-icon> Email: thietbinhahang.anchi@gmail.com</p>
            <p><v-icon>mdi-phone-outline</v-icon> CSKH: 038 612 5907</p>
        </v-container>
    </div>
</template>