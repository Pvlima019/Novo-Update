<template>
  <div>
   <h1> {{titulo}}</h1>

   
    <div>
      <h2>Ativo</h2>
          <select v-model="FkIdAtivo">
             
        <option v-for="(ativo, index) in ativos" :key="index" v-bind:value="index+1">
          {{ativo.descricao}}
        </option>
        
      </select>
  </div>
      <div>
        <h2>Quantidade</h2>
        <input placeholder="Quantidade" v-model="Quantidade" number />
      </div>
      
      <div>
        <h2>Preço</h2>
        <input placeholder="Preco" v-model="Preco" number />
      </div>

        <div>
        <h2>Data</h2>
        <input type="date" :value="myDate && myDate.toISOString().split('T')[0]"
                     @input="myDate = $event.target.valueAsDate">

      </div>
      

      <div>
        <h2>Classe_Negociacao</h2>
        <input placeholder="Classe_Negociacao" type="text" v-model="ClasseNegociacao" />
      </div>



      <div>
        <h2></h2>
        <input type="submit" value="Submit" v-on:click="submit" />
      </div> 
  </div>
</template>


<script>
export default {
  data(){
    return{
      titulo:'Cadastro de Ordens',
      FkIdAtivo:"1",
      Quantidade:"",
      Preco:"",
      ClasseNegociacao:"",
      ordens: [],
      ativos:[],
      myDate:''

    }

  }, 
  created() {
    
      this.$http
        .get("http://localhost:5000/api/ordens")
        .then(res => res.json())
        .then(ordens => (this.ordens = ordens));
         this.$http
        .get("http://localhost:5000/api/ativos")
        .then(res => res.json())
        .then(ativos => (this.ativos = ativos));
  },
  
  props: {},
  
  methods: {
    submit() {
      
      let _ordem ={
        FkIdAtivo: this.FkIdAtivo,
         Quantidade: this.Quantidade,
          Preco: this.Preco,
           Data: this.myDate,
            ClasseNegociacao: this.ClasseNegociacao
      }

     this.$http
        .post("http://localhost:5000/api/ordens",_ordem)
        .then(res => res.json())
        .then(ordem => {
          this.ordens.push(ordem);
          
        });
        this.fkIdAtivo = "";
      
    }
  },
    
  
}
</script>

<style lang="scss" scoped>

</style>
