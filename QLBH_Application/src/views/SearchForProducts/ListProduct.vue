<script>
    import pageheader from "@/components/page-header.vue"
    import { VCurrencyField } from "vue-currency-input";

    export default {
        name: "PRODUCT-LIST",
        components: {
            pageheader
        },
        directives: {
            currency: VCurrencyField,
        },
        data(){
            return{
                arrange:[
                    {title: 'Mới nhất', value: 1},
                    {title: 'Giá từ thấp đến cao', value: 2},
                    {title: 'Giá từ cao đến thấp', value: 3},
                ],
                orderBy: null,
                searchPrice: [0,1000000],
                productTypeData: [],
                searchProductType: null,
                productData:[

                ]
            }
        },
        watch: {
            // Theo dõi sự thay đổi của query parameter productName
            '$route.query.productName': function(newValue) {
                // Cập nhật giá trị của productName
                // Gọi phương thức mỗi khi query thay đổi
                this.GetProductType(newValue??""); 
                this.GetPriceProduct(newValue??"");
                this.GetProduct(newValue??"");
            },
        },
        computed:{
            filteredProduct() {
                let filtered = this.productData.filter((item) => {
                    // Lọc theo loại sản phẩm
                    const matchesProductType = this.searchProductType
                    ? item.productType === this.searchProductType
                    : true;

                    // Lọc theo tên khách 
                    const matchesPriceFrom = item.price >= this.searchPrice[0] && item.price <= this.searchPrice[1]

                    return matchesProductType &&  matchesPriceFrom;
                });
                if(this.orderBy == 2){
                    return filtered.sort((a, b) => a.price - b.price);
                }
                else if(this.orderBy == 3){
                    return filtered.sort((a, b) => b.price - a.price);
                }
                return filtered;
            },
            formattedPriceTo: {
                get() {
                    return this.$common.formatPrice(this.searchPrice[1]) ;
                },
                set(value) {
                    this.searchPrice[1] = this.$common.formatPrice(value);
                },
            },
            formattedPriceFrom: {
                get() {
                    return this.$common.formatPrice(this.searchPrice[0]) ;
                },
                set(value) {
                    this.searchPrice[0] = this.$common.formatPrice(value);
                },
            },
        },
        created(){
            var productName = this.$route.query.productName ?? "";
            this.GetProductType(productName);
            this.GetPriceProduct(productName);
            this.GetProduct(productName);
        },
        methods:{
            GetProductType(productName){
                this.$apiClient.get(`/ProductType/GetProductTypeByName?name=${productName}`)
                        .then(response=>{
                            this.productTypeData = response.data;
                        })
            },
            GetPriceProduct(productName){
                this.$apiClient.get(`/Product/GetPrice?name=${productName}`)
                        .then(response=>{
                            this.searchPrice[0] = response.data.priceFrom;
                            this.searchPrice[1] = response.data.priceTo;
                        })
            },
            GetProduct(productName){
                this.$apiClient.get(`/Product/FindProduct?name=${productName}`)
                        .then(response=>{
                            this.productData = response.data;
                        })
            },
            GotoDetail(id){
                const route = this.$router.resolve({ name: 'detail', params: { productId: id } });
                window.open(route.href, '_blank');

            }
        }
    }
</script>
<template>
    <BRow>
        <BCol class="col-xl-2">
            <pageheader title="" pageTitle="Sản phẩm" />
        </BCol>
        <BCol class="col-xl-3 p-3">
            <v-autocomplete
                clearable
                label="Loại sản phẩm"
                :items="productTypeData"
                item-title="name"
                item-value="id"
                v-model="searchProductType"
                variant="outlined"
                hide-details>
            </v-autocomplete>
        </BCol>
        <BCol class="col-xl-4 p-3">
            <div class="form-group m-0">
                <label class="form-label">Giá sản phẩm</label>
                <v-range-slider
                    v-model="searchPrice"
                    range
                    :min="searchPrice[0]"
                    :max="searchPrice[1]"
                    step="1000"
                    color="primary"
                    hide-details
                ></v-range-slider>
                <BRow>
                    <BCol class="col-6">
                        <v-text-field
                            v-model="formattedPriceFrom"
                            v-currency
                            currency="VND"
                            locale="vi-VN"
                            label="Từ"
                            hide-details
                            suffix="VNĐ"
                        ></v-text-field>
                    </BCol>
                    <BCol class="col-6">
                        <v-text-field
                            label="Đến"
                            v-model="formattedPriceTo"
                            :type="'text'"
                            hide-details
                            suffix="VNĐ"
                        ></v-text-field>
                    </BCol>
                </BRow>
            </div>
        </BCol>
        <BCol class="col-xl-3 p-3">
            <v-select
                clearable
                label="Sắp xếp"
                :items="arrange"
                item-title="title"
                item-value="value"
                v-model="orderBy"
                variant="outlined"
                hide-details>
            </v-select>
        </BCol>
    </BRow>
    <BRow style="min-height: 685px;">
        <div v-for="(item,index) in filteredProduct" :key="index" class="col-xl-3 col-12 p-2">
            <router-link @click="GotoDetail(item.id)" target="_blank">
                <div class="d-flex w-100 justify-center">
                    <img :src="item.pathImg" width="280" height="280" alt="">
                </div>
                <div class="text-center mt-3">
                    <p style="font-size: 18px !important;">{{ item.name }}</p>
                    <h3 style="color: red;">{{ item.price.toLocaleString("vi-vn") }} VNĐ</h3>
                </div>
            </router-link>
        </div>
    </BRow>
</template>