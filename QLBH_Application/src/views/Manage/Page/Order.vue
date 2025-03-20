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
                        {title: 'Mã đơn hàng', value: 'id',sortable: true},
                        {title: 'Tên khách hàng',value:'customerName',sortable: true},
                        {title: 'Tên sản phẩm',value:'productName',sortable: true},
                        {title: 'SĐT',value:'sdtCustomer',sortable: true},
                        {title: 'Địa chỉ',value:'address',sortable: true},
                    ],
                headersTableProduct:[
                    {title: 'Tên sản phẩm', value: 'productName',sortable: true},
                    {title: 'Số lượng',value:'productNumber',sortable: true},
                    {title: 'Đơn giá',value:'price',sortable: true},
                    {title: 'Tổng tiền',value:'sumPrice',sortable: true},
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
                            this.viewdialog = true
                        })
            },
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
                            class="border-sm rounded-lg"
                            >
                            <template v-slot:[`item.id`]="{ item }">
                                <button @click="Detail(item.id)" class="text-primary">{{ item.id }}</button>
                            </template>
                            <template v-slot:[`item.productName`]="{ item }">
                                <span v-for="(itemProduct,index) in item.products" :key="index">{{ itemProduct.productName }} (x{{ itemProduct.productNumber }})</span>
                            </template>
                        </v-data-table>
                    </BCardBody>
                </BCard>
            </BCol>
        </BRow>
        <BModal v-model="viewdialog" hide-footer 
            :title="`Chi tiết đơn hàng: ${order.id} - ${order.orderDate}`"
            modal-class="fadeInRight"
            class="v-modal-custom" centered size="xl" >
        <div class="card-body">
            <v-form v-model="form" ref="form">
                <BRow>
                    <BCard no-body>
                        <BCardHeader class="p-3 "><h4 class="m-0">Khách hàng</h4></BCardHeader>
                        <BCardBody  class="p-3">
                            <h5>{{ order.customerName }} - {{ order.gender }}</h5>
                            <p class="m-0">SĐT: {{ order.sdtCustomer }} </p>
                            <p>Địa chỉ: {{ order.address }}</p>
                        </BCardBody>
                    </BCard>
                    <v-data-table 
                        :headers = "headersTableProduct"
                        :items="order.products"
                        class="border-sm rounded-lg"
                        disable-pagination
                        hide-default-footer
                        >
                        <template v-slot:[`item.productName`]="{ item }">
                            <div class="d-flex align-center mt-1">
                                <v-avatar :image="item.productIMG" size="60" class="mr-1"></v-avatar>
                                <div>
                                    <h5 class="m-0">{{ item.productName }}</h5>
                                    <p class="m-0">Mã sản phẩm: {{ item.id }}</p>
                                </div>
                            </div>
                        </template>
                        <template v-slot:[`item.price`]="{ item }">
                           {{ this.$common.formatTablePrice(item.price) }}
                        </template>
                        <template v-slot:[`item.sumPrice`]="{ item }">
                           {{ this.$common.formatTablePrice(item.price * item.productNumber) }}
                        </template>
                    </v-data-table>
                    
                </BRow>
            </v-form>
        </div>
        <div class="modal-footer v-modal-footer">
            <div class="d-flex justify-space-between w-100">
                <h4 class="m-0">Tổng tiền: 1.000.000 VNĐ</h4>
                <BButton type="button" variant="light" @click="viewdialog = false">Close
                </BButton>
            </div>
            
        </div>
    </BModal>
</template>


