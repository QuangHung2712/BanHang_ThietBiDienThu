<style scoped>
.saved-button {
  color: red !important; /* Màu chữ trắng */
}
/* .product-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
  gap: 20px;
  margin-top: 20px;
} */
.selected {
  background-color: #ffffff;
  color: #0056b3;
  padding: 5px;
  padding-left: 25px;
  padding-right: 25px;
  border: 2px solid #0056b3;
  border-radius: 40px;
}
.selected:hover {
  background-color: #0056b3;
  color: white;
}
.view-more {
  margin-top: 20px;
  margin-bottom: 20px;
  text-align: center;
}
.view-more button:hover {
  background-color: #0056b3;
}
.v-slide-group {
  max-width: 100%;
}

.item {
  width: 240px; /* Hiển thị 4 items cùng lúc, trừ khoảng cách */
}
@media (max-width: 768px) {
  .item {
    width: 200px; /* Hiển thị 4 items cùng lúc, trừ khoảng cách */
  }
  .item img {
    height: 170px; /* Khi màn hình nhỏ hơn 768px */
    width: 170px;
  }
  .content {
    padding: 0px;
  }
  .v-slide-group__next,
  .v-slide-group__prev {
    width: 21px !important;
    min-width: 21px !important; /* Đảm bảo kích thước không bị override */
  }
}
/*  why chose us */
.why-choose-us {
  text-align: center;
  padding: 40px;
  background: #f8f8f8;
}
.reason:hover {
  transform: scale(1.15);
  transition: transform 0.3s ease;
}
.title {
  font-size: 28px;
  font-weight: bold;
  margin-bottom: 20px;
}
.reasons {
  display: flex;
  justify-content: center;
  gap: 20px;
  flex-wrap: wrap;
}
.reason {
  width: 220px;
  background: white;
  padding: 20px;
  border-radius: 10px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
  text-align: center;
}
.reason img {
  width: 120px;
  height: 120px;
  margin-bottom: 10px;
}
.reason h3 {
  font-size: 20px;
  margin-bottom: 10px;
}
.reason p {
  font-size: 14px;
  color: #555;
}
/* tại sao chọn chúng tôi */
</style>
<script>
import { Swiper, SwiperSlide } from "swiper/vue";
import { Navigation, Autoplay } from "swiper/modules";
import Feedback from "./Feedback.vue";

export default {
  data() {
    return {
      modules: [Navigation, Autoplay],
      data: [
        {
          productName: "Loại 1",
          products: [{ pathImg: "", name: "", price: "", id: 0 }],
        },
      ],
      model: null,
      categoryRefs: [],
    };
  },
  name: "LANDING",
  components: {
    Swiper,
    SwiperSlide,
    Feedback
  },
  created() {
    this.GetProduct();
  },
  methods: {
    GetProduct() {
      this.$apiClient.get(`/Product/GetAllProductByType`).then((response) => {
        this.data = response.data;
        console.log(response.data);
      });
    },
    GotoDetail(id) {
      const route = this.$router.resolve({
        name: "detail",
        params: { productId: id },
      });
      window.open(route.href, "_blank");
    },
    scrollToCategory(index) {
      if (this.categoryRefs[index]) {
        this.categoryRefs[index].scrollIntoView({
          behavior: "smooth",
          block: "start",
        });
      }
    },
  },
};
</script>

<template>
  <swiper
    :navigation="true"
    :autoplay="true"
    :modules="modules"
    :loop="true"
    class="mySwiper"
  >
    <swiper-slide>
      <img
        src="images/test/z3277142962677_4bdf325a013a1ace985c6d15a2cfe66a.jpg"
        alt=""
        class="img-fluid"
    /></swiper-slide>
    <swiper-slide
      ><img
        src="images/test/z3277143158749_978ce87bba64930b4e0bb27d3c615348.jpg"
        alt=""
        class="img-fluid"
    /></swiper-slide>
    <swiper-slide>
      <img
        src="images/test/z3277154203256_ef0789bbc28c686e512069187cbdbbcf.jpg"
        alt=""
        class="img-fluid"
    /></swiper-slide>
  </swiper>

  <div class="contact my-3">
    <h1 style="text-align: center">COOLMAX</h1>
    <p style="text-align: center">
      Chuyên cung cấp các thiết bị lạnh nhập khẩu như: Tủ mát, tủ đông, tủ trưng
      bày bánh kem...
    </p>
  </div>

  <!-- why-choose-us -->
  <div class="why-choose-us">
    <h2 class="title">Tại sao nên chọn chúng tôi?</h2>
    <div class="reasons">
      <div class="reason">
        <img src="/images/homeimage/bestquality.png" alt="Chất lượng cao" />
        <h3>Chất lượng cao</h3>
        <p>Sản phẩm đạt tiêu chuẩn quốc tế, bền bỉ, tiết kiệm điện năng.</p>
      </div>
      <div class="reason">
        <img src="/images/homeimage/tietkiem1.png" alt="Giá cả cạnh tranh" />
        <h3>Giá cả cạnh tranh</h3>
        <p>Mức giá hợp lý, phù hợp với nhu cầu kinh doanh của bạn.</p>
      </div>
      <div class="reason">
        <img src="/images/homeimage/tietkiem.png" alt="Bảo hành dài hạn" />
        <h3>Bảo hành dài hạn</h3>
        <p>Bảo hành chính hãng lên đến 5 năm, hỗ trợ kỹ thuật 24/7.</p>
      </div>
      <div class="reason">
        <img src="/images/homeimage/help.png" alt="Dịch vụ tận tâm" />
        <h3>Dịch vụ tận tâm</h3>
        <p>Đội ngũ tư vấn chuyên nghiệp, hỗ trợ tận nơi trên toàn quốc.</p>
      </div>
    </div>
  </div>

  <!-- why-choose-us -->
  <!-- why-choose-us -->
  <div>
    <!-- Các nút chọn loại sản phẩm -->
    <div class="category-buttons" style="text-align: center">
      <button
        v-for="(item, index) in data"
        :key="'btn-' + index"
        @click="scrollToCategory(index)"
        class="btn btn-primary m-2 selected"
        style="margin-left: 50px"
      >
        {{ item.productName }}
      </button>
    </div>

    <div class="row">
      <div class="container content col-lg-9" v-if="true">
        <div
          v-for="(item, index) in data"
          :key="index"
          :ref="(el) => (categoryRefs[index] = el)"
        >
          <h3 style="text-align: center">{{ item.productName }}</h3>
          <hr />
          <v-slide-group v-model="model" show-arrows class="pb-4">
            <v-slide-group-item
              v-for="(itemProduct, indexProduct) in item.products"
              :key="indexProduct"
            >
              <a
                @click="GotoDetail(itemProduct.id)"
                target="_blank"
                class="product-card item m-2 my-3"
              >
                <div>
                  <img
                    :src="itemProduct.pathImg"
                    alt="product"
                    style="width: 100%"
                  />
                </div>
                <div class="detail">
                  <div class="product-name" style="text-align: center">
                    {{ itemProduct.name }}
                  </div>
                  <h5
                    style="
                      color: red;
                      text-align: center;
                      background-color: #f8f8f8;
                    "
                    class="text m-0"
                  >
                    {{ itemProduct.price.toLocaleString("vi-vn") }} VNĐ
                  </h5>
                </div>
              </a>
            </v-slide-group-item>
          </v-slide-group>
        </div>
        <div class="view-more">
          <BButton type="button" variant="primary" to="lstproduct"
            >Xem thêm</BButton
          >
        </div>
      </div>
      <div class="col-lg-3">
        <div class="img-iner">
          <img
            src="https://goldcool.vn/wp-content/uploads/2022/03/z3277154187962_8cdd04adef520c62e826840f1a73bafa-scaled.jpg"
            alt="https://goldcool.vn/wp-content/uploads/2022/03/z3277154187962_8cdd04adef520c62e826840f1a73bafa-scaled.jpg"
            style="width: 100%"
          />
        </div>
      </div>
    </div>
  </div>
  <!-- why-choose-us -->
  <!-- why-choose-us -->
  <div class="container content" v-if="false">
    <div v-for="(item, index) in data" :key="index">
      <h3>{{ item.productName }}</h3>
      <hr />
      <v-slide-group v-model="model" show-arrows class="pb-4">
        <v-slide-group-item
          v-for="(itemProduct, indexProduct) in item.products"
          :key="indexProduct"
        >
          <a
            @click="GotoDetail(itemProduct.id)"
            target="_blank"
            class="product-card item m-2 my-3"
          >
            <div><img :src="itemProduct.pathImg" alt="product" /></div>
            <div class="product-name">{{ itemProduct.name }}</div>
            <h4 style="color: red" class="text m-0">
              {{ itemProduct.price.toLocaleString("vi-vn") }} VNĐ
            </h4>
          </a>
        </v-slide-group-item>
      </v-slide-group>
    </div>
    <div class="view-more">
      <BButton type="button" variant="primary" to="lstproduct"
        >Xem thêm</BButton
      >
    </div>
  </div>
  <feedback></feedback>
</template>
