import { createApp } from 'vue';
import App from './App.vue';
import router from './router';
import store from "./state/store";
import BootstrapVueNext from 'bootstrap-vue-next';
import VueApexCharts from "vue3-apexcharts";
import PhosphorIcons from "@phosphor-icons/vue";
import Wizard from 'form-wizard-vue3'
import vuetify from './plugins/vuetify'
import { loadFonts } from './plugins/webfontloader'
import { createI18n } from 'vue-i18n';
import vi from 'vuetify/locale';

import 'bootstrap';

// CSS import
import 'bootstrap/dist/js/bootstrap.bundle';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue-next/dist/bootstrap-vue-next.css';
import '@vueform/slider/themes/default.css';

// Register components
import '@/assets/css/style.css';
import '@/assets/scss/style.scss';
import '@/assets/scss/style-preset.scss'
import '@/assets/scss/landing.scss'
import '@/assets/scss/uikit.scss'

import '@/assets/fonts/feather.css'
import '@/assets/fonts/material.css'
import '@/assets/fonts/fontawesome.css'
import '@/assets/fonts/tabler-icons.min.css'
// //plugin css
import '@/assets/css/plugins/animate.min.css'
import '@/assets/css/plugins/swiper-bundle.css'
import 'form-wizard-vue3/dist/form-wizard-vue3.css'
// css
import '@/assets/css/style.css'
import '@/assets/css/landing.css'
import '@/assets/css/uikit.css'
import axios from '@/plugins/axios';
import notification from "./plugins/Confignotification";
import commonPlugin from "./plugins/common";

loadFonts()
const i18n = createI18n({
    locale: 'vi',  // Ngôn ngữ mặc định là tiếng Việt
    messages: {
        vi,  // Sử dụng locale tiếng Việt của Vuetify
    },
    });
createApp(App)
    .use(store)
    .use(router)
    .use(BootstrapVueNext)
    .use(vuetify)
    .use(commonPlugin)
    .use(axios)
    .use(notification)
    .use(VueApexCharts)
    .use(PhosphorIcons)
    .use(i18n) // Thêm i18n vào ứng dụng
    .component('Wizard', Wizard)
    .mixin({
        data() {
          return {
            rules: {
              required: v => !!v || 'Vui lòng không để trống',
              validEmail: v => /^[a-zA-Z0-9._%+-]+@gmail\.com$/.test(v) || 'Email phải hợp lệ và kết thúc bằng @gmail.com',
              validPassword: v => /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/.test(v) || 'Mật khẩu phải có ít nhất 8 ký tự, bao gồm ít nhất 1 chữ cái và 1 chữ số',
              matchPassword: (v) => v === this.changePassword.passwordNew || 'Mật khẩu xác nhận không khớp',
            }
          }
        }
      })
    .mount('#app')
store.dispatch('autoLogin');
