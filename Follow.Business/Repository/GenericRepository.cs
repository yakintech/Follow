using Follow.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Follow.Business.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        internal FollowContext followContext;
        internal DbSet<T> dbSet;

        public GenericRepository()
        {
            this.followContext = new FollowContext();
            this.dbSet = followContext.Set<T>();
        }

        public List<T> GetAll()
        {
            return dbSet.Where(q => q.IsDeleted == false).OrderBy(x => x.CreatedDate).ToList();
        }

        public T GetByQuery(Func<T, bool> query)
        {
            return dbSet.FirstOrDefault(query);
        }


        public List<T> GetAllWithIncludes(string[] includes)
        {
            var query = dbSet.Where(q => q.IsDeleted == false).AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.ToList();
        }

        public T GetById(int id)
        {
            return dbSet.FirstOrDefault(x => x.Id == id && x.IsDeleted == false);
        }

        public T GetByIdWitIncludes(int id, string[] includes)
        {
            var query = dbSet.Where(q => q.Id == id && q.IsDeleted == false).AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.FirstOrDefault();
        }


        public T Create(T entity)
        {
            dbSet.Add(entity);
            followContext.SaveChanges();
            return entity;
        }


        public void Delete(int id)
        {
            T entity = dbSet.FirstOrDefault(x => x.Id == id && x.IsDeleted == false);

            if(entity != null)
            {
                entity.IsDeleted = true;
                followContext.SaveChanges();
            }
        }

        public T Update(T entity)
        {
            dbSet.Update(entity);
            followContext.SaveChanges();
            return entity;
        }
    }



}
