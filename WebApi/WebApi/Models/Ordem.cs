using System;
using System.Collections.Generic;

//O Model Ordem foi obtido atraves de engenharia reversa com o banco de dados ja existente
namespace WebApi.models
{
    public partial class Ordem
    {
        public int IdOrdem { get; set; }
        public int FkIdAtivo { get; set; }
        public double Quantidade { get; set; }
        public double Preco { get; set; }
        public DateTime Data { get; set; }
        public string ClasseNegociacao { get; set; }



      
    }
}
