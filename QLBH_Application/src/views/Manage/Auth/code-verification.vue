<script>
import Rightbar from "@/components/right-bar.vue"

export default {
    name: "CODE-VERIFICATION",
    components: {
        Rightbar
    }
}
</script>

<template>
    <div class="auth-main v2">
        <div class="bg-overlay bg-dark"></div>
        <div class="auth-wrapper">
            <div class="auth-sidecontent">

                <div class="auth-sidefooter">
                    <h4 class="text-white">QUẢN LÝ NHÀ TRỌ</h4>
                    <!-- <img src="@/assets/images/logo-white.svg" class="img-brand img-fluid" alt="images"> -->
                    <hr class="mb-3 mt-4">
                    <!-- <BRow class="row">
                        <BCol class="col my-1">
                            <p class="m-0">Light Able ♥ crafted by Team <a href="#"
                                    target="_blank"> themes</a></p>
                        </BCol>
                        <BCol class="col-auto my-1">
                            <ul class="list-inline footer-link mb-0">
                                <li class="list-inline-item"><router-link to="/dashboard">Home</router-link></li>
                                <li class="list-inline-item"><a href="#"
                                        target="_blank">Documentation</a></li>
                                <li class="list-inline-item"><a href="#"
                                        target="_blank">Support</a></li>
                            </ul>
                        </BCol>
                    </BRow> -->
                </div>
            </div>
            <div class="auth-form">
                <div class="card my-5 mx-3">
                    <div class="card-body">
                        <h4 class="f-w-500 mb-1">Xác nhận bằng OTP</h4>
                        <p class="mb-0">Chúng tôi đã gửi mã đến Email: qua****qn@gmail.com</p>
                        <p class="mb-3">Không nhận được Email?<a href="#" class="link-primary ms-1">Gửi lại mã</a></p>
                        <BRow class="row my-4 text-center">
                            <BCol class="col">
                                <input type="text" class="form-control text-center" placeholder="0">
                            </BCol>
                            <BCol class="col">
                                <input type="text" class="form-control text-center" placeholder="0">
                            </BCol>
                            <BCol class="col">
                                <input type="text" class="form-control text-center" placeholder="0">
                            </BCol>
                            <BCol class="col">
                                <input type="text" class="form-control text-center" placeholder="0">
                            </BCol>
                            <BCol class="col">
                                <input type="text" class="form-control text-center" placeholder="0">
                            </BCol>
                            <BCol class="col">
                                <input type="text" class="form-control text-center" placeholder="0">
                            </BCol>
                        </BRow>
                        <div class="d-grid mt-4">
                            <button type="button" class="btn btn-primary">Tiếp tục</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <Rightbar />
</template>