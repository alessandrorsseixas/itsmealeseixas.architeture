using itsmealeseixas.architeture.domain.Seedworks.Abrastracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itsmealeseixas.architeture.service.Interfaces
{
    public interface IUnitOfWorkService
    {
        public IService<TEntity> GetService<TEntity>() where TEntity : Entity;
    }
}
