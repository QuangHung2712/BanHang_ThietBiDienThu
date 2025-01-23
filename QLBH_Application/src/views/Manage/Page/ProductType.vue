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
                viewdialog: false,
                titleDialog:null,
                searchName: '',
                form: false,
                headersTable:[
                        {title: 'STT', value: 'stt',sortable: true},
                        {title: 'Tên loại sản phẩm',value:'name',sortable: true},
                        {title: 'Hành đồng',value: 'actions',sortable: false}
                    ],
                productTypeData: [

                ],
                selectProductType : 
                {
                    id:0,
                    name:null
                },
            }
        },
        computed:{
            filterService(){
                return this.productTypeData.filter(type =>type.name.toLowerCase().includes((this.searchName || '').toLowerCase()))
            }
        },
        created(){
            this.GetData();
        },
        methods:{
            GetData(){
                this.$apiClient.get(`/ProductType/GetAll`)
                        .then(response=>{
                            this.productTypeData = response.data;
                            this.viewdialog = false;
                        })
                        .catch(error=>{
                            this.$notify(error.response.data.Message,error.response.data.Errors.join('. '),"error");
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
        <pageheader title="" pageTitle="Dịch vụ" />
        <BRow>
            <BCol class="col-sm-12">
                <BCard no-body class="table-card p-sm-2">
                    <BCardBody>
                        <BRow class="text-end p-sm-2 ">
                            <BCol class="col-sm-2 d-sm-block d-none"></BCol>
                            <BCol class="col-sm-4 col-6"><v-text-field label="Tên dịch vụ" variant="outlined" v-model="searchName" clearable hide-details></v-text-field></BCol>
                            <BCol class="col-sm-2 col-6"><v-btn @click="(viewdialog = !viewdialog) && (CreateEditProduct(0))" color="blue-lighten-1" class="mt-2"> Thêm loại sản phẩm </v-btn></BCol>
                        </BRow>
                        
                        <v-data-table 
                            :headers = "headersTable"
                            :items="filterService"
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
                <BModal v-model="viewdialog" hide-footer :title="titleDialog" modal-class="fadeInRight"
                    class="v-modal-custom" centered size="lg" >
                    <div class="card-body">
                        <v-form v-model="form" ref="form">
                            <BRow>
                                <BCol class="col-lg-12">
                                    <div class="form-group">
                                        <label class="form-label">Tên dịch vụ:</label>
                                        <v-text-field v-model="selectProductType.name" :rules="[rules.required]" variant="outlined" clearable placeholder="Nhập vào tên dịch vụ" class="input-control"></v-text-field>
                                    </div>
                                </BCol>
                            </BRow>
                        </v-form>
                    </div>
                    <div class="modal-footer v-modal-footer">
                        <BButton type="button" variant="light" @click="viewdialog = false">Close
                        </BButton>
                        <BButton type="button" variant="primary" @click="Save()" :disabled="!form">Save Changes</BButton>
                    </div>
                </BModal>
            </BCol>
        </BRow>
</template>