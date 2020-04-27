import moment from 'moment'

<template>
  <div>
   <h1> {{titulo}}</h1>

 <p><span>Relatorios:</span></p>
<select v-model="selected">
  <option disabled value="">Selecione um Relatório</option>
  <option>Relatório de Ordens</option>
  <option>Relatório de Posição</option>
</select>

<h2></h2>


<div v-if="selected === 'Relatório de Ordens'" > 
        <div>
       <h2>Data</h2>
        <input type="date" placeholder="Data para consulta" v-model="myDate" />
      
      <div>
        <h2></h2>
        <input type="submit" value="Submit" v-on:click="submit" />
      </div>
</div> 
<table border="1px" border-colapse=collapse>
  <tbody>
        <thead>
          <th colspan="1">Ativo</th>
          <th>Qtd</th>
          <th>Preco</th>
          <th>Data</th>
          <th>Classe_Negociacao</th>
        </thead>
        <tr v-for="(ordem, index) in ordens" :key="index" >
          <td >{{ordem.descricao}}</td>
          <td >{{ordem.quantidade}}</td>
          <td >{{ordem.preco}}</td>
          <td >{{ordem.data}}</td>
          <td >{{ordem.classeNegociacao}}</td>
          </tr>
    </tbody>
  </table>
</div>
<div v-else-if="selected === 'Relatório de Posição'" >
<table border="1px">
  <tbody>
    <thead>
      <th>Ativo</th>
      <th>Qtd</th>
    </thead>
    <tr v-for="(posicao, index) in posicoes" :key="index">
         <td>{{posicao.desc_ativo}}</td>
         <td>{{posicao.qtd}}</td>
    </tr>
  </tbody>
  </table>
  </div >

 
  </div>
</template>



<script>
export default {
  data(){
    return{
      titulo:'Meus Relatórios',
      selected: '',
      posicoes:[],
      ordens: [],
      myDate:''



    }

  }, 
  created() {
    
    
         this.$http
        .get("http://localhost:5000/api/ordens")
        .then(res => res.json())
        .then(posicoes => (this.posicoes = posicoes));
  },

  props: {},   

  methods: {
     
     submit()
      {
          this.$http
        .get(`http://localhost:5000/api/Ordens/${this.myDate}`)
        .then(res => res.json())
        .then(ordens => (this.ordens = ordens));

      }
  },
  
}
</script>

<style scoped>

</style>
