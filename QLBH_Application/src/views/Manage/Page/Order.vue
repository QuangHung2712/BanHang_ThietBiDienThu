<script>
    import pageheader from "@/components/page-header.vue"
    export default {
        name: "PRODUCT-LIST",
        components: {
            pageheader
        },
        data(){
            return{
                searchProduct: null,
                searchCustomerName: null,
                searchSDTCustomer: null,
                form: false,
                headersTable:[
                        {title: 'STT', value: 'stt',sortable: true},
                        {title: 'Tên khách hàng',value:'customerName',sortable: true},
                        {title: 'Tên sản phẩm',value:'productName',sortable: true},
                        {title: 'SĐT',value:'sdtCustomer',sortable: true},
                        {title: 'Địa chỉ',value:'address',sortable: true},
                        {title: 'Hành đồng',value: 'actions',sortable: false}
                    ],
                orderData: [

                ],
                product:[

                ],
                order:{

                },
                viewdialog: false
            }
        },
        computed:{
            filteredData() {
                return this.orderData.filter((item) => {
                    // Lọc theo loại sản phẩm
                    const matchesProduct = this.searchProduct
                    ? item.productId === this.searchProduct
                    : true;

                    // Lọc theo tên khách 
                    const matchesSearchCustomerName = this.searchCustomerName
                    ? item.customerName?.toLowerCase().includes(this.searchCustomerName.toLowerCase())
                    : true;
                    const matchesSearchSDTCustomer = this.searchSDTCustomer
                    ? item.sdtCustomer?.includes(this.searchSDTCustomer)
                    : true;


                    return matchesProduct &&  matchesSearchCustomerName && matchesSearchSDTCustomer;
                });
            },
        },
        created(){
            this.GetData();
            this.GetProduct();
        },
        methods:{
            GetData(){
                this.$apiClient.get(`/Order/GetAll`)
                        .then(response=>{
                            this.orderData = response.data;
                            console.log(this.orderData);
                        })
                        .catch(error=>{
                            this.$notify(error.response.data.Message,error.response.data.Errors.join('. '),"error");
                        })
            },
            GetProduct(){
                this.$apiClient.get(`/Product/GetAll`)
                        .then(response=>{
                            this.product = response.data;
                        })
            },
            Detail(Id){
                this.$apiClient.get(`/Order/GetDetail/${Id}`)
                        .then(response=>{
                            this.order = response.data;
                        })
            }
        }
}
</script>



<template>
        <pageheader title="" pageTitle="Chờ tư vấn" />
        <BRow>
            <BCol class="col-sm-12">
                <BCard>
                    <BCardBody class="p-0">
                        <BRow>
                            <BCol class="col-sm-4 col-6">
                                <v-autocomplete
                                    clearable
                                    label="Sản phẩm"
                                    :items="product"
                                    item-title="name"
                                    item-value="id"
                                    v-model="searchProduct"
                                    variant="outlined"
                                    hide-details>
                                </v-autocomplete>
                            </BCol>
                            <Bcol class="col-sm-4 col-6"><v-text-field label="Tên khách hàng" variant="outlined" v-model="searchCustomerName" clearable hide-details></v-text-field></Bcol>
                            <Bcol class="col-sm-4 col-6"><v-text-field label="SĐT khách hàng" variant="outlined" v-model="searchSDTCustomer" clearable hide-details></v-text-field></Bcol>

                        </BRow>
                    </BCardBody>
                </BCard>
                <BCard no-body class="table-card p-sm-2">
                    <BCardBody>
                        <v-data-table 
                            :headers = "headersTable"
                            :items="filteredData"
                            class="border-sm rounded-lg">
                            <template v-slot:[`item.stt`]="{ index }">
                                {{ index + 1 }}
                            </template>
                            <template v-slot:[`item.actions`]="{ item }">
                                <v-icon class="ml-lg-3" small @click="(viewdialog = !viewdialog) && (Detail(item.id))" >mdi-eye</v-icon>
                            </template>

                        </v-data-table>
                    </BCardBody>
                </BCard>
            </BCol>
        </BRow>
        <BModal v-model="viewdialog" hide-footer title="Chi tiết đơn hàng" modal-class="fadeInRight"
        class="v-modal-custom" centered size="xl" >
        <div class="card-body">
            <v-form v-model="form" ref="form">
                <BRow>
                    <BCol class="col-lg-6 d-flex justify-content-center">
                        <img :src="order.productImg" alt="Ảnh sản phẩm" width="250px" height="250px">
                    </BCol>
                    <BCol class="col-lg-6">
                        <div class="form-group m-0">
                            <label class="form-label">Tên sản phẩm:</label>
                            <v-text-field v-model="order.productName" variant="outlined" readonly class="input-control"></v-text-field>
                        </div> 
                        <div class="form-group m-0">
                            <label class="form-label">Giá:</label>
                            <v-text-field v-model="order.price" type="text" variant="outlined" readonly class="input-control"></v-text-field>
                        </div>
                    </BCol>
                    <BCol class="col-xl-6">
                        <div class="form-group m-0">
                            <label class="form-label">Tên khách hàng:</label>
                            <v-text-field v-model="order.customerName" type="text" variant="outlined" readonly class="input-control"></v-text-field>
                        </div>
                    </BCol>
                    <BCol class="col-xl-6">
                        <div class="form-group m-0">
                            <label class="form-label">Số điện thoại:</label>
                            <v-text-field v-model="order.sdtCustomer" type="text" variant="outlined" readonly class="input-control"></v-text-field>
                        </div>
                    </BCol>
                    <BCol class="col-xl-6">
                       <BRow>
                            <BCol class="col-xl-6">
                                <div class="form-group m-0">
                                    <label class="form-label">Giới tính:</label>
                                    <v-text-field v-model="order.gender" type="text" variant="outlined" readonly class="input-control"></v-text-field>
                                </div>
                            </BCol>
                            <BCol class="col-xl-6">
                                <div class="form-group m-0">
                                    <label class="form-label">Ngày đặt hàng:</label>
                                    <v-text-field v-model="order.orderDate" type="text" variant="outlined" readonly class="input-control"></v-text-field>
                                </div>
                            </BCol>
                        </BRow>
                    </BCol>
                    <BCol class="col-xl-6">
                        <div class="form-group m-0">
                            <label class="form-label">Email:</label>
                            <v-text-field v-model="order.email" type="text" variant="outlined" readonly class="input-control"></v-text-field>
                        </div>
                    </BCol>
                    <BCol class="col-xl-12">
                        <div class="form-group m-0">
                            <label class="form-label">Địa chỉ:</label>
                            <v-text-field v-model="order.address" type="text" variant="outlined" readonly class="input-control"></v-text-field>
                        </div>
                    </BCol>
                    <BCol class="col-xl-12">
                        <div class="form-group m-0">
                            <label class="form-label">Ghi chú:</label>
                            <v-text-field v-model="order.note" type="text" variant="outlined" class="input-control"></v-text-field>
                        </div>
                    </BCol>
                </BRow>
            </v-form>
        </div>
        <div class="modal-footer v-modal-footer">
            <BButton type="button" variant="light" @click="viewdialog = false">Close
            </BButton>
        </div>
    </BModal>
</template>