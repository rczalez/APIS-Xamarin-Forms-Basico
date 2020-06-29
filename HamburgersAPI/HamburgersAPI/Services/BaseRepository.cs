using HamburgersAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HamburgersAPI.Services
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        internal HamburgersContext context;
        internal DbSet<TEntity> dbset;
        public BaseRepository(HamburgersContext context)
        {

            this.context = context;
            this.dbset = context.Set<TEntity>();

        }
        public void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbset.Attach(entityToDelete);

            }

            dbset.Remove(entityToDelete);
        }

        public void Delete(object id)
        {
            TEntity entitytoDelete = dbset.Find(id);
            Delete(entitytoDelete);
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbset;
            if(filter != null)
            {
               query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderby != null)
            {
                return orderby(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public TEntity GetById(object id)
        {
          return  dbset.Find(id);
        }

        public void Insert(TEntity entityToInsert)
        {
            dbset.Add(entityToInsert);
        }

        public void Update(TEntity entityToUpdate)
        {
            dbset.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
