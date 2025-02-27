<script>
    import pageheader from "@/components/page-header.vue"
    import Swal from "sweetalert2";
    export default {
        name: "PRODUCT",
        components: {
            pageheader
        },
        data(){
            return{
                headersTable:[
                    {title: 'Mã sản phẩm', value: 'id',sortable: true},
                    {title: 'Tên sản phẩm',value:'name',sortable: true},
                    {title: 'Loại sản phẩm',value:'productTypeName',sortable: true},
                    {title: 'Giá',value: 'price',sortable: true},
                    {title: 'Hành đồng',value: 'actions',sortable: false}
                ],
                viewdialogEdit:false,
                titleDialog: null,
                time: null,
                unitFeature: null,
                form: false,
                selectProduct:{
                    id: 0,
                    name: null,
                    price: null,
                    productTypeId: null,
                    warrantyPeriod: null,
                    infoProduct:[
                        { 
                            id: 0,
                            name: null,
                            describe: null,
                        }
                    ],
                    img:[

                    ]
                },
                previewUrls:[],
                productTypeData:[],
                productData:[],
                searchName: null,
                searchProductType: null,
                viewdialogProductTypeEdit: null,
                selectProductType:{
                    id: 0,
                    name: null
                },
                form1: false,
            }
        },
        computed:{
            filteredProduct() {
                return this.productData.filter((item) => {
                    // Lọc theo loại sản phẩm
                    const matchesProductType = this.searchProductType
                    ? item.productTypeName === this.searchProductType
                    : true;

                    // Lọc theo tên khách 
                    const matchesSearchName = this.searchName
                    ? item.name?.toLowerCase().includes(this.searchName.toLowerCase())
                    : true;


                    return matchesProductType &&  matchesSearchName;
                });
            },
        },
        created(){
            this.GetProductType();
            this.GetProduct();
        },
        methods:{
            GetProductType(){
                this.$apiClient.get(`/ProductType/GetAll`)
                        .then(response=>{
                            this.productTypeData = response.data;
                        })
                        .catch(error=>{
                            this.$notify(error.response.data.Message??"Đã xảy ra lỗi",error.response.data.Errors.join('. ')?? " ","error");
                        })
            },
            GetProduct(){
                this.$apiClient.get(`/Product/GetAll`)
                        .then(response=>{
                            this.productData = response.data;
                        })

            },
            createEditProduct(id){
                if(id === 0){
                    this.titleDialog = 'Thêm sản phẩm'
                    this.selectProduct = {
                        id: 0,
                        name: null,
                        price: null,
                        productTypeId: null,
                        warrantyPeriod: null,
                        infoProduct: [
                        {
                            id: 0,
                            name: null,
                            describe: null,
                        },
                        ],
                        img: [],
                    };
                    this.time = null;
                    this.unitFeature = null;
                }
                else{
                    this.$apiClient.get(`/Product/GetDetail/${id}`)
                        .then(response=>{
                            this.selectProduct = response.data;
                            this.loadImages();
                            const str = this.selectProduct.warrantyPeriod.split(" ");
                            this.time = str[0];
                            this.unitFeature = str[1]
                        })
                        .catch(error=>{
                            this.$notify(error.response.data.Message??"Đã xảy ra lỗi",error.response.data.Errors.join('. ')?? " ","error");
                        })
                }
            },
            handleFileChange() {
                this.previewUrls = [];
                if (this.selectProduct.img) {
                    for (let file of this.selectProduct.img) {
                    const reader = new FileReader();
                    reader.onload = (e) => {
                        this.previewUrls.push(e.target.result);
                    };
                    reader.readAsDataURL(file);
                    }
                }
            },
            async loadImages(){
                this.selectProduct.img = [];
                for (let url of this.selectProduct.pathImg) {
                    try {
                    
                        const response = await fetch(url);
                        
                        const blob = await response.blob(); // Chuyển phản hồi thành Blob
                        const file = new File([blob], url.split('/').pop(), { type: blob.type }); // Tạo File từ Blob
                        // Lưu vào mảng imgRoom
                        this.selectProduct.img.push(file);
                    } catch (error) {
                        this.$notify("Đã xảy ra lỗi khi load ảnh",error,"error");
                    }
                }
            },
            DeleteInfoProduct(index){
                this.selectProduct.infoProduct.splice(index, 1);
            },
            AddInfoProduct(){
                this.selectProduct.infoProduct.push({
                    id: 0,
                    name: null,
                    describe: null
                })
            },
            FormatPrice(){
                this.selectProduct.price = this.$common.formatPrice(this.selectProduct.price);
            },
            Save(){
                if(!/^\d+$/.test(this.selectProduct.price))
                this.selectProduct.price =  this.selectProduct.price.replace(/[^\d]/g, '');
                const formData = new FormData();
                formData.append("id",this.selectProduct.id);
                formData.append("name", this.selectProduct.name);
                formData.append("price", this.selectProduct.price);
                formData.append("warrantyPeriod",this.time + " " + this.unitFeature);
                formData.append("productType", this.selectProduct.productTypeId);
                if (this.selectProduct.infoProduct) {
                    this.selectProduct.infoProduct.forEach((info, index) => {
                        formData.append(`infoProduct[${index}].name`, info.name);
                        formData.append(`infoProduct[${index}].describe`, info.describe);
                    });
                }
                if(this.selectProduct.img)
                {
                    this.selectProduct.img.forEach((File)=>{
                        formData.append("img", File);
                    })
                }
                this.$apiClient.put(`/Product/CreateEdit`,formData)
                        .then(()=>{
                            this.$notify("Thao tác thành công"," ","success");
                            this.viewdialogEdit = false
                            this.GetProduct();
                        })
                        .catch(error=>{
                            this.$notify(error.response.data.Message??"Đã xảy ra lỗi",error.response.data.Errors.join('. ')?? " ","error");
                        })
            },
            DeleteProduct(Id,Name){
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
                        text: `Bạn đang muốn xóa loại sản phẩm: ${Name}`,
                        icon: "warning",
                        confirmButtonText: "Có!",
                        cancelButtonText: "Không!",
                        showCancelButton: true,
                    })
                    .then((confirm) => {
                        if (confirm.value) {
                            this.$apiClient.delete(`/Product/Delete/${Id}`)
                                .then(()=>{
                                    this.$notify("Thao tác thành công"," ","success");
                                    this.GetProduct();
                                })
                                .catch(error=>{
                                    this.$notify(error.response.data.Message??"Đã xảy ra lỗi",error.response.data.Errors.join('. ')?? " ","error");
                                })
                        } else if ( /* Read more about handling dismissals below */ confirm.dismiss === Swal.DismissReason.cancel) return
                    });
            },
            onAddClick(){
                this.selectProductType.id = 0;
                this.selectProductType.name = null;
            },
            SaveProductType(){
                this.$apiClient.put(`/ProductType/CreateEdit`,this.selectProductType)
                        .then(()=>{
                            this.$notify("Thao tác thành công","","success");
                            this.viewdialogProductTypeEdit = false;
                            this.GetProductType();
                        })
                        .catch(error=>{
                            this.$notify(error.response.data.Message,error.response.data.Errors.join('. '),"error");
                        })
            },
            formatTable(data){
                var result = this.$common.formatTablePrice(data)
                return result
            }
        }
    }
</script>
<template>
    <pageheader title="" pageTitle="Sản phẩm" />
    <BCard>
        <BCardBody class="p-0">
            <BRow>
                <BCol class="col-sm-4 col-6">
                    <v-autocomplete
                        clearable
                        label="Loại sản phẩm"
                        :items="productTypeData"
                        item-title="name"
                        item-value="name"
                        v-model="searchProductType"
                        variant="outlined"
                        hide-details>
                    </v-autocomplete>
                </BCol>
                <Bcol class="col-sm-8 col-6"><v-text-field label="Tên sản phẩm" variant="outlined" v-model="searchName" clearable hide-details></v-text-field></Bcol>
            </BRow>
        </BCardBody>
    </BCard>
    <BCard no-body class="table-card p-sm-2">
        <BCardBody>
            <BRow class="text-end pb-3">
                <BCol class="col-sm-10 col-6"><v-btn color="blue-lighten-1" class="mt-2" @click="(viewdialogEdit = !viewdialogEdit) && (createEditProduct(0))"> Thêm sản phẩm</v-btn></BCol>
                <!-- <BCol class="col-sm-3 col-6"><v-btn color="blue-lighten-1" class="mt-2"> Tính tiền Phòng</v-btn></BCol> -->
            </BRow>
            <v-data-table 
            :headers = "headersTable"
                :items="filteredProduct"
                class="border-sm rounded-lg">
                <template v-slot:[`item.price`]="{ item }">
                    <!-- Hiển thị giá đã định dạng -->
                    {{ this.$common.formatTablePrice(item.price) }}
                </template>
                <template v-slot:[`item.actions`]="{ item }">
                    <v-icon small @click="(viewdialogDetail = !viewdialogDetail)&& (DetailRoom(item.id))" title="Xem chi tiết">mdi-eye</v-icon>
                    <v-icon class="ml-lg-3" small @click="(viewdialogEdit = !viewdialogEdit) && (createEditProduct(item.id))" title="Sửa phòng" >mdi-pencil-circle </v-icon>
                    <v-icon class="ml-lg-3" v-show="!item.customerName" small @click="DeleteProduct(item.id,item.name)" title="Xoá phòng" >mdi-delete-empty </v-icon>
                </template>
            </v-data-table>
        </BCardBody>
    </BCard>
    <BModal v-model="viewdialogEdit" hide-footer :title="titleDialog" modal-class="fadeInRight"
        class="v-modal-custom" centered size="xl" >
        <div class="card-body">
            <v-form v-model="form" ref="form">
                <BRow>
                    <BCol class="col-lg-5">
                        <div class="form-group m-0">
                            <label class="form-label">Tên sản phẩm:</label>
                            <v-text-field v-model="selectProduct.name" :rules="[rules.required]" variant="outlined" clearable placeholder="Nhập vào tên sản phẩm" class="input-control"></v-text-field>
                        </div>
                        <div class="form-group m-0">
                            <label class="form-label">Giá:</label>
                            <v-text-field v-model="selectProduct.price" :rules="[rules.required]" @input="FormatPrice" type="text" variant="outlined" clearable placeholder="Nhập vào giá của sản phẩm" class="input-control"></v-text-field>
                        </div>
                        <div class="form-group m-0">
                            <label class="form-label">Loại sản phẩm:</label>
                            <v-autocomplete
                                clearable
                                :items="productTypeData"
                                item-title="name"
                                item-value="id"
                                v-model="selectProduct.productTypeId"
                                placeholder="Chọn loại sản phẩm"
                                dense
                                variant="outlined">
                                <template #append>
                                    <v-icon 
                                    class="cursor-pointer"
                                    @click="(viewdialogProductTypeEdit = !viewdialogProductTypeEdit) && (onAddClick())"
                                    >
                                    mdi-plus
                                    </v-icon>
                                </template>
                            </v-autocomplete>
                        </div>
                        <div class="form-group m-0">
                            <label class="form-label">Thời gian bảo hành:</label>
                            <BRow>
                                <BCol class="col-lg-6">
                                    <v-select
                                        clearable
                                        label="Thời gian"
                                        :items=[1,2,3,4,5,6,7,8,9,10,11,12]
                                        v-model="time"
                                        variant="outlined">
                                    </v-select>
                                </BCol>
                                <BCol class="col-lg-6">
                                    <v-select
                                        clearable
                                        label="Đơn vị tính"
                                        :items="['Năm','Tháng']"
                                        v-model="unitFeature"
                                        variant="outlined">
                                    </v-select>
                                </BCol>
                            </BRow>
                        </div>
                    </BCol>
                    <BCol class="col-lg-7">
                        <div v-for="(info, index) in selectProduct.infoProduct" :key="index">
                            <div class="form-group m-0">
                                <label class="form-label d-flex justify-content-between align-items-center">
                                    Thông tin số {{ index + 1 }} sản phẩm:
                                    <BButton v-if="index != 0" class="py-0" type="button" variant="primary" @click="DeleteInfoProduct(index)" >Xoá</BButton>
                                </label>
                                <BRow>
                                    <BCol class="col-lg-4">
                                        <v-text-field v-model="info.name"  label="Tên thông số" :rules="[rules.required]" type="text" variant="outlined" clearable placeholder="Nhập vào tên của thống số" class="input-control"></v-text-field>
                                    </BCol>
                                    <BCol class="col-lg-8">
                                        <v-text-field v-model="info.describe"  label="Mô tả" :rules="[rules.required]" type="text" variant="outlined" clearable placeholder="Nhập vào mô tả của thống số" class="input-control"></v-text-field>
                                    </BCol>
                                </BRow>
                            </div>
                        </div>
                        <BButton type="button" variant="primary" @click="AddInfoProduct(index)" >Thêm thông số</BButton>
                    </BCol>
                    <BCol class="col-lg-12">
                        <div class="form-group">
                            <label class="form-label">Ảnh sản phẩm:</label>
                            <v-file-input
                                v-model="selectProduct.img"
                                multiple
                                accept="image/*"
                                show-size
                                @change="handleFileChange"
                            ></v-file-input>
                        </div>
                    </BCol>
                    <v-row>
                        <v-col
                            v-for="(file, index) in selectProduct.img"
                            :key="index"
                            cols="12"
                            sm="6"
                            md="3"
                        >
                            <v-img
                                :src="previewUrls?.[index] || selectProduct?.pathImg?.[index]"
                                aspect-ratio="1"
                                class="mb-4"
                            ></v-img>
                            <p>{{ file.name }}</p>
                        </v-col>
                    </v-row>
                </BRow>
            </v-form>
        </div>
        <div class="modal-footer v-modal-footer">
            <BButton type="button" variant="light" @click="viewdialogEdit = false">Close
            </BButton>
            <BButton type="button" variant="primary" @click="Save()" :disabled="!form">Save Changes</BButton>
        </div>
    </BModal>
    <BModal v-model="viewdialogProductTypeEdit" hide-footer title="Thêm loại sản phẩm" modal-class="fadeInRight"
        class="v-modal-custom" centered size="md" >
        <div class="card-body">
            <v-form v-model="form1" ref="form">
                <BRow>
                    <BCol class="col-lg-12">
                        <div class="form-group m-0">
                            <label class="form-label">Tên loại sản phẩm:</label>
                            <v-text-field v-model="selectProductType.name" :rules="[rules.required]" variant="outlined" clearable placeholder="Nhập vào tên loại sản phẩm" class="input-control"></v-text-field>
                        </div>
                    </BCol>
                </BRow>
            </v-form>
        </div>
        <div class="modal-footer v-modal-footer">
            <BButton type="button" variant="light" @click="viewdialogProductTypeEdit = false">Close
            </BButton>
            <BButton type="button" variant="primary" @click="SaveProductType()" :disabled="!form1">Save Changes</BButton>
        </div>
    </BModal>
</template>