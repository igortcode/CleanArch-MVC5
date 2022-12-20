using System;
using System.Linq;

namespace ExMvc.Domain.Interfaces.Repository.Generic
{
    public interface IGenericRepository<TEntity> : IDisposable
    {
        IQueryable<TEntity> FindAll();
        TEntity FindById(Guid id);
        bool Create(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(Guid id);
    }
}
