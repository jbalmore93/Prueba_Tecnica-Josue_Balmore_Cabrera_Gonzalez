import { createApp } from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify'
import { loadFonts } from './plugins/webfontloader'
import 'bootstrap/dist/css/bootstrap.css'
import './assets/css/style.css'
import router from './route'
loadFonts()

createApp(App)
  .use(router)
  .use(vuetify)
  .mount('#app')
