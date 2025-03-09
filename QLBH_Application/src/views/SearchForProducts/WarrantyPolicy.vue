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
  <pageheader title="" pageTitle="Chính sách bảo hành" />
  <section class="warranty-policy">
    <div class="container">

      <h1>CHÍNH SÁCH BẢO HÀNH</h1>
      <p>
        Coolmax Việt Nam cam kết mang đến cho khách hàng dịch vụ bảo hành chuyên
        nghiệp và thuận tiện nhất.
      </p>

      <h2>1. Thời Gian Bảo Hành</h2>
      <p>
        Sản phẩm do Coolmax cung cấp được bảo hành 24 tháng kể từ ngày mua (ngày
        kích hoạt bảo hành) nhưng không quá 30 tháng kể từ ngày xuất kho (tùy
        điều kiện nào đến trước).
      </p>

      <h2>2. Điều Kiện Bảo Hành Miễn Phí</h2>
      <ul>
        <li>Sản phẩm còn trong thời hạn bảo hành.</li>
        <li>
          Sản phẩm đã được đăng ký bảo hành trên hệ thống của Coolmax hoặc căn
          cứ vào ngày xuất kho.
        </li>
        <li>Sản phẩm bị hư do lỗi kỹ thuật của nhà sản xuất.</li>
        <li>Sản phẩm được cung cấp bởi hệ thống phân phối của Coolmax.</li>
        <li>
          Số sê-ri máy, kiểu máy trên sản phẩm phải trùng khớp với phiếu bảo
          hành.
        </li>
        <li>
          Phiếu bảo hành phải còn nguyên vẹn, thể hiện rõ tên khách hàng và ngày
          mua.
        </li>
        <li>
          Trường hợp không có hóa đơn tài chính hoặc không đăng ký kích hoạt bảo
          hành điện tử: Thời hạn bảo hành là 30 tháng kể từ ngày sản xuất.
        </li>
        <li>
          Đảm bảo nguồn điện ổn định theo chính sách bảo hành của Coolmax.
        </li>
      </ul>

      <h2>3. Điều Kiện Bảo Hành Tính Phí</h2>
      <ul>
        <li>Sản phẩm đã hết hạn bảo hành.</li>
        <li>
          Không có phiếu bảo hành hay chứng từ hóa đơn liên quan đến sản phẩm.
        </li>
        <li>
          Số sê-ri máy, kiểu máy trên sản phẩm không trùng khớp với phiếu bảo
          hành.
        </li>
        <li>
          Sản phẩm bị hư hỏng do thiên tai, lũ lụt, sét đánh, hỏa hoạn, vận
          chuyển làm nứt, móp, bể, trầy xước.
        </li>
        <li>
          Sản phẩm hư hỏng do sử dụng không đúng hướng dẫn, lắp đặt sai kỹ
          thuật, nguồn điện không ổn định.
        </li>
        <li>
          Sản phẩm có dấu hiệu đã tháo lắp, sửa chữa thay thế linh kiện tại nơi
          không thuộc hệ thống bảo hành của Coolmax.
        </li>
        <li>
          Các phụ kiện kèm theo sản phẩm không thuộc phạm vi bảo hành (bánh xe,
          khay, dây cắm điện, vỏ máy, tay nắm, núm vặn, v.v.).
        </li>
        <li>
          Các yêu cầu vệ sinh, bảo trì sản phẩm không thuộc phạm vi bảo hành.
        </li>
        <li>
          Để đảm bảo máy hoạt động hiệu quả, thực phẩm cần được làm nguội hoàn
          toàn trước khi cho vào tủ.
        </li>
      </ul>

      <h2>4. Quy trình bảo hành</h2>
      <ol>
        <li>
          Khách hàng mang sản phẩm đến trung tâm bảo hành hoặc liên hệ
          <a href="tel:0386125907" class="clickable">Hotline: 0386.125.907</a>
          để được hướng dẫn.
        </li>
        <li>Nhân viên kiểm tra tình trạng sản phẩm.</li>
        <li>Thời gian bảo hành tùy thuộc vào mức độ hư hỏng của sản phẩm.</li>
      </ol>

      <h2>5. Liên hệ</h2>
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
.warranty-policy {
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
