using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.models;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class OrdensController : Controller
    {

        private readonly DB_PauloVictorContext _context;

 
        public IRepositorio _repo { get; }
        public OrdensController(IRepositorio repo, DB_PauloVictorContext context)
        {
            _context = context;
            _repo = repo;

        }

        //Metodo Get com passagem de paramentro recebe Data e retorna todas as ordens de uma data

        [HttpGet("{data}")]
        public IEnumerable<Join> Get(DateTime data)
        {
            var actions = (from ordem in _context.Ordem
                           join ativo in _context.Ativo on ordem.FkIdAtivo equals ativo.IdAtivo
                           where ordem.Data == data
                           select new
                           {
                               descricao = ativo.Descricao,
                               quantidade = ordem.Quantidade,
                               preco = ordem.Preco,
                               data = ordem.Data,
                               classeNegociacao = ordem.ClasseNegociacao
                           }).ToList().Select(c => new Join
                           {
                               Descricao = c.descricao,
                               Quantidade = c.quantidade,
                               Preco = c.preco,
                               Data = c.data,
                               ClasseNegociacao = c.classeNegociacao
                           });
            return actions;
        }

        //Metodo get retorna a posicao dos ativos (
        [HttpGet]
        public IEnumerable<Posicao> GetActions()
        {
            //É feito o join das tabelas
            var actions = (from ordem in _context.Ordem
                           join ativo in _context.Ativo on ordem.FkIdAtivo equals ativo.IdAtivo
                           select new
                           {
                               descricao = ativo.Descricao,
                               quantidade = ordem.Quantidade,
                           })
                           .GroupBy(g=>g.descricao) //É feito um agrupamento pela descricao
                           .Select(c => new Posicao
                           {
                               desc_ativo = c.Key,              //Um objeto Posicao é criado contendo o key do groupb (GroupBy)
                               qtd = c.Sum(g => g.quantidade),  //E o total das quantidades de cada ativo

                           });
            return actions;
        }

        //Metodo post recebe um modelo do tipo Ordem e adiciona no banco de dados essa nova ordem
        [HttpPost]
        public async Task<IActionResult> Post(Ordem model)
        {
            //Verifica se a quantidade é multipla do Lote minimo
            Ativo query =                                                      
                    (from ativo in _context.Ativo
                     where ativo.IdAtivo == model.FkIdAtivo
                     select ativo)
                    .First();

            if (model.Quantidade%query.LoteMinimo!=0)
                {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "A quantidade deve ser multipla do lote minimo");

            }

//Verifica se a ClasseNegociacao eh "C" ou "V" desconsiderando diferenca entre maiusculas e minusculas
            if (String.Compare(model.ClasseNegociacao, "C", true) != 0)
                {
                    if (String.Compare(model.ClasseNegociacao, "V", true) != 0)
                    {
                    return this.StatusCode(StatusCodes.Status500InternalServerError, "Classe negociacao deve ser C- Compra ou V venda");
                    }
                    else
                        {
                             model.Quantidade = model.Quantidade * (-1);
                        }
                }
            
            try
            {
                _repo.Add(model);

                if(await _repo.SaveChangesAsync())
                    {
                        return Created($"/api/Ordens/{model.IdOrdem}",model);
                    }
              
            }

            catch (System.Exception)
            {
                    return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados falhou");
            }

            return BadRequest();

        }


    }
}