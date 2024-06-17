import IndexComponent from "./components/IndexComponent.vue";
import RuletaComponent from "./components/RuletaComponent.vue";
import { createRouter, createWebHistory } from 'vue-router';

export const routes = [
    {path: '/', component: IndexComponent},
    {path: '/Ruleta/:nombre/:puntos', component: RuletaComponent}
];

const router = createRouter({
    history: createWebHistory(),
    routes,
  });
  
  export default router;