using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Love.Discussion.Core.Interfaces
{

    public interface IRepository<T> where T : class
    {
        T Add(T obj);
        void Delete(int id);
        T Update(T obj);
        T Get(int id);
        IQueryable<T> Get();
        IQueryable<T> Get(Expression<Func<T, bool>> filter);
        IQueryable<T> Get(Expression<Func<T, bool>> filter, Expression<Func<T, object>> order = null, int? count = 0, int? skip = 0, bool reverse = false);
        void DisposeContext();
        DbContext GetDbContext();
        void SaveChanges();
    }
}
