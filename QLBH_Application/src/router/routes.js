export default [
    {
        path: '/manage',
        name: "manage",
        meta: { title: "Trang quản lý",requiresAuth: true },
        component: () => import("../views/Manage/Manage.vue"),
        redirect: { name: "homepage" },
        children:[
            {
                path: "homepage",
                name: "homepage",
                meta: { title: "Trang chủ",requiresAuth: true },
                component: () => import("../views/Manage/Page/HomePage.vue"),  
            },
            {
                path: "product",
                name: "product",
                meta: { title: "Trang chủ",requiresAuth: true },
                component: () => import("../views/Manage/Page/Product.vue"),  
            },
            {
                path: "producttype",
                name: "producttype",
                meta: { title: "Loại sản phẩm",requiresAuth: true },
                component: () => import("../views/Manage/Page/ProductType.vue"),  
            },
            {
                path: "order",
                name: "order",
                meta: { title: "Chờ tư vấn",requiresAuth: true },
                component: () => import("../views/Manage/Page/Order.vue"),  
            },
        ]
    },
    {
        path: "/login",
        name: "login",
        meta: { title: "Đăng nhập" },
        component: () => import("../views/Manage/Auth/Login.vue"),
    },
    {
        path: "/register",
        name: "register",
        meta: { title: "Đăng ký" },
        component: () => import("../views/Manage/Auth/register.vue"),
    },
    {
        path: "/forgot",
        name: "forgot",
        meta: { title: "Quên mật khẩu" },
        component: () => import("../views/Manage/Auth/forgot-password.vue"),
    },
    {
        path: "/verification",
        name: "verification",
        meta: { title: "Quên mật khẩu" },
        component: () => import("../views/Manage/Auth/code-verification.vue"),
    },
    {
        path: "/error",
        name: "error",
        meta: { title: "Lỗi kết nối" },
        component: () => import("../views/Error.vue"),
    },
    {
        path: "/",
        name: "Home",
        meta: { title: "Trang chủ" },
        component: () => import("../views/SearchForProducts/index.vue"),
        children: [
            {
                path: "/",
                name: "FindARoom",
                meta: { title: "Thiết bị nhà hàng An Chi" },
                component: () => import("../views/SearchForProducts/HomePage.vue"),
            },
            {
                path: "lstproduct",
                name: "lstproduct",
                meta: { title: "Danh sách sản phẩm" },
                component: () => import("../views/SearchForProducts/ListProduct.vue"),
            },
            {
                path: "introduce",
                name: "introduce",
                meta: { title: "Giới thiệu" },
                component: () => import("../views/SearchForProducts/Introduce.vue"),
            },
            {
                path: "contact",
                name: "contact",
                meta: { title: "Liên hệ" },
                component: () => import("../views/SearchForProducts/Contact.vue"),
            },
            {
                path: "detail/:productId",
                name: "detail",
                meta: { title: "Chi tiết sản phẩm" },
                component: () => import("../views/SearchForProducts/ProductDetail.vue"),
            },
        ]
    },
]