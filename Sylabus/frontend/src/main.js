import { createApp } from 'vue';
import App from './App.vue';
import './main.css';  // Załaduj główny plik CSS
import 'bootstrap/dist/css/bootstrap.css';  // Załaduj Bootstrap CSS
import { BootstrapVue3, IconsPlugin } from 'bootstrap-vue-3';  // Używamy BootstrapVue3 dla Vue 3


const app = createApp(App);


app.use(BootstrapVue3);
app.use(IconsPlugin);


app.mount('#app');