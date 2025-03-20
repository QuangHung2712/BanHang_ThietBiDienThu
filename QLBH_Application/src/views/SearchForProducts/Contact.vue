<script>
import pageheader from "@/components/page-header.vue";
export default {
  name: "PRODUCT-LIST",
  components: {
    pageheader,
  },
};
</script>
<template>
  <pageheader title="" pageTitle="Liên hệ" />

  <div class="container">
    <div class="contact-container">
      <h2 class="contact-title">Liên hệ với chúng tôi</h2>
      <p class="contact-desc">Bạn có thắc mắc? Hãy liên hệ ngay!</p>

      <div class="contact-info">
        <div class="contact-item">
          <i class="fas fa-map-marker-alt"></i>
          <span>Cụm Công Nghiệp Đại Tự , Xã Kim Chung , Huyện Hoài Đức, Hà Nội</span>
        </div>
        <div class="contact-item">
          <i class="fas fa-phone-alt"></i>
          <span>0386.125.907</span>
        </div>
        <div class="contact-item">
          <i class="fas fa-envelope"></i>
          <span>Coolmaxvietnam@gmail.com</span>
        </div>
      </div>
    </div>
    <div class="map-container">
      <iframe
          src="https://www.google.com/maps/embed?pb=!1m17!1m12!1m3!1d2642.460570765008!2d105.71467032267958!3d21.045184092730388!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m2!1m1!2zMjHCsDAyJzQyLjQiTiAxMDXCsDQyJzU1LjkiRQ!5e1!3m2!1svi!2s!4v1740997209124!5m2!1svi!2s"
          width="80%"
          height="450"
          style="border: 0"
          allowfullscreen=""
          loading="lazy"
          referrerpolicy="no-referrer-when-downgrade"
      ></iframe>
    </div>
  </div>
</template>

<style>
.image {
  display: flex; /* Kích hoạt Flexbox */
  justify-content: center; /* Căn giữa ngang */
  align-items: center; /* Căn giữa dọc */
  height: 100%; /* Set chiều cao khung ảnh */ /* Chiều rộng full khung */

  overflow: hidden; /* Ẩn phần thừa của ảnh */
}

.image img {
  object-fit: cover; /* Giữ tỉ lệ ảnh và cắt phần thừa */
  width: 100%; /* Ảnh full khung */
  height: 100%; /* Ảnh full khung */
}
.contact-container {
  max-width: 600px;
  margin: 0 auto;
  padding: 20px;
  background: #f9f9f9;
  border-radius: 10px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
  text-align: center;
}

.contact-title {
  font-size: 24px;
  font-weight: bold;
  margin-bottom: 10px;
}

.contact-desc {
  font-size: 16px;
  color: #666;
  margin-bottom: 20px;
}

.contact-info {
  display: flex;
  flex-direction: column;
  gap: 15px;
}

.contact-item {
  display: flex;
  align-items: center;
  gap: 10px;
  font-size: 16px;
  color: #333;
}

.contact-item i {
  font-size: 20px;
  color: #323232;
}

.map-container {
  display: flex; /* Kích hoạt Flexbox */
  justify-content: center; /* Căn giữa ngang */
  align-items: center; /* Căn giữa dọc (nếu cần) */
  margin: 40px auto; /* Thêm khoảng cách trên dưới */
  width: 100%; /* Full width theo container */
  max-width: 800px; /* Giới hạn chiều rộng cho đẹp */
}

.map-container iframe {
  width: 100%; /* Full width khung map */
  border-radius: 10px; /* Bo góc map cho đẹp */
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.2); /* Thêm hiệu ứng bóng nhẹ */
}
</style>
