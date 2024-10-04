using itsmealeseixas.architeture.domain.Seedworks.Abrastracts;
using itsmealeseixas.architeture.service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itsmealeseixas.architeture.repository
{
    public interface IUnitOfWorkService
    {
        public IService<TEntity> GetService<TEntity>() where TEntity : Entity;
    }
}
