using Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    public class EFEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
          where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
       

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var db = new TContext())
            {
                var result = db.Set<TEntity>().FirstOrDefault(filter);
                return result;
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter, string includeProperties)
        {

            using (var db = new TContext())
            {
                var result = db.Set<TEntity>().Where(filter).AsQueryable();

                var relations = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var property in relations)
                {
                    result = result.Include(property);
                }
                return result.FirstOrDefault();
            }

        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var db = new TContext())
            {
                var result = filter == null
                    ? db.Set<TEntity>().ToList()
                    : db.Set<TEntity>().Where(filter).ToList();


                return result;
            }
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter, string includedProperties)
        {
            using (var db = new TContext())
            {
                var result = db.Set<TEntity>().Where(filter).AsQueryable();

                var relations = includedProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var property in relations)
                {
                    result = result.Include(property);
                }

                return result.ToList();
            }
        }

        public TEntity Add(TEntity entity)
        {
            using (var db = new TContext())
            {
                var result = db.Add(entity).Entity;
                db.Entry(entity).State = EntityState.Added;
                db.SaveChanges();
                return result;
            }

        }


        public TEntity Update(TEntity entity)
        {
            using (var db = new TContext())
            {
                var result = db.Update(entity).Entity;
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                return result;
            }
        }

        public TEntity Delete(TEntity entity)
        {
            using (var db = new TContext())
            {

                var result = db.Remove(entity).Entity;
                db.Entry(entity).State = EntityState.Deleted;
                db.SaveChanges();
                return result;
            }
        }

    }
}

























