using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Conecta2.DAL.Repositories.Contract
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        //task dice que es una tarea asincrona
        //TModel es lo que va a devolver por ejemplo un post un user una categoria x
        //Get el nombre del metodo
        //Expression convierte lo que se le pase a una consulta sql 
        //func <tmodel,bool> tmodel es lo que entra y bool el resultado si lo encontro o no
        //Filter el nombre que recibe el parametro
        Task<TModel> Get(Expression<Func<TModel, bool>> filter);
        Task<TModel> Create(TModel model);
        Task<bool> Update(TModel model);
        Task<bool> Delete(TModel model);
        Task<bool> SoftDelete(TModel model);
        Task<bool> RemoveRange(IEnumerable<TModel> entities);
        Task<bool> AddRange(IEnumerable<TModel> entities);
        Task<IQueryable<TModel>> Query(Expression<Func<TModel, bool>> filter = null);
        
    }
}
