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
                    {title: 'Thứ tự mặc định', value: 0},
                    {title: 'Mới nhất', value: 1},
                    {title: 'Giá từ thấp đến cao', value: 2},
                    {title: 'Giá từ cao đến thấp', value: 3},
                ],
                orderBy: null,
                searchPrice: [0,1000000],
                productData:[
                    {id:1,name: "Tủ Bàn Mát Salad GoldCool 15 Khay 1/6 1.5M",price:3000000,img: 'images/test/z3277142962677_4bdf325a013a1ace985c6d15a2cfe66a.jpg'},
                    {id:2,name: "Sản phẩm 2",price:3000000,img: 'images/test/z3277142962677_4bdf325a013a1ace985c6d15a2cfe66a.jpg'},
                    {id:3,name: "Sản phẩm 3",price:3000000,img: 'images/test/z3277142962677_4bdf325a013a1ace985c6d15a2cfe66a.jpg'},
                    {id:4,name: "Sản phẩm 4",price:3000000,img: 'images/test/z3277142962677_4bdf325a013a1ace985c6d15a2cfe66a.jpg'},
                    {id:5,name: "Sản phẩm 5",price:3000000,img: 'images/test/z3277142962677_4bdf325a013a1ace985c6d15a2cfe66a.jpg'},
                    {id:6,name: "Sản phẩm 6",price:3000000,img: 'images/test/z3277142962677_4bdf325a013a1ace985c6d15a2cfe66a.jpg'},
                    {id:7,name: "Sản phẩm 7",price:3000000,img: 'images/test/z3277142962677_4bdf325a013a1ace985c6d15a2cfe66a.jpg'},
                    {id:8,name: "Sản phẩm 8",price:3000000,img: 'images/test/z3277142962677_4bdf325a013a1ace985c6d15a2cfe66a.jpg'},
                    {id:9,name: "Sản phẩm 9",price:3000000,img: 'images/test/z3277142962677_4bdf325a013a1ace985c6d15a2cfe66a.jpg'},
                    {id:10,name: "Sản phẩm 10",price:3000000,img: 'images/test/z3277142962677_4bdf325a013a1ace985c6d15a2cfe66a.jpg'},


                ]
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
                :items="arrange"
                item-title="title"
                item-value="value"
                v-model="orderBy"
                variant="outlined"
                hide-details>
            </v-autocomplete>
        </BCol>
        <BCol class="col-xl-3 p-3">
            <div class="form-group m-0">
                <label class="form-label">Giá phòng</label>
                <v-range-slider
                    v-model="searchPrice"
                    range
                    min="0"
                    max="10000000"
                    step="1000"
                    color="primary"
                    hide-details
                ></v-range-slider>
                <BRow>
                    <BCol class="col-6">
                        <v-text-field
                            v-model="searchPrice[0]"
                            v-currency
                            currency="VND"
                            locale="vi-VN"
                            label="Từ"
                            hide-details
                        ></v-text-field>
                    </BCol>
                    <BCol class="col-6">
                        <v-text-field
                            label="Đến"
                            v-model="searchPrice[1]"
                            :type="'text'"
                            hide-details
                            suffix="đ"
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
        <BCol class="col-xl-1 d-flex align-center">
            <BButton type="button" variant="primary" @click="saveUpdate()" >Lọc</BButton>
        </BCol>
    </BRow>
    <BRow>
        <div v-for="(item,index) in productData" :key="index" class="col-xl-3 col-12 p-2">
            <router-link >
                <div class="d-flex w-100 justify-center">
                    <img :src="item.img" width="280" height="280" alt="">
                </div>
                <div class="text-center mt-3">
                    <p style="font-size: 18px !important;">{{ item.name }}</p>
                    <h3 style="color: red;">{{ item.price.toLocaleString("vi-vn") }} VNĐ</h3>
                </div>
            </router-link>
        </div>
    </BRow>
</template>