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
  <pageheader title="" pageTitle="Giới thiệu" />
  <div class="about-container">
    <h1 class="title">
      <H1 style="color: red">AN CHI</H1> THIẾT BỊ NHÀ HÀNG , NỒI PHỞ , BẾP CÔNG
      NGHIỆP HÀNG ĐẦU VIỆT NAM
    </h1>
    <p class="intro">
      <strong>AN CHI </strong> chuyên nhập khẩu và phân phối các sản phẩm gia
      dụng, điện lạnh có thương hiệu <strong>uy tín</strong>.
    </p>
    <p class="description">
      AN CHI chuyên cung cấp các thiết bị nhà bếp , nhà hàng, nhập khẩu nguyên
      chiếc như: Tủ mát, tủ đông, nồi phở , tủ cơm , thiết bị nhà hàng inox và
      số đồ gia dụng đông lạnh khác.
    </p>
    <div class="contact-info">
      <div>
        <strong>Trụ sở chính:</strong> Lai xá Kim chung Hoài đức, Hanoi, Vietnam
      </div>
      <div>
        <strong>Văn Phòng Hà Nội:</strong> Lai xá Kim chung Hoài đức, Hanoi,
        Vietnam
      </div>
      <div><strong>Hotline CSKH:</strong> 038 612 5907 – 038 612 5907</div>
      <div>
        <strong>Facebook:</strong>
        <a href="https://www.facebook.com/thietbinhahanganchi" target="_blank"
          >AnChi_thiết bị nhà bếp
        </a>
      </div>
    </div>
    <div class="commitment">
      <h2>Cam kết về các sản phẩm của chúng tôi</h2>
      <ul>
        <li>
          Liên tục cập nhật công nghệ để tạo ra nhiều tính năng hữu ích, nâng
          cao trải nghiệm cho người sử dụng.
        </li>
        <li>
          Tuân thủ quy trình sản xuất nghiệm ngặt để đảm bảo sự đồng bộ, hạn chế
          phát sinh lỗi và nâng cao tính ổn định của sản phẩm.
        </li>
        <li>
          Chăm sóc khách hàng hàng đầu từ giai đoạn tìm hiểu, mua sắm đến sử
          dụng sản phẩm.
        </li>
        <li>
          Liên tục mở các gói hàng dựa trên nhu cầu của từng khu vực để mang đến
          cho đối tác lợi ích cao nhất.
        </li>
      </ul>
    </div>
    <div class="why-choose">
      <h2>Tại Sao Lên Chọn Sản Phẩm Của An chi</h2>
      <ul>
        <li>Nhập khẩu nguyên chiếc, có nguồn gốc xuất xứ rõ ràng.</li>
        <li>Tiêu chuẩn công nghệ quốc tế .</li>
        <li>Được sản xuất trên dây chuyền hiện đại.</li>
      </ul>
    </div>
  </div>
</template>
<style scoped>
.about-container {
  padding: 20px;
  text-align: left;
  background: #f5f5f5;
  border-radius: 10px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
  max-width: 800px;
  margin: 0 auto;
}
.title {
  font-size: 28px;
  font-weight: bold;
  text-align: center;
  color: #333;
}
.intro,
.description {
  margin: 10px 0;
  font-size: 16px;
}
.contact-info div {
  margin: 5px 0;
  font-size: 14px;
}
.commitment,
.why-choose {
  margin-top: 20px;
}
ul {
  list-style: inside;
  margin: 0;
  padding: 0;
}
li {
  margin: 5px 0;
  font-size: 14px;
}
</style>
