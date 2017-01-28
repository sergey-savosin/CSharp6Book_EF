using AutoLotDAL.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLotDAL.Repos
{
    public abstract class BaseRepo<T>: IDisposable where T: class//, new()
    {
        public AutoLotEntities Context { get; } = new AutoLotEntities();
        protected DbSet<T> Table;
        bool disposed = false;

        public BaseRepo()
        {
            //Context.Database.Log = Console.WriteLine;
        }
        internal int SaveChanges()
        {
            try
            {
                return Context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw;
            }
            catch (CommitFailedException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        internal async Task<int> SaveChangesAsync()
        {
            try
            {
                return await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw;
            }
            catch (DbUpdateException ex)
            {
                throw;
            }
            catch (CommitFailedException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int Save(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            return SaveChanges();
        }
        public Task<int> SaveAsync(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            return SaveChangesAsync();
        }

        public T GetOne(int? id) => Table.Find(id);
        public Task<T> GetOneAsync(int? id) => Table.FindAsync(id);
        public List<T> GetAll() => Table.ToList();
        public Task<List<T>> GetAllAsync() => Table.ToListAsync();

        public List<T> ExecuteQuery(string sql) => Table.SqlQuery(sql).ToList();
        public Task<List<T>> ExecuteQueryAsync(string sql) => Table.SqlQuery(sql).ToListAsync();
        public List<T> ExecuteQuery(string sql, object[] sqlParametersObjects)
                => Table.SqlQuery(sql, sqlParametersObjects).ToList();
        public Task<List<T>> ExecuteQueryAsync(string sql, object[] sqlParametersObjects)
                => Table.SqlQuery(sql, sqlParametersObjects).ToListAsync();

        public int Add(T entity)
        {
            Table.Add(entity);
            return SaveChanges();
        }
        public Task<int> AddAsync(T entity)
        {
            Table.Add(entity);
            return SaveChangesAsync();
        }
        public int AddRange(IList<T> entities)
        {
            Table.AddRange(entities);
            return SaveChanges();
        }
        public Task<int> AddRangeAsync(IList<T> entities)
        {
            Table.AddRange(entities);
            return SaveChangesAsync();
        }

        public int Delete (T entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            return SaveChanges();
        }
        public Task<int> DeleteAsync (T entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            return SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
            if (disposing)
            {
                Context.Dispose();
                // Free any managed objects here
                //
            }

            // Free any unmanaged objects here
            //
            disposed = true;
        }
    }

}
