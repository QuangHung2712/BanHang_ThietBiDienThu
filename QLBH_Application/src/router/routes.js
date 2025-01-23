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
        component: () => import("../views/FindRoom/index.vue"),
        children: [
            {
                path: "/",
                name: "FindARoom",
                meta: { title: "Tìm kiếm nhà trọ" },
                component: () => import("../views/FindRoom/FindRoom.vue"),
            },
            {
                path: "/findpeople",
                name: "findpeople",
                meta: { title: "Tìm kiếm người ở ghép" },
                component: () => import("../views/FindRoom/FindPeople.vue"),
            },
            {
                path: "/detail/:idroom",
                name: "detail",
                meta: { title: "Chi tiết phòng trọ" },
                component: () => import("../views/FindRoom/DetailRoom.vue"),
            },
            {
                path: "/save",
                name: "save",
                meta: { title: "Phòng, bài đăng đã lưu" },
                component: () => import("../views/FindRoom/Save.vue"),
            },
            {
                path: "/editpost/:idpost",
                name: "editpost",
                meta: { title: "Đăng bài" },
                component: () => import("../views/FindRoom/Post.vue"),
            },
            {
                path: "/listPost",
                name: "listPost",
                meta: { title: "Quản lý bài đăng" },
                component: () => import("../views/FindRoom/ManagePosts..vue"),
            },
            {
                path: "/detailpost/:idpost",
                name: "detailpost",
                meta: { title: "Chi tiết bài đăng" },
                component: () => import("../views/FindRoom/DetailPost.vue"),
            },
        ]
    },
]