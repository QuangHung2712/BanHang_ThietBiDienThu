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
        note: null,
        gender: 1,
        products: [
          {  quantity: 1, id: null}
        ]
      },
      index: 0,
      visible: false,
      model: null,
      ViewAdvise: false,
      form: false,
      quantity: 1,
    };
  },
  created() {
    const productId = this.$route.params.productId;
    this.GetDetail(productId);
  },
  inject: ["reloadquantityCart"], // Inject hàm từ cha vào
  methods: {
    GetDetail(nameSlug) {
      this.$apiClient.get(`/Product/GetDetail?nameSlug=${nameSlug}`).then((response) => {
        this.product = response.data;
        this.$apiClient
          .get(
            `/Product/GetProductByType?productType=${this.product.productTypeId}&nameSlug=${nameSlug}`
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
        //
        customerName: null,
        sDTCustomer: null,
        address: null,
        note: null,
        gender: 1,
        products: [
          {  quantity: this.quantity, id: this.product.id }
        ]
      }
    },
    SaveAdvise() {
      this.$apiClient.post(`/Order/Create`, this.Advise).then(() => {
        this.$notify(
          "Đặt hàng thánh công",
          "Bạn vui lòng để ý tin nhắn  chúng tôi sẽ liên hệ với bạn",
          "success"
        );
        this.ViewAdvise = false;
      });
    },
    increaseQuantity(){
      this.quantity++;
    },
    decreaseQuantity(){
      if(this.quantity > 1) this.quantity--
    },
    AddToCart(){
      if(this.product.id == undefined){
        this.$notify(
          "Đã xảy ra lỗi",
          "Không có sản phẩm không thể thêm vào giỏ hàng",
          "error"
        );
      }
      try{
        let cart = JSON.parse(localStorage.getItem("cart") ?? "[]");
        let existingProduct = cart.find(p => p.id === this.product.id);
        if (existingProduct) {
            existingProduct.quantity += this.quantity;
        } else {
            cart.push({ id: this.product.id, quantity:  this.quantity});
        }
        localStorage.setItem("cart", JSON.stringify(cart)); // Lưu 1 năm
        this.$notify(
          "Thao tác thành công",
          "Thêm sản phẩm vào giỏ hàng thành công",
          "success"
        );
        if(this.reloadquantityCart)
        {
          this.reloadquantityCart();
        }
      }
      catch{
        this.$notify(
          "Đã xảy ra lỗi",
          "Thêm sản phẩm vào giỏ hàng đã xảy ra lỗi",
          "error"
        );
      }
    },
  },
};
</script>
<template>
  <pageheader title="Chi tiết sản phẩm" pageTitle="Sản phẩm" />
  <div class="">
    <BRow class="mb-12">
      <BCol class="col-xl-5 col-12">
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
          <h2 class="text-red">
            {{ this.$common.formatTablePrice(product.price) }}
          </h2>
          <ul class="text text-black">
            <li><strong>Kích thước(DxRxC cm): </strong>{{ product.size }}</li>
            <li><strong>Công suất: </strong>{{ product.capacity }}W</li>
            <li><strong>Hãng: </strong>{{ product.manufacturer }}</li>
            <li>
              <strong>Loại sản phẩm: </strong>{{ product.productTypeName }}
            </li>
            <li><strong>Bảo hành: </strong>{{ product.warrantyPeriod }}</li>
          </ul>
          <div class="promotion">
            <h3 style="background-color: yellow">KHUYẾN MẠI</h3>
            <ul>
              <li><span>1️⃣</span> Giao hàng miễn phí nội thành Hà Nội.</li>
              <li><span>2️⃣</span> Bảo hành 24 tháng tại nhà.</li>
              <li>
                <span>3️⃣</span> Lỗi nhà sản xuất đổi mới trong 7 ngày đầu.
              </li>
            </ul>
          </div>
        </div>
        <BRow>
          <div class="col-xl-3 col-3 d-flex">
            <v-btn icon variant="outlined" @click="decreaseQuantity" rounded="0" class="square-btn">
              <v-icon size="x-small">mdi-minus</v-icon>
            </v-btn>
            <span class="text-quantity">{{ quantity }}</span>
            <v-btn icon variant="outlined" @click="increaseQuantity" rounded="0" class="square-btn">
              <v-icon size="x-small">mdi-plus</v-icon>
            </v-btn>
          </div>
          <div class="col-xl-4 col-4">
            <v-btn
              class="custom-button "
              color="#4ec3f7"
              size="large"
              variant="flat"
              @click="btnAdvise()"
            >
              <strong>Đặt hàng ngay</strong>
            </v-btn>
          </div>
          <div class="col-xl-5 col-5 p-0">
            <v-btn
              class="custom-button"
              color="#4ec3f7"
              size="large"
              variant="flat"
              magin-top="10px"
              @click="AddToCart()"
            >
              <strong>Thêm vào giỏ hàng</strong>
            </v-btn>
          </div>
        </BRow>
        
        
      </BCol>
      <div class="col-xl-2 contact-box" style="padding: 0px">
        <div class="contact-header">
          <h5 style="padding: 5px">📌 LIÊN HỆ – CỬA HÀNG</h5>
        </div>
        <div style="margin-left: 15px">
          <div class="contact-info">
            <p>
              <strong>📍 Địa chỉ:</strong> Cụm Công Nghiệp Đại Tự, Kim Chung,
              Hoài Đức, Hà Nội
            </p>
            <p><strong>📞 Hotline:</strong> 039.335.6261</p>
          </div>
          <hr />
          <div class="contact-benefits">
            <div class="benefit-item">
              <img
                src="https://goldcool.vn/wp-content/uploads/2019/02/3.jpg"
                alt="Hàng chính hãng"
              />
              <p>
                <strong>Bảo hành chính hãng</strong><br />
                24 tháng tại nhà.
              </p>
            </div>
            <div class="benefit-item">
              <img
                src="http://goldcool.vn/wp-content/uploads/2019/02/1.jpg"
                alt="Đổi trả miễn phí"
              />
              <p>
                <strong>Đổi mới trong 7 ngày</strong><br />
                Lỗi do nhà sản xuất.
              </p>
            </div>
            <div class="benefit-item">
              <img
                src="http://goldcool.vn/wp-content/uploads/2019/02/2.jpg"
                alt="Giao hàng miễn phí"
              />
              <p>
                <strong>Giao hàng lắp đặt miễn phí</strong><br />
                nội thành Hà Nội.
              </p>
            </div>
          </div>
        </div>
      </div>
    </BRow>
  </div>
  <BCard>
    <BCardHeader class="p-0">
      <h3>Sản phẩm tương tự</h3>
    </BCardHeader>
    <BCardBody class="pl-0 pr-0">
      <v-slide-group v-model="model" show-arrows class="pb-4">
        <v-slide-group-item
          v-for="(itemProduct, indexProduct) in SimilarProducts"
          :key="indexProduct"
        >
          <a
            @click="GotoDetail(itemProduct.nameSlug)"
            target="_blank"
            class="product-card item m-2 my-3"
          >
            <div>
              <img :src="itemProduct.pathImg" alt="product" />
            </div>
            <div class="product-name">{{ itemProduct.name }}</div>
            <h4 style="color: red" class="text m-0">
              {{ itemProduct.price.toLocaleString("vi-vn") }} VNĐ
            </h4>
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
            <h5>
              Bạn vui lòng nhập đúng số điện thoại để chúng tôi sẽ gọi xác nhận
              đơn hàng trước khi giao hàng. Xin cảm ơn!
            </h5>
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
              <v-radio-group v-model="Advise.gender" >
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
      <BButton
        type="button"
        variant="primary"
        @click="SaveAdvise()"
        :disabled="!form"
        >Đặt hàng ngay</BButton
      >
    </div>
  </BModal>
</template>
<style scoped>
/* CSS cho trang giảm giá*/
.promotion {
  width: fit-content;
  border-radius: 5px;
}

.promotion h3 {
  text-align: center;
  margin: 0;
  font-weight: bold;
}

.promotion ul {
  list-style: none;
  padding: 0;
  margin: 0;
  padding-left: 5px;
  padding-right: 5px;
}

.promotion li {
  display: flex;
  align-items: center;
  font-size: 16px;
  margin-top: 5px;
}

.promotion li span {
  margin-right: 8px;
}

/* CSS cho trang giảm giá*/

/*CSS cho contact*/
.contact-box {
  border: 1px solid #ddd;

  font-family: Arial, sans-serif;
}

.contact-header {
  background-color: #9b9b9b;
  color: rgb(38, 45, 225);

  text-align: center;
  font-weight: bold;
}

.contact-info {
  background-color: #f2f2f2;

  font-size: 14px;
}

.contact-info p {
  margin: 5px 0;
}

.benefit-item {
  display: flex;
  align-items: center;
  margin-bottom: 10px;
}

.benefit-item img {
  width: 40px;
  height: 40px;
  margin-right: 10px;
}

.benefit-item p {
  font-size: 14px;
  margin: 0;
}

hr {
  margin: 5px 10px;
  border: 0.5px solid #ddd;
}

/*CSS cho contact*/

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
  width: 95%;
  font-size: 16px;
  font-weight: bold;
  color: white !important;
  text-transform: none; /* Giữ nguyên chữ thường */
  border-radius: 8px; /* Bo góc nhẹ */
}
.item {
  width: 25% !important;
}
.text-black {
  font-size: 20px; /* Sửa kích cỡ chữ */
}
.square-btn {
  border: 1px solid #e0e0e0;
  height: 45px;
  width: 25px;
  background-color: #f1f1f1;
  border-radius: 0 !important; /* Nút vuông */
}
.text-quantity{
  font-size: 16px;
  border: 1px solid #e0e0e0;
  padding: 10px 20px 1px 20px;
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
  .text-quantity{
    font-size: 16px;
    border: 1px solid #e0e0e0;
    padding: 10px 10px 1px 10px;
  }
  .square-btn {
    margin-right: 5px;
  }
}
</style>
