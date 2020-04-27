<template>
  <div id="app" >
    <Nav></Nav>
    <dir class="marginPrincipal">
      <router-view></router-view>
    </dir>
  </div>
</template>

<script>
//import Cadastro from './components/Cadastro.vue'
//import Relatorios from './components/Relatorios.vue'
import Nav  from   './components/_nav/Nav'


export default {
  name: 'app',
  components: {
  // Cadastro,
    //Relatorios,
    Nav

    
  }
}
</script>

<style>
.marginPrincipal{
  width: 50%;
  margin:auto;

}
#app{
  width: 100%;
  
}


</style>
