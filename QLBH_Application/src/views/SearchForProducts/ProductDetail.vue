<script>
    import pageheader from "@/components/page-header.vue"

    export default {
        name: "PRODUCT-DETAIL",
        components: {
            pageheader
        },
        data(){
            return{
                product:{

                }
            }
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
        },
        created(){
            const productId = this.$route.params.productId;
            this.GetDetail(productId)
        },
        methods:{
            GetDetail(Id){
                this.$apiClient.get(`/Product/GetDetail/${Id}`)
                        .then(response=>{
                            this.product = response.data;
                        })
            }
        }
    }
</script>
<template>
    <pageheader title="Chi tiết sản phẩm" pageTitle="Sản phẩm" />
    <BRow>
        <BCol class="col-xl-8">

        </BCol>
    </BRow>
</template>