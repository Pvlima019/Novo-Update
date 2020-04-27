using System;
using System.Collections.Generic;

//O Model ativo foi obtido atraves de engenharia reversa com o banco de dados ja existente
namespace WebApi.models
{
    public partial class Ativo
    {
         public int IdAtivo { get; set; }
        public string Descricao { get; set; }
        public int LoteMinimo { get; set; }

    }
}
