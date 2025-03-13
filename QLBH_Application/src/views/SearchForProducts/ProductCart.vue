<template style="min-height: auto">
    <div style="height: 50px"></div>
    <div class="shopping-page container">
      <div class="row">
        <!-- Danh sách sản phẩm -->
        <div
          class="lstProduct col-lg-7 col-12"
          style="margin-right: 50px; margin-bottom: 50px"
        >
          <div class="header row font-weight-bold border-bottom pb-2 mb-2">
            <div class="col-6 d-flex justify-content-center">Sản phẩm</div>
  
            <div class="col-2">Giá</div>
            <div class="col-2">Số lượng</div>
            <div class="col-2">Tạm tính</div>
          </div>
          <div class="product-list">
            <div
              v-for="(product, index) in products"
              :key="index"
              class="product-item row align-items-center"
            >
              <div class="col-1">
                <button @click="removeProduct(product.id)" class="btn btn-danger">
                  x
                </button>
              </div>
              <div class="col-2">
                <img :src="product.pathImg" alt="product" class="img-fluid" />
              </div>
              <div class="col-3">{{ product.name }}</div>
              <div
                class="col-2 text-danger"
                style="font-weight: bold; font-size: 1.2em"
              >
                {{ formatPrice(product.price) }}
              </div>
              <div class="col-2 d-flex align-items-center justify-content-center">
                <button @click="decreaseQuantity(product.id)" class="quantity-btn">
                  -
                </button>
                <span class="mx-2 quantity-text">{{ product.quantity }}</span>
                <button @click="increaseQuantity(product.id)" class="quantity-btn">
                  +
                </button>
              </div>
              <div
                class="col-2 text-danger"
                style="font-weight: bold; font-size: 1.2em"
              >
                {{ formatPrice(product.price * product.quantity) }}
              </div>
            </div>
          </div>
        </div>
  
        <!-- Thanh toán -->
        <div class="thanhtoan col-lg-4 col-10">
          <div class="thanhtoandetail p-4 border rounded">
            <h5 class="border-bottom pb-2">TỔNG CỘNG GIỎ HÀNG</h5>
            <div class="total d-flex justify-content-between">
              <span>Tạm tính:</span>
              <span
                class="text-danger"
                style="font-weight: bold; font-size: 1.1em"
                >{{ formatPrice(totalPrice) }}</span
              >
            </div>
            <div class="sum d-flex justify-content-between mt-2">
              <span>Tổng:</span>
              <span
                class="text-danger"
                style="font-weight: bold; font-size: 1.2em"
                >{{ formatPrice(totalPrice) }}</span
              >
            </div>
            <button class="btn w-100 mt-3 custom-btn" @click="checkout">
              TIẾN HÀNH THANH TOÁN
            </button>
          </div>
          <div class="saleCODE mt-3 p-3 border rounded">
            <label class="font-weight-bold">Mã ưu đãi</label>
            <input
              v-model="discountCode"
              type="text"
              placeholder="Mã ưu đãi"
              class="form-control mt-1"
            />
            <button class="btn w-100 mt-3 custom-btn">Áp dụng</button>
          </div>
        </div>
      </div>
      <BCard style="margin-top: 100px">
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
                @click="GotoDetail(itemProduct.id)"
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
    </div>
  </template>
  
  <script>
  export default {
    name: "ShoppingPage",
    data() {
      return {
        products: [
        ],
        discountCode: "",
        
      };
    },
    inject: ["reloadquantityCart"], // Inject hàm từ cha vào
    computed: {
      totalPrice() {
        return this.products.reduce(
          (sum, product) => sum + product.price * product.quantity,
          0
        );
      },
      cookieProduct(){
        return  JSON.parse(localStorage.getItem("cart") ?? "[]");
      }
    },
    created(){
      this.GetData();
    },
    methods: {
      formatPrice(value) {
        return new Intl.NumberFormat("vi-VN", {
          style: "currency",
          currency: "VND",
        }).format(value);
      },
      GetData(){
        this.$apiClient.post(`/Product/GetAllProductById`,this.cookieProduct)
            .then((response) => {
              this.products = response.data
            })
      },
      increaseQuantity(id) {
        // Tìm index của sản phẩm cần cập nhật
          let index = this.products.findIndex(item => item.id === id);

          if (index !== -1) {
            // Tăng quantity của sản phẩm
            this.products[index].quantity++;

            // Chỉ lưu id và quantity vào localStorage
            let updateProduct = this.products.map(item => ({ id: item.id, quantity: item.quantity }));
            localStorage.setItem("cart", JSON.stringify(updateProduct));

            // Cập nhật UI
            this.reloadquantityCart();
        }
      },
      decreaseQuantity(id) {
        // Tìm index của sản phẩm cần cập nhật
        let index = this.products.findIndex(item => item.id === id);

        if (index !== -1) {
          // Giảm quantity nhưng đảm bảo không dưới 0
          this.products[index].quantity = Math.max(0, this.products[index].quantity - 1);

          // Chỉ lưu id và quantity vào localStorage
          let updateProduct = this.products.map(item => ({ id: item.id, quantity: item.quantity }));
          localStorage.setItem("cart", JSON.stringify(updateProduct));

          // Gọi hàm cập nhật số lượng hiển thị giỏ hàng
          this.reloadquantityCart();
        }
      },
      removeProduct(id) {
        try{
          this.products = this.products.filter(item => item.id !== id);
          localStorage.setItem("cart", JSON.stringify(this.products ?? []));
          this.reloadquantityCart();
          this.$notify(
            "Thao tác thành công",
            "Xoá sản phẩm khỏi giỏ hàng thành công",
            "success"
          );
        }
        catch{
          this.$notify(
            "Đã xảy ra lỗi",
            "Xoá sản phẩm khỏi giỏ hàng đã xảy ra lỗi",
            "error"
          );
        }
      },
      checkout() {
        // Lưu giỏ hàng vào LocalStorage
        localStorage.setItem("cart", JSON.stringify(this.products));
  
        // Lưu tổng tiền
        localStorage.setItem("totalPrice", this.totalPrice);
  
        // Chuyển hướng sang trang thanh toán
        this.$router.push("checkout");
      },
    },
  };
  </script>
  
  <style scoped>
  .custom-btn {
    background-color: #1eb8ff; /* Màu cam */
    color: white;
    border: none;
  }
  
  .custom-btn:hover {
    background-color: #b4e7ff; /* Màu cam đậm hơn khi hover */
  }
  
  .product-item {
    border-bottom: 1px solid #ddd;
    padding: 10px 0;
  }
  button {
    border: none;
    background: #ddd;
    border-radius: 5px;
    width: 20px;
    height: 30px;
    padding: 5px 5px;
    cursor: pointer;
  }
  .btn-danger {
    background: transparent;
    color: rgb(118, 118, 118);
    border: 2px solid rgb(133, 133, 133);
    border-radius: 50%;
    width: 30px;
    height: 30px;
    display: flex;
    align-items: center;
    justify-content: center;
    padding: 0;
  }
  .thanhtoandetail {
    background: #f8f9fa;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
  }
  .saleCODE {
    background: #ffffff;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
  }
  </style>
  