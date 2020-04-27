import Vue from 'vue';
import Router from 'vue-router';
import Cadastro from './components/Cadastro.vue'
import Relatorios from './components/Relatorios.vue'


Vue.use(Router);


export default new Router({
    routes:[
        {
                path: '/cadastro',
                nome: 'Cadastro',
                component: Cadastro
            },
            {
                path: '/relatorios',
                nome: 'Relatorios',
                component: Relatorios
            }
    ]
})