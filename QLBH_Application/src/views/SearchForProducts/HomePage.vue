
<style scoped>
    .saved-button {
        color: red !important; /* Màu chữ trắng */
    }
</style>
<script>
import { Swiper, SwiperSlide } from 'swiper/vue';
import { Navigation, Autoplay } from 'swiper/modules';




export default {
    data() {
        return{
            modules: [Navigation, Autoplay],
            data: [
                {
                    image: null,
                    productName: null,
                    productPrice: null
                },
            ],
        }
    },
    name: "LANDING",
    components: {
        Swiper,
        SwiperSlide,
        
    },
    computed:{
    },
    created(){

    },
    methods: {
    },
}
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
</template>