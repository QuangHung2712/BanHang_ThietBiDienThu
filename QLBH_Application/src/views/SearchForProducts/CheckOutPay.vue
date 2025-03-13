<template>
    <div class="row d-flex" style="padding-left: 70px; padding-right: ">
      <h3 style="text-align: center">Thông tin thanh toán</h3>
      <div class="profile col-lg-5 col-9">
        <div class="inputname">
          <label for="name">Họ và tên</label>
          <input
            v-model="customer.name"
            type="text"
            id="name"
            placeholder="Nhập họ và tên"
          />
        </div>
        <div class="inputphone">
          <label for="phone">Số điện thoại</label>
          <input
            v-model="customer.phone"
            type="text"
            id="phone"
            placeholder="Nhập số điện thoại"
          />
        </div>
        <div class="grener">
          <label for="grener">Giới tính</label>
          <select v-model="customer.gender" name="grener" id="grener">
            <option value="male">Nam</option>
            <option value="female">Nữ</option>
          </select>
        </div>
        <div class="inputaddress">
          <label for="address">Địa chỉ</label>
          <input
            v-model="customer.address"
            type="text"
            id="address"
            placeholder="Nhập địa chỉ"
          />
        </div>
        <div class="inputnote">
          <label for="note">Ghi chú</label>
          <textarea
            v-model="customer.note"
            id="note"
            placeholder="Nhập ghi chú"
          ></textarea>
        </div>
      </div>
      <div class="col-1"></div>
      <!-- Hiển thị danh sách sản phẩm trong đơn hàng -->
      <div class="donhang col-lg-5 col-10">
        <h3>ĐƠN HÀNG CỦA BẠN</h3>
        <div v-for="(item, index) in cart" :key="index" class="cart-item">
          <div class="cart-item-info">
            <img :src="item.image" alt="product" class="product-image" />
            <div>
              <p class="product-name">{{ item.name }}</p>
              <p class="product-quantity">Số lượng: {{ item.quantity }}</p>
              <p class="product-price">
                {{ formatPrice(item.price * item.quantity) }}
              </p>
            </div>
          </div>
        </div>
  
        <div class="total">
          <strong>Tổng cộng:</strong>
          <span class="text-danger">{{ formatPrice(totalPrice) }}</span>
        </div>
  
        <button class="btn btn-success w-100 mt-3" @click="showConfirmDialog">
          ĐẶT HÀNG
        </button>
      </div>
    </div>
    <v-dialog v-model="showDialog" max-width="400">
      <v-card>
        <v-card-title class="headline">Xác nhận đặt hàng</v-card-title>
        <v-card-text>Bạn có chắc chắn muốn đặt hàng không?</v-card-text>
        <v-card-actions>
          <v-btn color="red" @click="showDialog = false">Hủy</v-btn>
          <v-btn color="green" @click="placeOrder">Xác nhận</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </template>
  
  <script>
  export default {
    name: "CheckoutPage",
    data() {
      return {
        cart: [],
        totalPrice: 0,
        customer: {
          name: "",
          gender: "male",
          address: "",
          note: "",
        },
        showDialog: false,
      };
    },
    created() {
      const storedCart = localStorage.getItem("cart");
      if (storedCart) {
        this.cart = JSON.parse(storedCart);
      }
  
      this.totalPrice = this.cart.reduce(
        (sum, item) => sum + item.price * item.quantity,
        0
      );
    },
    methods: {
      formatPrice(value) {
        return new Intl.NumberFormat("vi-VN", {
          style: "currency",
          currency: "VND",
        }).format(value);
      },
      showConfirmDialog() {
        this.showDialog = true;
      },
      closeDialog() {
        this.showDialog = false;
      },
      placeOrder() {
        if (!this.customer.name || !this.customer.address) {
          alert("Vui lòng nhập đầy đủ thông tin!");
          return;
        }
  
        const orderData = {
          customer: this.customer,
          cart: this.cart,
          totalPrice: this.totalPrice,
          orderDate: new Date().toLocaleString(),
        };
  
        console.log("Đơn hàng:", orderData);
        alert("Đơn hàng đã được đặt thành công!");
  
        localStorage.removeItem("cart");
        localStorage.removeItem("totalPrice");
  
        this.cart = [];
        this.totalPrice = 0;
        this.showDialog = false;
      },
    },
  };
  </script>
  
  <style scoped>
  .row {
    display: flex;
    justify-content: center;
    align-items: start;
    flex-wrap: wrap;
    gap: 20px;
    padding: 20px;
  }
  
  h3 {
    text-align: center;
    color: #333;
    font-weight: bold;
    margin-bottom: 20px;
  }
  
  /* FORM THÔNG TIN */
  .profile {
    background: #f8f9fa;
    padding: 20px;
    border-radius: 10px;
    box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.1);
  }
  
  .profile label {
    font-weight: bold;
    margin-bottom: 5px;
    display: block;
    color: #555;
  }
  
  .profile input,
  .profile select,
  .profile textarea {
    width: 100%;
    padding: 10px;
    border: 1px solid #ddd;
    border-radius: 5px;
    font-size: 16px;
    transition: 0.3s;
  }
  
  .profile input:focus,
  .profile select:focus,
  .profile textarea:focus {
    border-color: #28a745;
    outline: none;
    box-shadow: 0px 0px 8px rgba(40, 167, 69, 0.3);
  }
  
  .inputnote textarea {
    height: 80px;
  }
  
  /* ĐƠN HÀNG */
  .donhang {
    background: white;
    padding: 20px;
    border-radius: 10px;
    box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.1);
  }
  
  .cart-item {
    display: flex;
    align-items: center;
    padding: 10px;
    border-bottom: 1px solid #ddd;
  }
  
  .cart-item:last-child {
    border-bottom: none;
  }
  
  .cart-item-info {
    display: flex;
    align-items: center;
  }
  
  .product-image {
    width: 80px;
    height: 80px;
    object-fit: cover;
    border-radius: 8px;
    margin-right: 15px;
  }
  
  .product-name {
    font-weight: bold;
    font-size: 16px;
    color: #333;
  }
  
  .product-quantity,
  .product-price {
    color: #777;
    font-size: 14px;
  }
  
  /* TỔNG TIỀN */
  .total {
    font-size: 18px;
    font-weight: bold;
    margin-top: 15px;
    text-align: right;
  }
  
  .text-danger {
    color: red;
  }
  
  /* BUTTON ĐẶT HÀNG */
  .btn-success {
    background: linear-gradient(to right, #2846a7, #526fcf);
    color: white;
    font-size: 18px;
    padding: 12px;
    border: none;
    border-radius: 5px;
    transition: 0.3s;
  }
  
  .btn-success:hover {
    background: linear-gradient(to right, #218838, #1e7e34);
    transform: scale(1.05);
  }
  
  /* RESPONSIVE */
  @media (max-width: 768px) {
    .profile,
    .donhang {
      width: 100%;
    }
  
    .cart-item-info {
      flex-direction: column;
      text-align: center;
    }
  
    .product-image {
      margin-bottom: 10px;
    }
  
    .total {
      text-align: center;
    }
  }
  </style>
  