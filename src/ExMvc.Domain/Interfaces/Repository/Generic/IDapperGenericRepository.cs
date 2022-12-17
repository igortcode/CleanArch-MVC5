using System;
using System.Linq;

namespace ExMvc.Domain.Interfaces.Repository.Generic
{
    public interface IDapperGenericRepository<TEntity>
    {
        IQueryable<TEntity> FindAllAsync();
        TEntity FindByIdAsync(Guid id);
        bool CreateAsync(TEntity entity);
        bool UpdateAsync(TEntity entity);
        bool DeleteAsync(Guid id);
    }
}
