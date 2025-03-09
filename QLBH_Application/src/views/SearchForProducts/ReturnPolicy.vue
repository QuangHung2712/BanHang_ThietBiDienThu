<script>
    import pageheader from "@/components/page-header.vue"

    export default {
        name: "PRODUCT-LIST",
        components: {
            pageheader
        },
    }
</script>
<template>
  <pageheader title="" pageTitle="Chính sách đổi trả" />
  <section class="return-policy">
    <div class="container">
      <h1>CHÍNH SÁCH ĐỔI TRẢ</h1>
      <p>
        Coolmax Việt Nam cam kết mang đến cho khách hàng những sản phẩm chất
        lượng cao và dịch vụ hậu mãi tốt nhất. Dưới đây là chính sách đổi trả áp
        dụng cho các sản phẩm thực phẩm đông lạnh.
      </p>

      <h2>1. Điều kiện đổi trả</h2>
      <ul>
        <li>
          Sản phẩm bị lỗi do nhà sản xuất hoặc trong quá trình vận chuyển.
        </li>
        <li>
          Sản phẩm còn nguyên tem, bao bì, chưa qua sử dụng hoặc bảo quản sai
          cách.
        </li>
        <li>
          Khách hàng phải thông báo trong vòng <strong>24 giờ</strong> kể từ khi
          nhận hàng.
        </li>
        <li>Có hóa đơn mua hàng hợp lệ.</li>
      </ul>

      <h2>2. Những trường hợp không hỗ trợ đổi trả</h2>
      <ul>
        <li>
          Sản phẩm đã bị mở bao bì, sử dụng hoặc không còn trong tình trạng ban
          đầu.
        </li>
        <li>
          Khách hàng bảo quản không đúng quy định làm ảnh hưởng đến chất lượng
          sản phẩm.
        </li>
        <li>
          Sản phẩm đã quá thời gian yêu cầu đổi trả (sau 24 giờ kể từ khi nhận
          hàng).
        </li>
        <li>Không có hóa đơn mua hàng hợp lệ.</li>
      </ul>

      <h2>3. Quy trình đổi trả</h2>
      <ol>
        <li>
          Khách hàng liên hệ ngay với
          <a href="tel:0386125907" class="clickable">Hotline: 0386.125.907</a>
          hoặc gửi email về
          <a href="mailto:Coolmaxvietnam@gmail.com" class="clickable"
            >Coolmaxvietnam@gmail.com</a
          >
        </li>
        <li>Nhân viên kiểm tra thông tin và hướng dẫn quy trình đổi trả.</li>
        <li>Khách hàng gửi sản phẩm về địa chỉ quy định.</li>
        <li>
          Sau khi kiểm tra và xác nhận đủ điều kiện, chúng tôi sẽ gửi sản phẩm
          thay thế hoặc hoàn tiền (nếu có).
        </li>
      </ol>

      <h2>4. Liên hệ hỗ trợ</h2>
      <p>
        <a href="tel:0386125907" class="clickable">Hotline: 0386.125.907</a>
      </p>
      <p>
        <a href="mailto:Coolmaxvietnam@gmail.com" class="clickable"
          >Coolmaxvietnam@gmail.com</a
        >
      </p>
      <p>
        Địa Chỉ: Cụm Công Nghiệp Đại Tự, Xã Kim Chung, Huyện Hoài Đức, Hà Nội
      </p>
    </div>
  </section>
</template>

<style scoped>
.return-policy {
  padding: 40px;
  background-color: #f8f9fa;
}
.container {
  max-width: 800px;
  margin: auto;
  background: white;
  padding: 20px;
  border-radius: 8px;
  box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
}
h1,
h2 {
  color: #333;
}
ul,
ol {
  padding-left: 20px;
}
li {
  margin-bottom: 10px;
}
p {
  margin-bottom: 15px;
}
.clickable {
  color: #007bff;
  text-decoration: none;
  font-weight: bold;
}
.clickable:hover {
  text-decoration: underline;
}
.footer {
  text-align: center;
  margin-top: 40px;
  padding: 20px;
  background-color: #5a85d4;
  color: rgb(3, 3, 3);
  border-radius: 8px;
}
</style>
