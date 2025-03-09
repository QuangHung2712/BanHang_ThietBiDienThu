<style scoped>
    .color-text{
        color: azure;
    }
    .nav-item *{
        margin-bottom: 0px;
        color: azure;
    }
    .nav-item {
        margin-bottom: 0px;
        color: azure;
        padding-top: 5px;
        padding-bottom: 5px;
        padding-left: 10px;
        padding-right: 10px;
        border-right: 1px solid white;
    }
    .navbar .nav-link a :hover{
        color: azure !important;
    }
    .nav-item:hover {
        background-color: #81d4fa;
    }
    .footer{
        background-color: #81d4fa;
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
        width: 80%;
    }
    .col-xl-6 h5{
        color: white !important;
        margin-top: 25px;
    }        
    .search-box {
        width: 100%;
        display: flex;
        justify-content: center;
        align-items: center;
    }

    .search-box input {
        padding: 5px;
        width: 70%;
        border-top-left-radius: 5px;
        border-bottom-left-radius: 5px;
        border: 3px solid #4fc3f7;
    }

    .search-box button {
        background-color: #4fc3f7;
        padding-left: 14px;
        padding-right: 14px;
        padding-bottom: 1px;
        font-size: 24px;
        cursor: pointer;
        border-top-right-radius: 5px;
        border-bottom-right-radius: 5px;
    }
    #home{
        height: 130px;
    }
    @media (max-width: 768px) {
        .logo {
            width: 80%; /* Kích thước hình ảnh giảm xuống 50% khi màn hình nhỏ */
        }
        #home{
            min-height: 130px;
        }
        .m15{
            margin-bottom: 7px;
        }
        .navbar-toggler{
            color: #4fc3f7 !important;
            border: 0px;
        }
        .search-box {
            background-color: #4fc3f7 ;
        }
        .search-box input {
            background-color: white;
        }
    }
    .call-button {
  display: flex;
  align-items: center;
  width: 200px;
}

.call-link {
  font-size: 15px;
  font-weight: bold;
  color: white;
  background-color: #81d4fa;
  padding: 6px;
  border-top-right-radius: 30px;
  border-bottom-right-radius: 30px;
  text-decoration: none;
  display: inline-block;
}

.call-icon {
  width: 45px;
  height: 45px;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
  overflow: hidden;
  margin-right: -10px;
  z-index: 1;
  animation: zoomEffect 1.5s infinite ease-in-out;
  color: white;
}

.call-icon img {
  width: 100%;
  height: auto;
}

@keyframes zoomEffect {
  0% {
    transform: scale(1);
  }
  50% {
    transform: scale(1.2);
  }
  100% {
    transform: scale(1);
  }
}
</style>
<style>
    .product-card {
        border: 1px solid #ddd;
        padding: 15px;
        text-align: center;
        border-radius: 10px;
        transition: transform 0.3s ease;
    }
    .product-card:hover {
        transform: scale(1.05);
    }
    .product-card img {
        width:  100%;
        height: 250px;
        border-radius: 10px;
    }
    .product-name{
        display: -webkit-box;
        -webkit-line-clamp: 2; /* Giới hạn tối đa 2 dòng */
        -webkit-box-orient: vertical;
        overflow: hidden;
        height: 55px; /* Giới hạn chiều cao */
        text-overflow: ellipsis;
        word-break: break-word;
        font-size: 18px;
    }
    @media (max-width: 768px) {
        .product-name{
            height: 40px; /* Giới hạn chiều cao */
            font-size: 90% !important;
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
        <BNav style="background-color: white; " class="navbar navbar-expand-md navbar-light default p-0">
            <div>
                <BRow >
                    <div class="container ">
                        <div class="row mt-2 align-items-center">
                            <BCol class="col-xl-3 col-6 col-md-2 m15">
                                <a class="pc-navbar-brand" href="/" >
                                    <img src="/images/z6387100830882_530977ddd7e3988629efa878cac295bc.jpg" alt="" class="logo" >
                                </a>
                            </BCol>
                            
                            <button @click="toggleMenu" v-show="menu" class="navbar-toggler col-6 m15" type="button" data-bs-toggle="collapse" data-bs-target="#navbarTogglerDemo01" aria-controls="navbarTogglerDemo01" aria-expanded="false" aria-label="Toggle navigation">
                                <v-icon size="x-large">mdi-menu</v-icon>                      
                            </button>
                            <button @click="closeMenu" v-show="!menu" class="navbar-toggler col-6 m15" type="button" >
                                <v-icon size="x-large">mdi-close</v-icon>
                            </button>
                            <BCol class="col-xl-6 col-12 col-md-4">
                                <div class="search-box">
                                    <input v-model="searchProductName" type="text" placeholder="Tìm kiếm sản phẩm, danh mục..." @keyup.enter="FindProductName()" />
                                    <button @click="FindProductName()" >
                                        <i class="mdi mdi-magnify" style="color: white"></i>
                                    </button>
                                </div>
                            </BCol>
                            <BCol class="col-xl-3 col-12 col-md-4 d-none d-md-flex justify-content-end">
                                <div class="call-button">
                                    <div class="call-icon">
                                        <img src="https://tiemtraannhien.vn/wp-content/uploads/2023/04/phone.png" alt="Gọi ngay" />
                                    </div>
                                    <a href="tel:0386125907" class="call-link">: 0386 125 907</a>
                                </div>
                            </BCol>
                        </div>
                    </div>

                    <div class="col-12 col-md-12 collapse navbar-collapse" id="navbarTogglerDemo01" style="background-color: #4fc3f7;">
                        <ul class="navbar-nav container d-flex justify-content-center">
                            <li class="nav-item" style="border-left: 1px white solid;">
                                <router-link class="nav-link" :to="'/'">TRANG CHỦ</router-link>
                            </li>
                            <li class="nav-item">
                                <router-link :to="'/introduce'" class="nav-link" >GIỚI THIỆU</router-link>
                            </li>
                            <li class="nav-item">
                                <router-link class="nav-link" :to="'/lstproduct'">SẢN PHẨM</router-link>
                            </li>
                            <li class="nav-item">
                                <router-link :to="'/chinh-sach-bao-hanh'" class="nav-link"> CHÍNH SÁCH BẢO HÀNH</router-link>
                            </li>
                            <li class="nav-item">
                                <router-link :to="'/chinh-sach-doi-tra'" class="nav-link"> CHÍNH SÁCH ĐỔI TRẢ</router-link>
                            </li>
                            <li class="nav-item">
                                <router-link :to="'/chinh-sach-giao-hang'" class="nav-link"> CHÍNH SÁCH GIAO HÀNG</router-link>
                            </li>
                            <li class="nav-item">
                                <router-link :to="'/contact'" class="nav-link"> LIÊN HỆ</router-link>
                            </li>
                        </ul>
                    </div>
                </BRow>
            </div>
        </BNav>
    </header>
    <v-container style="min-height: 880px;" class="p-0">
        <router-view :key="$route.fullPath">
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
            <BRow>
                <div class="col-xl-6">
                    <h3 style="color: white;" class="mt-4">CÔNG TY TNHH TM&NK AN CHI</h3>
                    <h5><v-icon>mdi-map-marker</v-icon> Địa chỉ: Cụm Công Nghiệp Đại Tự , Xã Kim Chung , Huyện Hoài Đức, Hà Nội</h5>
                    <h5><v-icon>mdi-email-outline</v-icon> Email: Coolmaxvietnam@gmail.com</h5>
                    <h5><v-icon>mdi-phone-outline</v-icon> CSKH: 0386.125.907</h5>
                    <h3 style="color: white;" class="mt-3">Hotline: 039.335.6261</h3>
                </div>
                <div class="col-xl-6">
                    <iframe
                        src="https://www.google.com/maps/embed?pb=!1m17!1m12!1m3!1d2642.460570765008!2d105.71467032267958!3d21.045184092730388!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m2!1m1!2zMjHCsDAyJzQyLjQiTiAxMDXCsDQyJzU1LjkiRQ!5e1!3m2!1svi!2s!4v1740997209124!5m2!1svi!2s"
                        width="80%"
                        height="300"
                        style="border: 0"
                        allowfullscreen=""
                        loading="lazy"
                        referrerpolicy="no-referrer-when-downgrade"
                    ></iframe>
                </div>
            </BRow>
        </v-container>
    </div>
</template>