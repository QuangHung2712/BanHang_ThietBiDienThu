<script>

export default {
    name: "LOGIN",
    components: {
    }
}
</script>

<template>
    <div class="auth-main v1">
        <div class="auth-wrapper">
            <div class="auth-form">
                <div class="error-card">
                    <div class="card-body">
                        <div class="error-image-block">
                            <img class="img-fluid" src="images/img-error-404.png" alt="img">
                        </div>
                        <div class="text-center">
                            <h1 class="mt-2">Không thể kết nối với Server</h1>
                            <p class="mt-2 mb-4 text-muted f-20">Trang web đang không thể kết nối được với Server. Vui lòng liên hệ Admin để sửa lỗi</p>
                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</template>