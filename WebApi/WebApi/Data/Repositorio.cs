using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Data
{
    public class Repositorio : IRepositorio
    {
        //Lista de Ativos
        private List<Ativo> ativos = new List<Ativo>();

        //Lista de Ordens
        private List<Ordem> ordens = new List<Ordem>();

        private List<Posicao> posicoes = new List<Posicao>();


        public DB_PauloVictorContext _context { get; }
        public Repositorio(DB_PauloVictorContext context)
        {
            _context = context;

        }

        //Geral

        //Adiciona
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        //Atualiza
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        //Deleta
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        //Salva alteracoes assincronas
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }




        //Ativos
        //Get para Todos os Ativos

        public async Task<Ativo[]> GetAllAtivosAsync()
        {

            IQueryable<Ativo> query = _context.Ativo;

            query = query
                        .AsNoTracking()
                        .OrderBy(a => a.IdAtivo);
            return await query.ToArrayAsync();
        }


        //Get para um ativo com id Especificado
        public async Task<Ativo> GetAtivoAsyncById(int id_ativo)
        {
            IQueryable<Ativo> query = _context.Ativo;
            query = query
                                  .AsNoTracking()
                                  .OrderBy(a => a.IdAtivo)
                                  .Where(a => a.IdAtivo == id_ativo);



            return await query.FirstOrDefaultAsync();
        }


        //Ordens


        //Get para uma ordem
        /*  public async Task<Ordem[]> GetAllOrdensAsync()
          {
              IQueryable<Ordem> query = _context.Ordem;

              query = query
                          .AsNoTracking()
                          .OrderBy(a=>a.IdOrdem);
              return await query.ToArrayAsync();
          }
          */
        ////Get para uma ordem com id Especificado
        public async Task<Ordem> GetOrdemAsyncById(int id_ordem)
        {
            IQueryable<Ordem> query = _context.Ordem;

            query = query
                        .AsNoTracking()
                        .OrderBy(a => a.IdOrdem)
                        .Where(a => a.IdOrdem == id_ordem);

            return await query.FirstOrDefaultAsync();
        }

        public Task<Ordem[]> GetAllOrdensAsync(DateTime data)
        {
            throw new NotImplementedException();
        }

        public Task<Ordem[]> GetAllOrdensByIdAtivo(string descricao)
        {
            throw new NotImplementedException();
        }



        /* public async Task<Ordem[]> GetAllOrdensByIdAtivo(string classe_negociacao)
             {
                 IQueryable<Ordem> query = _context.Ordem;
                 query = query
                             .AsNoTracking()
                             .OrderBy(a=>a.FkIdAtivo)
                             .Where(a=>a.ClasseNegociacao==classe_negociacao);

                 return await query.ToArrayAsync();
             }




             /* public async Task<Ordem[]> GetAllOrdensByIdAtivo(string descricao)
              {
                 // Ativo meuAtivo=ativos.Find(a=> a.Descricao==descricao);

                  //IQueryable<Ordem> query = _context.Ordem;

                  //query = query
                    //          .AsNoTracking()
                      //        .OrderBy(a=>a.FkIdAtivo)
                        //      .Where(a=>a.FkIdAtivo==meuAtivo.IdAtivo);

                  //return await query.ToArrayAsync();
              }*/

        //public async Task<Ordem[]> GetAllOrdensByData(DateTime data)
        /* 
          {
               IQueryable<Ordem> queryOrdem = _context.Ordem;
              IQueryable<Ativo> queryAtivo = _context.Ativo;

               queryOrdem = queryOrdem
                         .AsNoTracking()
                        .OrderBy(a=>a.IdOrdem)
                     .Where(a=>a.Data==data);


              foreach(var order in queryOrde)
                  { }*/
        /* public IEnumerable<Join> GetAllOrdensAsync(DateTime data)

      var innerJoin = _context.Ordem.Join(// outer sequence 
                    _context.Ativo,  // inner sequence 
                     ordem => ordem.FkIdAtivo,    // outerKeySelector
                    ativo => ativo.IdAtivo,  // innerKeySelector
                    (ordem, ativo) => new  // result selector
                    {
                        descricao = ativo.Descricao,
                        quantidade = ordem.Quantidade,
                        preco = ordem.Preco,
                        data = ordem.Data,
                        classe_negociacao = ordem.ClasseNegociacao
                    }).Tos();

         */
        /*
        return await innerJoin.To();
    }
}
*/

        /*  var db = new DogDataContext(ConnectString);
          var result = from d in db.Dogs
                       join b in db.Breeds on d.BreedId equals b.BreedId
                       select new Tuple<Dog, Breed>(d, b);

          return result;



      }
      */
        /*
                public async Task<Ordem[]>Posicao()
                {
                    throw new NotImplementedException();
                }


            */










        /*
        public IEnumerable<Ativo> GetAllAtivos()
        {
            return ativos;
        }

        //Get para um Ativo com id Especificado
        public Ativo GetAtivo(int id_ativo)
        {
             return ativos.Find(p => p.IdAtivo == id_ativo);
        }




        //Ordens
         //Get para Todos as ordens
        public IEnumerable<Ordem> GetAllOrdens()
        {
            ordens.Sort((x, y) => y.Data.CompareTo(x.Data));
            return ordens;
        }

         //Get para uma ordem com id Especificado
        public Ordem GetOrdem(int id_ordem)
        {
             return ordens.Find(o => o.IdOrdem == id_ordem);
        }

        public List<Ordem> GetAllOrdensByIdAtivo(string descricao)
        {
            Ativo meuAtivo=ativos.Find(a=> a.Descricao==descricao);
            return ordens.FindAll(o=> o.FkIdAtivo==meuAtivo.IdAtivo);
        }

        public List<Ordem> GetAllOrdensByData(DateTime data)
        {
            return ordens
                        .Where(o=> o.Data==data)
                        .OrderBy(o=> o.FkIdAtivo)
                        .ToList();
        }

        public List<Posicao> Posicao()
        {         
                            
            
            foreach (Ativo meuAtivo in ativos)
            {   
                double qtdTotal=0; 
                List<Ordem>todasOrdensDoAtivo = ordens.FindAll(o=> o.FkIdAtivo==meuAtivo.IdAtivo);
                foreach (Ordem minhaOrdem in todasOrdensDoAtivo)
                {
                    qtdTotal+=minhaOrdem.Quantidade;
                }
                
                Posicao item = new Posicao(meuAtivo.Descricao,qtdTotal);
                posicoes.Add(item);
            }
                
            return posicoes;




*/


        /* 
          Task<IActionResult> Posicao()
         var query = from d in _context.Ativo 
              join f in _context.Ordem
              on d.IdAtivo equals f.FkIdAtivo
              select new {
                             d.Descricao,f.Quantidade
                          }
              into x group x by new {x.Descricao} into g
              select new{
                          Descricao = g.Key.Descricao,
                          Total=g.Select(x=>x.Quantidade).Count()
              }
              If (query == null)
              {return NotFound ( )
              }
              return View(query);
              }


              */
    }
}

