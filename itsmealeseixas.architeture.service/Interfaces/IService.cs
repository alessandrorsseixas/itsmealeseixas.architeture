using itsmealeseixas.architeture.domain.Seedworks.Abrastracts;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace itsmealeseixas.architeture.service.Interfaces
{
    
    public interface IService<TEntity> where TEntity : Entity
    {

        Task AddAsync(TEntity entity);
        Task AddRangeAsync(List<TEntity> entities);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<bool> ExistAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetByIdAsync(Guid Id);
        Task<List<TEntity>> GetAllAsync();
        Task SaveChangesAsync();
    }
}
