using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        T Get(Expression<Func<T, bool>> filter);
        T Get(Expression<Func<T, bool>> filter, string includedProperties);
        List<T> GetList(Expression<Func<T, bool>> filter = null);
        List<T> GetList(Expression<Func<T, bool>> filter, string includedProperties);
        T Add(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}
