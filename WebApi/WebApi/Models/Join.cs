using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Join
    {
        public string Descricao { get; set; }
        public double Quantidade { get; set; }
        public double Preco { get; set; }
        public DateTime Data { get; set; }
        public string ClasseNegociacao { get; set; }
    }
}
