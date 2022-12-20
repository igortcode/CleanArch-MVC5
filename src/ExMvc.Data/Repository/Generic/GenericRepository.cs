using Dapper.Contrib.Extensions;
using ExMvc.Data.Context;
using ExMvc.Domain.Entities;
using ExMvc.Domain.Interfaces.Options;
using ExMvc.Domain.Interfaces.Repository.Generic;
using ExMvc.Domain.Settings;
using System;
using System.Data;
using System.Linq;

namespace ExMvc.Data.Repository.Generic
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Entity
    {
        protected IDbConnection DbConnection { get; private set; }
        private readonly DbSettings _dbSettings;

        public GenericRepository(IOptions<DbSettings> settings)
        {
            _dbSettings = settings.Value;
            DbConnection = new DbContext()
                .SetStrategy(_dbSettings.ProviderName)
                .GetDbContext(_dbSettings.ConnectionStrings);
        }

        public bool Create(TEntity entity)
        {
            DbConnection.Open();
            try
            {
                var result = DbConnection.Insert(entity);
                return result == 0;
            }
            finally
            {
                DbConnection.Close();
            }
        }

        public bool Delete(Guid id)
        {
            DbConnection.Open();
            try
            {
                var entity = DbConnection.Get<TEntity>(id);

                if (entity == null)
                    return false;

                return DbConnection
                    .Delete(entity);
            }
            finally
            {
                DbConnection.Close();
            }
        }

        public void Dispose()
        {
            DbConnection.Dispose();
        }

        public IQueryable<TEntity> FindAll()
        {
            DbConnection.Open();
            try
            {
                var results = DbConnection
                    .GetAll<TEntity>();

                return results
                    .AsQueryable();
            }
            finally
            {
                DbConnection.Close();
            }
        }

        public TEntity FindById(Guid id)
        {
            DbConnection.Open();

            try
            {
                return DbConnection
                    .Get<TEntity>(id);
            }
            finally { DbConnection.Close(); }
        }

        public bool Update(TEntity entity)
        {
            DbConnection.Open();

            try
            {
                return DbConnection
                    .Update(entity);
            }
            finally { DbConnection.Close(); }
        }
    }
}
