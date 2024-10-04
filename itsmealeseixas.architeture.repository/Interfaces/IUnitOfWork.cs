using itsmealeseixas.architeture.domain.Seedworks.Abrastracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itsmealeseixas.architeture.repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : Entity;
        Task SaveChanges();
        Task BeginTransaction();
        Task CommitTransaction();
        Task RollbackTransaction();
    }
}
