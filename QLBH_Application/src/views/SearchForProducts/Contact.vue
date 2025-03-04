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
    <div class="image">
      <div class="image">
        <img
          src="https://scontent.fhan18-1.fna.fbcdn.net/v/t39.30808-6/377709707_823082186286727_8751057681866016829_n.png?_nc_cat=109&ccb=1-7&_nc_sid=cc71e4&_nc_eui2=AeEVPxjqhDQzSoWO_kSTaLPT6dZsU5laef7p1mxTmVp5_tchAsPnxceuDnKFv2tKO2FTP188N7tMQUityMWBRlEA&_nc_ohc=YYySfcVhHiYQ7kNvgG4XY2v&_nc_oc=AdhmuOqD64S9wOQeGI9kh3RgxW2ND7xht8IAG7VHe9y2unjDFfm1MN0o2HPepl4vBeE&_nc_zt=23&_nc_ht=scontent.fhan18-1.fna&_nc_gid=ApH1glwMugieuKJVs7lzCgD&oh=00_AYDhDAXTr69OXjFIv-81KSsT9rSn6OWC2ZvFldJe6WzzSA&oe=67CB63A7"
          alt=""
        />
      </div>
    </div>
    <div class="contact-container">
      <h2 class="contact-title">Liên hệ với chúng tôi</h2>
      <p class="contact-desc">Bạn có thắc mắc? Hãy liên hệ ngay!</p>

      <div class="contact-info">
        <div class="contact-item">
          <i class="fas fa-map-marker-alt"></i>
          <span>Lai xá Kim chung Hoài đức, Hanoi, Vietnam</span>
        </div>
        <div class="contact-item">
          <i class="fas fa-phone-alt"></i>
          <span>038 612 5907</span>
        </div>
        <div class="contact-item">
          <i class="fas fa-envelope"></i>
          <span>thietbinhahang.anchi@gmail.com</span>
        </div>
      </div>
    </div>
    <div class="map-container">
      
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
