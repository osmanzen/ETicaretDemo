using Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    public abstract class EFEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity
        where TContext : DbContext
    {

        //Constructor Injection
        #region Constructor Injection

        private DbContext _db;

        public EFEntityRepositoryBase(DbContext db)
        {
            _db = db;
        }

        #endregion

        public void Add(TEntity entity)
        {
            //db.categories.Add(entity)
            //db.products.Add(entity)
            //db.SaveChanges();

            _db.Set<TEntity>().Add(entity);
            _db.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            _db.Set<TEntity>().Remove(entity);
            _db.SaveChanges();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ?
                  _db.Set<TEntity>().FirstOrDefault()
           : _db.Set<TEntity>().FirstOrDefault(filter);
        }

        public ICollection<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ?
                _db.Set<TEntity>().ToList()
                : _db.Set<TEntity>().Where(filter).ToList();
        }

        public void Update(TEntity entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
