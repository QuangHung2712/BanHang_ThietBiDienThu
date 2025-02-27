<script>
    import pageheader from "@/components/page-header.vue"
    import Swal from "sweetalert2";
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
                        {title: 'SĐT',value:'sdtCustomer',sortable: true},
                        {title: 'SĐT Zalo',value:'sdtZaloCustomer',sortable: true},
                        {title: 'Tên sản phẩm',value:'productName',sortable: true},
                        {title: 'Ảnh sản phẩm',value:'productName',sortable: true},
                        {title: 'Hành đồng',value: 'actions',sortable: false}
                    ],
                orderData: [

                ],
                product:[

                ],
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
            CreateEditProduct(id){
                if(id == 0){
                    this.titleDialog = "Thêm loại sản phẩm";
                    this.selectProductType.id = 0;
                    this.selectProductType.name = null;
                }
                else{
                    const producttype = this.productTypeData.find(Type => Type.id === id);
                    this.selectProductType = {...producttype}
                }
            },
            DeleteProduct(id,name){
                const swalWithBootstrapButtons = Swal.mixin({
                customClass: {
                    confirmButton: "btn btn-success",
                    cancelButton: "btn btn-danger ml-2",
                },
                buttonsStyling: false,
            });

            swalWithBootstrapButtons
                .fire({
                    title: "Bạn có chắc chắn không?",
                    text: `Bạn đang muốn xóa loại sản phẩm: ${name}`,
                    icon: "warning",
                    confirmButtonText: "Có!",
                    cancelButtonText: "Không!",
                    showCancelButton: true,
                })
                .then((confirm) => {
                    if (confirm.value) {
                        this.$apiClient.delete(`/ProductType/Delete/${id}`)
                                    .then(reponse=> {
                                        console.log(reponse)
                                        if(reponse.status){
                                            swalWithBootstrapButtons.fire(
                                            "Xóa thành công!",
                                            `Đã xóa thành công dịch vụ: ${name}`,
                                            "success")
                                            this.GetData()
                                        }
                                        else{
                                            swalWithBootstrapButtons.fire(
                                                reponse.data.Message,
                                                reponse.data.Errors.join('. '),
                                                "error"
                                            );
                                        }
                                    })
                                    .catch(error =>{
                                        swalWithBootstrapButtons.fire(
                                            error.reponse.data.Message,
                                            error.reponse.data.Errors.join('. '),
                                            "error"
                                        );
                                    })
                    } else if ( /* Read more about handling dismissals below */ confirm.dismiss === Swal.DismissReason.cancel) return
                });
            },
            Save(){
                this.$apiClient.put(`/ProductType/CreateEdit`,this.selectProductType)
                        .then(()=>{
                            this.$notify("Thao tác thành công","","success");
                            this.GetData();
                            this.viewdialog = false;
                        })
                        .catch(error=>{
                            this.$notify(error.response.data.Message,error.response.data.Errors.join('. '),"error");
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
                                <v-icon class="ml-lg-3" small @click="(viewdialog = !viewdialog) && (CreateEditProduct(item.id))" >mdi-pencil-circle </v-icon>
                                <v-icon class="ml-lg-3" small @click="DeleteProduct(item.id,item.name)" >mdi-delete-empty </v-icon>
                            </template>
                        </v-data-table>
                    </BCardBody>
                </BCard>
            </BCol>
        </BRow>
</template>