<style scoped>
    .custom-table {
        height: 290px; /* Giới hạn chiều cao tối đa */
        overflow-y: auto; /* Hiển thị thanh cuộn dọc nếu vượt quá */
    }
    .custom-table thead tr {
        position: sticky !important; /* Giữ cố định header */
        top: 0; /* Cố định ở đỉnh */
        z-index: 1100; /* Đảm bảo header nằm trên nội dung */
        background-color: white; /* Màu nền cho header */
    }
</style>
<script>
    import pageheader from "@/components/page-header.vue"


    export default {
        name: "HOMEPAGE",
        components: {
            pageheader
        },
        data(){
            return{
            }
        },
        created(){
        },
        methods:{
        }
}
</script>

<template>
    <pageheader title="" pageTitle="Trang chủ" />
    <!-- <BRow>
        <v-snackbar
            v-model="snackbar"
            :timeout="10000"
            class="custom-snackbar"
            :color="snackbarColor"
        >
            <h5 class="text-center">{{ message }}</h5>
        </v-snackbar>
        <BCol class="col-sm-4">
            <BCard class="statistics-card-1" no-body>
                <BCardHeader class="d-flex align-items-center justify-content-between py-2">
                    <h5>Số phòng đã thanh toán</h5>
                </BCardHeader>
                <BCardBody class="p-3">
                    <div class="d-flex align-items-center">
                        <div class="avtar bg-brand-color-1 text-white me-3">
                            <i class="ph-duotone ph-currency-dollar f-26"></i>
                        </div>
                        <h3 class="f-w-300 d-flex align-items-center m-b-0">{{ dataInfo.roomUnpaid }}</h3>
                    </div>
                    <p class="text-muted mb-2 text-sm mt-3">{{dataInfo.infoRoomUnpaid ? 'Các phòng đã thanh toán: ' + dataInfo.infoRoomUnpaid : 'Không có phòng nào đã thanh toán'}}</p>
                    <div class="progress" style="height: 7px">
                        <div class="progress-bar bg-brand-color-3" role="progressbar" style="width: 75%" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100"></div>
                    </div>
                </BCardBody>
            </BCard>
        </BCol>
        <BCol class="col-sm-4">
            <BCard class="statistics-card-1" no-body>
                <BCardHeader class="d-flex align-items-center justify-content-between py-2">
                    <h5>Số phòng chưa thanh toán</h5>
                </BCardHeader>
                <BCardBody class="p-3">
                    <div class="d-flex align-items-center">
                        <div class="avtar bg-brand-color-1 text-white me-3">
                            <i class="ph-duotone ph-currency-dollar f-26"></i>
                        </div>
                        <h3 class="f-w-300 d-flex align-items-center m-b-0">{{ dataInfo.roomPaid }}</h3>
                    </div>
                    <p class="text-muted mb-2 text-sm mt-3">{{dataInfo.infoRoomPaid ? 'Các phòng chưa thanh toán: ' + dataInfo.infoRoomPaid : 'Không có phòng nào chưa thanh toán'}}</p>
                    <div class="progress" style="height: 7px">
                        <div class="progress-bar bg-brand-color-3" role="progressbar" style="width: 75%" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100"></div>
                    </div>
                </BCardBody>
            </BCard>
        </BCol>
        <BCol class="col-sm-4">
            <BCard class="statistics-card-1" no-body>
                <BCardHeader class="d-flex align-items-center justify-content-between py-2">
                    <h5>Số phòng chưa có hoá đơn</h5>
                </BCardHeader>
                <BCardBody class="p-3">
                    <div class="d-flex align-items-center">
                        <div class="avtar bg-brand-color-1 text-white me-3">
                            <i class="ph-duotone ph-currency-dollar f-26"></i>
                        </div>
                        <h3 class="f-w-300 d-flex align-items-center m-b-0">{{ dataInfo.roomNoInvoice }}</h3>
                    </div>
                    <p class="text-muted mb-2 text-sm mt-3">{{dataInfo.infoRoomNoInvoice ? 'Các phòng chưa có hoá đơn: ' + dataInfo.infoRoomNoInvoice : 'Không có phòng nào chưa có hoá đơn'}}</p>
                    <div class="progress" style="height: 7px">
                        <div class="progress-bar bg-brand-color-3" role="progressbar" style="width: 75%" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100"></div>
                    </div>
                </BCardBody>
            </BCard>
        </BCol>
        <BCol class="col-sm-4">
            <div class="card statistics-card-1">
                <div class="card-body">
                    <div class="d-flex align-items-center">
                        <div class="avtar bg-brand-color-1 text-white me-3">
                            <i class="ph-duotone ph-currency-dollar f-26"></i>
                        </div>
                        <div>
                            <p class="text-muted mb-0">Số phòng còn trống</p>
                            <div class="d-flex align-items-end">
                                <h2 class="mb-0 f-w-500">{{ dataInfo.roomAvailable }}</h2>
                                <span class="badge bg-light-danger ms-2">{{dataInfo.infoRoomAvailable ? 'Phòng: ' + dataInfo.infoRoomAvailable : 'Không có phòng nào còn trống'}}</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card statistics-card-1">
                <div class="card-body">
                    <div class="d-flex align-items-center">
                        <div class="avtar bg-brand-color-1 text-white me-3">
                            <i class="ph-duotone ph-currency-dollar f-26"></i>
                        </div>
                        <div>
                            <p class="text-muted mb-0">Số phòng sắp hết hạn hợp đồng</p>
                            <div class="d-flex align-items-end">
                                <h2 class="mb-0 f-w-500">{{ dataInfo.roomExpireContract }}</h2>
                                <span class="badge bg-light-warning ms-2">{{dataInfo.infoRoomExpireContract ? 'Phòng: ' + dataInfo.infoRoomExpireContract : 'Không có phòng nào'}}</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card statistics-card-1">
                <div class="card-body">
                    <div class="d-flex align-items-center">
                        <div class="avtar bg-brand-color-1 text-white me-3">
                            <i class="ph-duotone ph-currency-dollar f-26"></i>
                        </div>
                        <div>
                            <p class="text-muted mb-0">Tổng số tiền đã thu được</p>
                            <div class="d-flex align-items-end">
                                <h2 class="mb-0 f-w-500">{{ formatPrice(dataInfo.totalAmount) }} <small class="text-muted">VND</small></h2>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </BCol>
        <BCol class="col-sm-8">
            <BCard>
                <BCardHeader class="p-0">
                    <h4>Các phòng đang chờ xác nhận thanh toán</h4>
                </BCardHeader>
                <BCardBody class="p-0">
                    <v-data-table 
                        :headers = "headersTable"
                        :items="RequestPaymentConfirmation"
                        class="border-sm rounded-lg custom-table"
                        hide-default-footer>
                        <template v-slot:[`item.soTien`]="{ item }">
                                {{ FormatPrice(item.soTien) }}
                            </template>
                        <template v-slot:[`item.actions`]="{ item }">
                            <button class="btn btn-sm btn-link-danger" @click="Refuse(item.id)">Từ chối</button>
                            <button class="btn btn-sm btn-primary ms-1" @click="Received(item.id)">Đã nhận</button>
                        </template>
                    </v-data-table>
                </BCardBody>
            </BCard>
        </BCol>
    </BRow> -->
</template>
