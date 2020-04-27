using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.models;
using WebApi.Models;

namespace WebApi.Data
{
    public interface IRepositorio
    {
         //Geral
        void Add<T>(T entity) where T: class;
        void Update<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        

        Task<bool> SaveChangesAsync();


        //Ativo

        
      

        //Get um Ativo por Id especificado
        Task<Ativo> GetAtivoAsyncById(int id_ativo);
        
        //Get Todos os Ativos
         Task<Ativo[]> GetAllAtivosAsync();




        //Ordem
        //Get todas as ordens
  
        Task<Ordem[]> GetAllOrdensAsync(DateTime data);
      


         




    }
}