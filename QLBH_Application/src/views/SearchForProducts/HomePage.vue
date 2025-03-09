<style scoped>
.saved-button {
    color: red !important; /* Màu chữ trắng */
}
/* .product-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
  gap: 20px;
  margin-top: 20px;
} */
.view-more {
    margin-top: 20px;
    margin-bottom: 20px;
    text-align: center;
}
.view-more button:hover {
  background-color: #0056b3;
}
.v-slide-group {
  max-width: 100%;
}

.item {
  width: 240px; /* Hiển thị 4 items cùng lúc, trừ khoảng cách */
}
@media (max-width: 768px) {
    .item {
        width: 200px; /* Hiển thị 4 items cùng lúc, trừ khoảng cách */
    }
    .item img {
        height: 170px; /* Khi màn hình nhỏ hơn 768px */
        width: 170px;
    }
    .content{
        padding: 0px;
    }
    .v-slide-group__next, 
    .v-slide-group__prev {
        width: 21px !important;
        min-width: 21px !important; /* Đảm bảo kích thước không bị override */
    }
}
</style>
<script>
import { Swiper, SwiperSlide } from "swiper/vue";
import { Navigation, Autoplay } from "swiper/modules";

export default {
    data() {
    return {
        modules: [Navigation, Autoplay],
        data: [
            {
                products: [
                    {pathImg: '',name: '',price: '', id: 0}
                ]
            }
        ],
        model: null
    };
    },
    name: "LANDING",
    components: {
        Swiper,
        SwiperSlide,
    },
    created(){
        this.GetProduct();
    },
    methods:{
        GetProduct(){
            this.$apiClient.get(`/Product/GetAllProductByType`)
                    .then(response=>{
                        this.data = response.data;
                        console.log(response.data)
                    })
        },
        GotoDetail(id){
            const route = this.$router.resolve({ name: 'detail', params: { productId: id } });
            window.open(route.href, '_blank');

        }
    }
};
</script>
<template>
    <swiper :navigation="true" :autoplay="true" :modules="modules" :loop="true" class="mySwiper">
        <swiper-slide> <img src="images/test/z3277142962677_4bdf325a013a1ace985c6d15a2cfe66a.jpg" alt=""
                class="img-fluid"></swiper-slide>
        <swiper-slide><img src="images/test/z3277143158749_978ce87bba64930b4e0bb27d3c615348.jpg" alt=""
                class="img-fluid"></swiper-slide>
        <swiper-slide> <img src="images/test/z3277154203256_ef0789bbc28c686e512069187cbdbbcf.jpg" alt=""
                class="img-fluid"></swiper-slide>
    </swiper>
    <div class="contact my-3">
        <h1>Chào mừng bạn đến với <b>An Chi</b></h1>
        <p>
            Chuyên cung cấp các thiết bị lạnh nhập khẩu như: Tủ mát, tủ đông, tủ
            trưng bày bánh kem...
        </p>
    </div>
    <div class="container content">

        <div  v-for="(item, index) in data" :key="index" >
            <h3>Giới thiệu về loại sản phẩm - {{ item.productName }}</h3>
            <hr>
            <v-slide-group v-model="model" show-arrows class="pb-4">
                <v-slide-group-item v-for="(itemProduct, indexProduct) in item.products" :key="indexProduct" >
                    <a @click="GotoDetail(itemProduct.id)" target="_blank" class="product-card item m-2 my-3">
                        <div><img :src="itemProduct.pathImg" alt="product" /></div>
                        <div class="product-name">{{ itemProduct.name }}</div>
                        <h4 style="color: red;" class="text m-0">{{ itemProduct.price.toLocaleString("vi-vn") }} VNĐ</h4>
                    </a>
                </v-slide-group-item>
            </v-slide-group>

        </div>
        <div class="view-more">
            <BButton type="button" variant="primary" to="lstproduct">Xem thêm</BButton>
        </div>
    </div>
</template>
