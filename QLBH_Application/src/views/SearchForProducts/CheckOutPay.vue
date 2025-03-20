<template>
    <BRow v-if="products.length">
        <BCol class="col-xl-6 col-12">
            <BCard no-body>
                <BCardHeader class="p-3">
                    <h4 class="m-0">Thông tin thanh toán</h4>
                </BCardHeader>
                <BCardBody class="p-3">
                    <v-form v-model="form">
                        <div class="form-group m-0">
                            <label class="form-label">Họ và tên:</label>
                            <v-text-field
                            v-model="order.customerName"
                            :rules="[rules.required]"
                            variant="outlined"
                            clearable
                            placeholder="Nhập địa chỉ"
                            density="compact"
                            />
                        </div>
                        <v-radio-group v-model="order.gender">
                            <div class="d-flex align-items-center">
                            <div class="text-body-1 font-weight-bold">Giới tính</div>
                            <div>
                                <v-radio label="Nam" :value="1"></v-radio>
                            </div>
                            <div>
                                <v-radio label="Nữ" :value="0"></v-radio>
                            </div>
                            </div>
                        </v-radio-group>
                        <div class="form-group m-0">
                            <label class="form-label">Số điện thoại:</label>
                            <v-text-field
                            v-model="order.sdtCustomer"
                            :rules="[rules.validPhone]"
                            variant="outlined"
                            clearable
                            placeholder="Nhập địa chỉ"
                            density="compact"
                            />
                        </div>
                        <div class="form-group m-0">
                            <label class="form-label">Địa chỉ:</label>
                            <v-text-field
                            v-model="order.address"
                            variant="outlined"
                            clearable
                            placeholder="Nhập địa chỉ"
                            density="compact"
                            />
                        </div>
                        <div class="form-group m-0">
                            <label class="form-label">Ghi chú thêm:</label>
                            <v-textarea
                            v-model="order.note"
                            variant="outlined"
                            clearable
                            placeholder="Nhập ghi chú"
                            density="compact"
                            rows="3"
                            />
                        </div>
                    </v-form>
                </BCardBody>
            </BCard>
        </BCol>
        <BCol class="col-xl-6 col-12">
            <BCard no-body>
                <BCardHeader class="p-3">
                    <h4 class="m-0">Đơn hàng của bạn</h4>
                </BCardHeader>
                <BCardBody class="p-3">
                    <div class="d-flex justify-space-between">
                        <h5 class="m-0">Sản phẩm</h5>
                        <h5 class="m-0">Tạm tính</h5>
                    </div>
                    <hr>
                    <div v-for="(item,index) in products" :key="index" class="d-flex justify-space-between">
                        <h6>{{ item.name }} (x{{ item.quantity}})</h6>
                        <h5 class="text-red">{{ this.$common.formatTablePrice(item.price * item.quantity) }}</h5>
                    </div>
                    <hr>
                    <div class="d-flex justify-space-between">
                        <h5>Tổng</h5>
                        <h5 class="text-red">{{  this.$common.formatTablePrice(sumPrice) }}</h5>
                    </div>
                    <BButton class="my-4" :disabled="!form" @click="SaveOrder()">Đặt hàng</BButton>
                    <p>Thông tin cá nhân của bạn sẽ được sử dụng để xử lý đơn hàng, tăng trải nghiệm sử dụng website, và cho các mục đích cụ thể khác đã được mô tả trong chính sách riêng tư.</p>
                </BCardBody>
            </BCard>
        </BCol>
    </BRow>
    <h1 v-else>Chưa có sản phẩm nào trong giỏ hàng</h1>
</template>
<script >
export default {
    data() {
        return{
            order:{
                
            },
            products: [
                
            ],
            form: false
        }
    },
    computed: {
        sumPrice() {
            return this.products.reduce((sum, item) => sum + item.quantity * item.price, 0);
        }
    },
    created(){
        const cart = localStorage.getItem("cart")
        if(cart){        
            this.getData(cart)
        }
    },
    methods:{
        getData(data){
            if(data.length){
                this.$apiClient.post(`/Product/GetAllProductById`,data)
                    .then((response) => {
                        this.products = response.data;
                    })
            }
        },
        SaveOrder(){
            this.order.products = this.products
            this.$apiClient.post(`/Order/Create`, this.order).then(() => {
                this.$notify(
                "Đặt hàng thành công",
                "Bạn vui lòng để ý tin nhắn  chúng tôi sẽ liên hệ với bạn",
                "success"
                );
                localStorage.removeItem("cart")
            });
        }
    }
}
</script>