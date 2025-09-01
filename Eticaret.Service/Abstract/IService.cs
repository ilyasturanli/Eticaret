using Eticaret.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Eticaret.Service.Abstract
{
    public interface IService<T> where T : class,IEntity,new()
    {
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T,bool>> expression);
        IQueryable<T> GetQueryable();
        T Get(Expression<Func<T,bool>> expression);
        T Find(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        int SaveChanges();
        //Asenkron metotlar
        Task<List<T>> GetAllAsync();// tüm liste...
        Task <List<T>> GetAllAsync(Expression<Func<T, bool>> expression); // sorgulanabilir liste...
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task<T> FindAsync(int id);
        Task AddAsync(T entity);
        //Task UpdateAsync(T entity);
        //Task DeleteAsync(T entity);
        Task<int> SaveChangesAsync();

    }
}
