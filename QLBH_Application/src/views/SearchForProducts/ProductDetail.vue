<script>
import pageheader from "@/components/page-header.vue";
export default {
  name: "PRODUCT-DETAIL",
  components: {
    pageheader,
  },
  data() {
    return {
      currentSlide: 0,
      product: {
        // sanpham
        name: null,
        pathImg: [],
        price: 0,
        infoProduct: [],
      },
      SimilarProducts: [], // sanpham tuong tu
      Advise: {
        //
        customerName: null,
        sDTCustomer: null,
        address: null,
        email: null,
        note: null, 
        productId: null,
        gender: 1,
      },
      index: 0,
      visible: false,
      model: null,
      ViewAdvise: false,
      form: false,
    };
  },
  created() {
    const productId = this.$route.params.productId;
    this.GetDetail(productId);
  },
  methods: {
    GetDetail(Id) {
      this.$apiClient.get(`/Product/GetDetail/${Id}`).then((response) => {
        this.product = response.data;
        this.$apiClient
          .get(
            `/Product/GetProductByType?productType=${this.product.productTypeId}&id=${Id}`
          )
          .then((response) => {
            this.SimilarProducts = response.data;
          });
      });
    },
    showImg(index) {
      this.index = index;
      this.visible = true;
    },
    handleHide() {
      this.visible = false;
    },
    GotoDetail(id) {
      this.$router.replace({
        name: this.$route.name,
        params: { productId: id },
      });
      this.GetDetail(id);
      window.scrollTo({ top: 0, behavior: "smooth" }); // Cuộn lên đầu trang
    },
    btnAdvise() {
      this.ViewAdvise = !this.ViewAdvise;
      this.Advise = {
        customerName: null,
        sDTCustomer: null,
        address: null,
        gender: 1,
        productId: this.product.id,
      };
    },
    SaveAdvise() {
      console.log(this.Advise);
      this.$apiClient.post(`/Order/Create`, this.Advise).then(() => {
        this.$notify(
          "Thao tác thành công",
          "Bạn vui lòng để ý tin nhắn  chúng tôi sẽ liên hệ với bạn",
          "success"
        );
        this.ViewAdvise = false;
      });
    },
  },
};
</script>
<template>
  <pageheader title="Chi tiết sản phẩm" pageTitle="Sản phẩm" />
  <div class="container">
    <BRow class="mb-10">
      <BCol class="col-xl-7 col-12">
        <v-row justify="center" class="m-0">
          <!-- Carousel chính -->
          <v-carousel
            v-model="currentSlide"
            hide-delimiters
            show-arrows="hover"
            height="480"
          >
            <v-carousel-item
              v-for="(item, index) in product.pathImg"
              :key="index"
            >
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
                :alt="'Ảnh phòng ' + index"
                class="thumbnail"
                :class="{ active: currentSlide === index }"
              />
            </div>
          </v-row>
        </v-row>
      </BCol>
      <BCol class="col-xl-5">
        <div style="min-height: 480px">
          <h3>{{ product.name }}</h3>
          <h1 class="text-red">
            {{ this.$common.formatTablePrice(product.price) }}
          </h1>
          <ul class="text-xl text-black">
            <li><strong>Kích thước(DxRxC cm): </strong>{{ product.size }}</li>
            <li><strong>Công suất: </strong>{{ product.capacity }}W</li>
            <li><strong>Hãng: </strong>{{ product.manufacturer }}</li>
            <li>
              <strong>Loại sản phẩm: </strong>{{ product.productTypeName }}
            </li>
            <li><strong>Bảo hành: </strong>{{ product.warrantyPeriod }}</li>
            <div v-for="(item, index) in product.infoProduct" :key="index">
              <li>
                <strong>{{ item.name }}: </strong>{{ item.describe }}
              </li>
            </div>
          </ul>
        </div>
        <v-btn
          class="custom-button"
          color="#4ec3f7"
          size="large"
          variant="flat"
          @click="btnAdvise()"
        >
          <strong>TƯ VẤN</strong>
        </v-btn>
      </BCol>
    </BRow>
  </div>
  <BCard>
    <BCardHeader class="p-0">
      <h3>Sản phẩm tương tự</h3>
    </BCardHeader>
    <BCardBody class="pl-0 pr-0">
        <v-slide-group v-model="model" show-arrows class="pb-4">
            <v-slide-group-item v-for="(itemProduct, indexProduct) in SimilarProducts" :key="indexProduct" >
                <a @click="GotoDetail(itemProduct.id)" target="_blank" class="product-card item m-2 my-3">
                    <div><img :src="itemProduct.pathImg" alt="product" /></div>
                    <div class="product-name">{{ itemProduct.name }}</div>
                    <h4 style="color: red;" class="text m-0">{{ itemProduct.price.toLocaleString("vi-vn") }} VNĐ</h4>
                </a>
            </v-slide-group-item>
        </v-slide-group>
    </BCardBody>
  </BCard>
  <vue-easy-lightbox
    :visible="visible"
    :imgs="product.pathImg"
    :index="index"
    @hide="handleHide"
  ></vue-easy-lightbox>
  <BModal
    v-model="ViewAdvise"
    hide-footer
    title="Thông tin liên hệ"
    modal-class="fadeInRight"
    centered
    size="lg"
  >
    <div class="card-body">
      <v-form v-model="form" ref="form">
        <BRow class="d-flex align-items-start">
          <!-- Hình ảnh sản phẩm -->
          <BCol lg="6" md="12" class="product">
            <div class="card d-flex align-center" style="width: 100%">
              <img
                class="card-img-top mt-3"
                :src="product.pathImg[0]"
                alt="Card image"
                style="width: 300px; height: 300px"
              />
              <div class="card-body">
                <p class="card-text">{{ product.name }}</p>
                <h3 class="text-red">
                  {{ product.price.toLocaleString("vi-vn") }} VNĐ
                </h3>
              </div>
            </div>
            <h5>Bạn vui lòng nhập đúng số điện thoại để chúng tôi sẽ gọi xác nhận đơn hàng trước khi giao hàng. Xin cảm ơn!</h5>
          </BCol>

          <!-- Form nhập thông tin (bên phải thẻ card) -->
          <BCol lg="6" md="12">
            <div class="form-container">
              <div class="form-group m-0">
                <label class="form-label">Họ và tên*:</label>
                <v-text-field
                  v-model="Advise.customerName"
                  :rules="[rules.required]"
                  variant="outlined"
                  clearable
                  placeholder="Nhập họ và tên"
                  density="compact"
                />
              </div>
                <v-radio-group v-model="Advise.gender" hide-details>
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
                <label class="form-label">Số điện thoại*:</label>
                <v-text-field
                  v-model="Advise.sDTCustomer"
                  :rules="[rules.validPhone]"
                  variant="outlined"
                  clearable
                  placeholder="Nhập số điện thoại"
                  density="compact"
                />
              </div>

              <div class="form-group m-0">
                <label class="form-label">Email:</label>
                <v-text-field
                  v-model="Advise.email"
                  variant="outlined"
                  clearable
                  placeholder="Nhập email"
                  density="compact"
                />
              </div>

              <div class="form-group m-0">
                <label class="form-label">Địa chỉ nhận hàng:</label>
                <v-text-field
                  v-model="Advise.address"
                  variant="outlined"
                  clearable
                  placeholder="Nhập địa chỉ"
                  density="compact"
                />
              </div>

              <div class="form-group m-0">
                <label class="form-label">Ghi chú thêm:</label>
                <v-textarea
                  v-model="Advise.note"
                  variant="outlined"
                  clearable
                  placeholder="Nhập ghi chú"
                  density="compact"
                  rows="3"
                />
              </div>
            </div>
          </BCol>
        </BRow>
      </v-form>
    </div>
    <div class="modal-footer v-modal-footer">
      <BButton type="button" variant="light" @click="ViewAdvise = false"
        >Close
      </BButton>
      <BButton type="button" variant="primary" @click="SaveAdvise()" :disabled="!form">Đặt hàng ngay</BButton
      >
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
  color: white !important;
  text-transform: none; /* Giữ nguyên chữ thường */
  border-radius: 8px; /* Bo góc nhẹ */
}
.item {
  width: 25% !important;
}

@media (max-width: 768px) {
  img {
    height: 120px; /* Khi màn hình nhỏ hơn 768px */
    width: 120px;
  }
  .text {
    font-size: 15px;
  }
  .item {
    width: 50% !important;
  }
}
</style>
