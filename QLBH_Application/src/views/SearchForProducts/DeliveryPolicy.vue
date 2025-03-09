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
    <pageheader title="" pageTitle="Chính sách giao hàng" />
  <section class="shipping-policy">
    <div class="container">
      <h1>CHÍNH SÁCH GIAO HÀNG</h1>
      <p>
        Coolmax Việt Nam cam kết cung cấp dịch vụ giao hàng toàn quốc nhanh
        chóng và an toàn cho các sản phẩm có kích thước lớn như tủ lạnh, tủ
        đông. Dưới đây là thông tin chi tiết về chính sách giao hàng của chúng
        tôi.
      </p>

      <h2>1. Khu vực giao hàng</h2>
      <ul>
        <li>Chúng tôi hỗ trợ giao hàng trên toàn quốc.</li>
        <li>
          Giao hàng trực tiếp tại các tỉnh/thành phố lớn hoặc thông qua đơn vị
          vận chuyển.
        </li>
        <li>
          Những khu vực ngoại thành, vùng sâu vùng xa có thể phát sinh phụ phí
          vận chuyển.
        </li>
      </ul>

      <h2>2. Thời gian giao hàng</h2>
      <ul>
        <li>
          Khu vực nội thành Hà Nội : Giao trong ngày nếu đặt hàng trước 12h
          trưa.
        </li>
        <li>
          Khu vực khác: Thời gian giao hàng tùy theo đơn vị vận chuyển, thường
          từ 1-3 ngày làm việc.
        </li>
        <li>
          Thời gian giao hàng có thể thay đổi do điều kiện thời tiết hoặc tình
          hình vận chuyển.
        </li>
      </ul>

      <h2>3. Phí vận chuyển</h2>
      <ul>
        <li>
          Miễn phí giao hàng trong <strong>nội thành Hà Nội</strong> trong điều
          kiện tốt nhất cho khách hàng
        </li>
        <li>
          Đối với các khu vực xa hơn, phí giao hàng sẽ được thông báo khi đặt
          hàng.
        </li>
        <li>
          Coolmax hỗ trợ một phần chi phí vận chuyển tùy theo thông tin đơn
          hàng.
        </li>
        <li>
          Hàng hóa có kích thước lớn sẽ có phí bốc xếp nếu cần hỗ trợ vận chuyển
          vào tận nhà.
        </li>
      </ul>

      <h2>4. Điều kiện giao nhận hàng</h2>
      <ol>
        <li>
          Nhân viên giao hàng sẽ liên hệ trước khi giao để xác nhận thời gian
          nhận hàng.
        </li>
        <li>
          Khách hàng vui lòng kiểm tra sản phẩm trước khi ký nhận. Nếu có vấn
          đề, cần báo ngay cho
          <a href="tel:0386125907" class="clickable">Hotline: 0386.125.907</a>.
        </li>
        <li>
          Trong trường hợp khách hàng không nhận hàng theo lịch hẹn, phí giao
          lại có thể phát sinh.
        </li>
      </ol>

      <h2>5. Liên hệ hỗ trợ</h2>
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
.shipping-policy {
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
