<script>
    import pageheader from "@/components/page-header.vue"
    export default {
        name: "PRODUCT-DETAIL",
        components: {
            pageheader
        },
        data(){
            return{
                currentSlide: 0,
                product:{
                    name: null,
                    pathImg:[],
                    price: 0,
                    infoProduct:[

                    ]
                },
                SimilarProducts:[

                ],
                Advise:{
                    customerName: null,
                    sDTCustomer: null,
                    sDTZaloCustomer: null,
                    fbCustomer: null,
                    productId: null,
                },
                index: 0,
                visible: false,
                model: null,
                ViewAdvise: false,
                form: false
            }
        },
        created(){
            const productId = this.$route.params.productId;
            this.GetDetail(productId);
        },
        methods:{
            GetDetail(Id){
                this.$apiClient.get(`/Product/GetDetail/${Id}`)
                        .then(response=>{
                            this.product = response.data;
                            this.$apiClient.get(`/Product/GetProductByType?productType=${this.product.productTypeId}&id=${Id}`)
                                .then(response=>{
                                    this.SimilarProducts = response.data;
                                    console.log(this.SimilarProducts)
                                })
                        })
            },
            showImg(index) {
                this.index = index;
                this.visible = true;
            },
            handleHide() {
                this.visible = false;
            },
            GotoDetail(id){
                this.$router.replace({ name: this.$route.name, params: { productId: id } });
                this.GetDetail(id);
                window.scrollTo({ top: 0, behavior: "smooth" }); // Cuộn lên đầu trang

            },
            btnAdvise(){
                this.ViewAdvise = !this.ViewAdvise;
                this.Advise = {
                    customerName: null,
                    sDTCustomer: null,
                    sDTZaloCustomer: null,
                    fbCustomer: null,
                    productId: this.product.id,
                }
            },
            SaveAdvise(){
                console.log(this.Advise);
                this.$apiClient.post(`/Order/Create`,this.Advise)
                        .then(()=>{
                            this.$notify("Thao tác thành công","Bạn vui lòng để ý tin nhắn Zalo chúng tôi sẽ liên hệ với bạn qua Zalo","success");
                            this.ViewAdvise = false;
                        })
            }
        }
    }
</script>
<template>
    <pageheader title="Chi tiết sản phẩm" pageTitle="Sản phẩm" />
    <BRow class="mb-10">
        <BCol class="col-xl-8 col-12">
            <v-row justify="center" class="m-0" >
                <!-- Carousel chính -->
                <v-carousel v-model="currentSlide" hide-delimiters show-arrows="hover" height="480" >
                    <v-carousel-item v-for="(item, index) in product.pathImg" :key="index">
                        <v-img :src="item" @click="showImg(index)" />
                    </v-carousel-item>
                </v-carousel>

                <!-- Thumbnails -->
                <v-row justify="center" class="m-0">
                    <div
                    v-for="(item, index) in product.pathImg"
                    :key="index"
                    @click="currentSlide = index"
                    class="thumbnail-container"
                    >
                    <v-img
                        :src="item"
                        :alt="'Ảnh phòng ' + index "
                        class="thumbnail"
                        :class="{ active: currentSlide === index }"
                    />
                    </div>
                </v-row>
            </v-row>
        </BCol>
        <BCol class="col-xl-4">
            <div style="min-height: 480px;">
                <h3>{{ product.name }}</h3>
                <h1 class="text-red">{{ this.$common.formatTablePrice(product.price) }}</h1>
                <ul class="text-xl text-black">
                    <li><strong>Loại sản phẩm: </strong>{{ product.productTypeName }}</li>
                    <li><strong>Bảo hành: </strong>{{ product.warrantyPeriod }}</li>
                    <div v-for="(item ,index) in product.infoProduct" :key="index">
                        <li><strong>{{ item.name }}: </strong>{{ item.describe }}</li>
                    </div>
                </ul>
            </div>
            <v-btn 
            class="custom-button"
            color="#3E5C44" 
            size="large"
            variant="flat"
            @click="btnAdvise()"
            >
                <strong>TƯ VẤN</strong>
            </v-btn>
        </BCol>
    </BRow>
    <BCard >
        <BCardHeader class="p-0">
            <h3>Sản phẩm tương tự</h3>
        </BCardHeader>
        <BCardBody class="pl-0 pr-0">
            <v-slide-group v-model="model" show-arrows>
                <v-slide-group-item v-for="(item,index) in SimilarProducts" :key="index">
                    <div class="item mx-1" @click="GotoDetail(item.id)"> 
                        <div class="d-flex w-100 justify-center">
                            <img :src="item.pathImg" width="280px" height="280px" alt="">
                        </div>
                        <div class="text-center mt-3">
                            <p style="font-size: 18px !important;">{{ item.name }}</p>
                            <h3 style="color: red;" class="text">{{ item.price.toLocaleString("vi-vn") }} VNĐ</h3>
                        </div>
                    </div>
                </v-slide-group-item>
            </v-slide-group>
        </BCardBody>
    </BCard>
    <vue-easy-lightbox :visible="visible" :imgs="product.pathImg" :index="index" @hide="handleHide"></vue-easy-lightbox>
    <BModal v-model="ViewAdvise" hide-footer title="Thông tin liên hệ" modal-class="fadeInRight" centered size="md" >
        <div class="card-body">
            <v-form v-model="form" ref="form">
                <BRow>
                    <BCol class="col-lg-12">
                        <div class="form-group m-0">
                            <label class="form-label">Họ và tên*:</label>
                            <v-text-field v-model="Advise.customerName" :rules="[rules.required]" variant="outlined" clearable placeholder="Nhập vào tên loại sản phẩm" class="input-control"></v-text-field>
                        </div>
                    </BCol>
                    <BCol class="col-lg-12">
                        <div class="form-group m-0">
                            <label class="form-label">Số điện thoại*:</label>
                            <v-text-field v-model="Advise.sDTCustomer" :rules="[rules.validPhone]" variant="outlined" clearable placeholder="Nhập vào tên loại sản phẩm" class="input-control"></v-text-field>
                        </div>
                    </BCol>
                    <BCol class="col-lg-12">
                        <div class="form-group m-0">
                            <label class="form-label">Số điện thoại Zalo*:</label>
                            <v-text-field v-model="Advise.sDTZaloCustomer" :rules="[rules.validPhone]" variant="outlined" clearable placeholder="Nhập vào tên loại sản phẩm" class="input-control"></v-text-field>
                        </div>
                    </BCol>
                    <BCol class="col-lg-12">
                        <div class="form-group m-0">
                            <label class="form-label">Link Facebook:</label>
                            <v-text-field v-model="Advise.fbCustomer" variant="outlined" clearable placeholder="Nhập vào tên loại sản phẩm" class="input-control"></v-text-field>
                        </div>
                    </BCol>
                </BRow>
            </v-form>
        </div>
        <div class="modal-footer v-modal-footer">
            <BButton type="button" variant="light" @click="ViewAdvise = false">Close
            </BButton>
            <BButton type="button" variant="primary" @click="SaveAdvise()" :disabled="!form">Save Changes</BButton>
        </div>
    </BModal>
</template>
<style scoped>
    .thumbnail-container {
        cursor: pointer;
        padding: 4px;
    }

    .thumbnail {
        border: 2px solid transparent;
        border-radius: 8px;
        transition: 0.3s;
    }

    .thumbnail.active {
        border-color: #1976d2; /* Màu xanh Vuetify */
        box-shadow: 0 0 8px rgba(25, 118, 210, 0.7);
    }

    .thumbnail {
        width: 50px;
        height: 50px; 
    }
    .custom-button {
        font-size: 16px;
        font-weight: bold;
        color: white;
        text-transform: none; /* Giữ nguyên chữ thường */
        border-radius: 8px; /* Bo góc nhẹ */
        background-color: #326e51;
    }
    .item{
        width: 25% !important;
    }
    img:hover {
        transform: scale(1.05);
    }
    @media (max-width: 768px) {
        img {
            height: 120px; /* Khi màn hình nhỏ hơn 768px */
            width: 120px;
        }
        .text{
            font-size: 15px;
        }
        .item{
            width: 50% !important;
        }
    }
</style>