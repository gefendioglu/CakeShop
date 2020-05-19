using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.Service
{
    public interface IBaseService<T> where T : class, new()
    {
        IDataResult<List<T>> GetAll();
        IDataResult<List<T>> GetActive();
        IDataResult<List<T>> GetDefault(Expression<Func<T, bool>> exp);
        IDataResult<T> GetById(int id);
        IDataResult<T> GetByDefault(Expression<Func<T, bool>> exp);
        IDataResult<T> Add(T model);
        IDataResult<T> Update(T model);
        IDataResult<T> HardDelete(T model);
        IDataResult<T> SoftDelete(T model);
        IDataResult<List<T>> DeleteAll(Expression<Func<T, bool>> exp);


        //IDataResult<T> HardDelete(int id);
        //IDataResult<T> HardDelete(List<T> model);
        //IDataResult<T> SoftDelete(int id);
        //IDataResult<T> SoftDelete(List<T> model);
        //IDataResult<List<T>> Add(List<T> model);
        //IDataResult<T> Any(Expression<Func<T, bool>> exp);
        //bool Any(Expression<Func<T, bool>> exp);
        //IDataResult<T> Save();
        //int Save();

    }
}
