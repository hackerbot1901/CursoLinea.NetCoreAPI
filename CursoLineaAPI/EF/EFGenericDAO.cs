using CursoLineaAPI.DAO;
using CursoLineaAPI.Modelos;
using Microsoft.EntityFrameworkCore;

namespace CursoLineaAPI.EF
{
    public class EFGenericDAO<T, ID> : IGenericDAO<T, ID> where T : class
    {
        private readonly Type _persistenceClass;
        protected DbContext dbContext;
        private readonly DbSet<T> _dbSet;


        public EFGenericDAO(Type persistenceClass)
        {
            _persistenceClass = persistenceClass ?? throw new ArgumentNullException(nameof(persistenceClass));
            dbContext = new CursoLineaContext();
            _dbSet = dbContext.Set<T>();
        }

        public void Create(T entity)
        {
            using var transaction = dbContext.Database.BeginTransaction();
            try
            {
                _dbSet.Add(entity);
                dbContext.SaveChanges();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public T Read(ID id)
        {
            return _dbSet.Find(id);
        }

        public void Update(T entity)
        {
            using var transaction = dbContext.Database.BeginTransaction();
            {
                try
                {
                    dbContext.Entry(entity).State = EntityState.Modified;
                    dbContext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Delete(T entity)
        {
            using var transaction = dbContext.Database.BeginTransaction();
            {
                try
                {
                    _dbSet.Remove(entity);
                    dbContext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

    }
}
