<template>
    <div>
      <div style="text-align: center">
        <div class="d-flex justify-content-center">
          <p>4.9 ⭐ Đánh giá sản phẩm</p>
          <p>(347)</p>
          <button
            @click="toggleReviews"
            class="toggle-reviews-button"
            style="margin-bottom: 16px; margin-left: 10px; padding-top: 1px"
          >
            {{ showReviews ? "Ẩn bớt <<<<" : "Xem tất cả >>>" }}
          </button>
        </div>
      </div>
  
      <div v-if="showReviews" class="customer-reviews">
        <h2>Đánh giá và Phản hồi Khách Hàng</h2>
        <ul>
          <li v-for="(review, index) in reviews" :key="index" class="review-item">
            <div class="review-header">
              <img :src="review.avatar" alt="Avatar" class="avatar" />
              <div class="review-info">
                <div class="review-top">
                  <p class="review-name">{{ review.name }}</p>
                  <div class="review-stars">
                    <span v-for="star in 5" :key="star" class="star">
                      {{ star <= Math.round(review.rating) ? "⭐" : "☆" }}
                    </span>
                  </div>
                </div>
                <p class="review-date">{{ review.date }}</p>
              </div>
            </div>
            <p class="review-comment">
              {{ review.comment }}
            </p>
          </li>
        </ul>
  
        <!-- Form nhập đánh giá -->
        <div class="review-form">
          <h2>Viết đánh giá của bạn</h2>
          <input
            v-model="newReview.name"
            placeholder="Nhập tên của bạn"
            class="input"
          />
          <textarea
            v-model="newReview.comment"
            placeholder="Nhập đánh giá..."
            class="textarea"
          ></textarea>
          <div class="rating">
            <span
              v-for="star in 5"
              :key="star"
              @click="newReview.rating = star"
              class="star-select"
            >
              {{ star <= newReview.rating ? "⭐" : "☆" }}
            </span>
          </div>
          <button @click="submitReview" class="submit-button">
            Gửi đánh giá
          </button>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    name: "CustomerReviews",
    data() {
      return {
        showReviews: false, // Ẩn/hiện danh sách đánh giá
        reviews: [
          {
            name: "Nguyễn Văn Sang",
            date: "1/02/2025",
            avatar:
              "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ7y9nNBUQgRiOEo-UjprbjovaxTSdigSzx_A&s",
            rating: 5,
            comment:
              "Sản phẩm thiết kế đẹp, mới sử dụng thử nên chưa biết có tiết kiệm điện hay không. Bên trong tủ trữ đồ thoải mái, cấp đông nhanh nên giữ đồ được lâu, động cơ thấy chạy êm.",
          },
          {
            name: "Lê Văn Luyện",
            date: "13/03/2025",
            avatar:
              "https://kenh14cdn.com/2019/1/6/dscf7559-15467169486081168127820-15467174973382129011568-1546717514370472635671.png",
            rating: 4.5,
            comment:
              "Sản phẩm sử dụng tạm ổn, kiểu dáng cũng được, bảo quản đồ thoải mái, hoạt động êm. Nói chung mẫu tủ này dùng được.",
          },
          {
            name: "Ngọc Huyền",
            date: "14/12/2024",
            avatar:
              "https://img.meta.com.vn/Data/image/2020/07/30/toc-dep-tuoi-trung-nien-3.jpg",
            rating: 4,
            comment:
              "Mua năm ngoái giờ vẫn thấy tủ đông dùng tốt, hoạt động ổn định, chạy êm.",
          },
        ],
        // Thông tin đánh giá mới
        newReview: {
          name: "",
          comment: "",
          rating: 0,
        },
        purchased: false, // Giả định chưa mua hàng
      };
    },
    methods: {
      toggleReviews() {
        this.showReviews = !this.showReviews;
      },
      submitReview() {
        if (!this.purchased) {
          alert("Bạn phải mua hàng mới có thể đánh giá sản phẩm!");
          return;
        }
        if (
          this.newReview.name &&
          this.newReview.comment &&
          this.newReview.rating > 0
        ) {
          this.reviews.push({
            name: this.newReview.name,
            date: new Date().toLocaleDateString(),
            avatar: "https://cdn-icons-png.flaticon.com/512/3135/3135715.png", // Ảnh mặc định
            rating: this.newReview.rating,
            comment: this.newReview.comment,
          });
  
          // Reset form sau khi gửi
          this.newReview.name = "";
          this.newReview.comment = "";
          this.newReview.rating = 0;
        } else {
          alert("Vui lòng điền đầy đủ thông tin và chọn số sao!");
        }
      },
    },
  };
  </script>
  
  <style scoped>
  .customer-reviews {
    padding: 20px;
    background-color: #ffffff;
    border-radius: 12px;
    max-width: 600px;
    margin: 0 auto;
    box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.1);
  }
  .customer-reviews h2 {
    margin-bottom: 15px;
    font-size: 24px;
    color: #222;
    text-align: center;
  }
  .customer-reviews ul {
    list-style-type: none;
    padding: 0;
  }
  .review-item {
    margin-bottom: 15px;
    padding: 15px;
    border-radius: 8px;
    background: #f7f7f7;
    box-shadow: 0px 2px 5px rgba(0, 0, 0, 0.05);
  }
  .review-header {
    display: flex;
    align-items: center;
    margin-bottom: 10px;
  }
  .avatar {
    width: 50px;
    height: 50px;
    border-radius: 50%;
    margin-right: 15px;
    border: 2px solid #ddd;
  }
  .review-info {
    flex: 1;
  }
  .review-top {
    display: flex;
    align-items: center;
    justify-content: space-between;
  }
  .review-name {
    font-weight: bold;
    font-size: 16px;
    color: #444;
    margin: 0;
  }
  .review-date {
    font-size: 12px;
    color: #999;
    margin-top: 3px;
  }
  .review-comment {
    font-size: 14px;
    color: #333;
    margin-top: 10px;
    line-height: 1.5;
  }
  .review-stars {
    font-size: 18px;
    color: #ffcc00;
  }
  .star {
    margin-right: 3px;
  }
  .star-select {
    cursor: pointer;
    font-size: 24px;
    transition: transform 0.2s;
  }
  .star-select:hover {
    transform: scale(1.2);
  }
  .toggle-reviews-button {
    color: #007bff;
    cursor: pointer;
    font-size: 14px;
    padding: 8px 12px;
    border-radius: 6px;
  }
  .review-form {
    margin-top: 20px;
    padding: 15px;
    background: white;
    border-radius: 10px;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
  }
  .input,
  .textarea {
    width: 100%;
    padding: 8px;
    margin-top: 10px;
    border: 1px solid #ddd;
    border-radius: 6px;
    font-size: 14px;
    transition: border 0.2s;
  }
  .input:focus,
  .textarea:focus {
    border-color: #007bff;
    outline: none;
  }
  .submit-button {
    margin-top: 12px;
    padding: 12px;
    width: 100%;
    background-color: #007bff;
    color: white;
    border: none;
    border-radius: 6px;
    font-size: 16px;
    font-weight: bold;
    cursor: pointer;
    transition: background 0.3s;
  }
  .submit-button:hover {
    background-color: #218838;
  }
  </style>
  