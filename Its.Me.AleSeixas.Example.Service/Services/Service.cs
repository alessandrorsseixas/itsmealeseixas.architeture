using itsmealeseixas.architeture.domain.Seedworks.Abrastracts;
using itsmealeseixas.architeture.repository.Interfaces;
using itsmealeseixas.architeture.service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Its.Me.AleSeixas.Example.Service.Services
{
    public class Service<TEntity> : IService<TEntity> where TEntity : Entity
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<TEntity> _repository;
        public Service(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            try
            {
                await _repository.AddAsync(entity);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task AddRangeAsync(List<TEntity> entities)
        {
            try
            {
                await _repository.AddRangeAsync(entities);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task DeleteAsync(TEntity entity)
        {
            try
            {
                await _repository.DeleteAsync(entity);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> ExistAsync(TEntity entity)
        {

            try
            {
                return await _repository.ExistAsync(entity);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {

            try
            {
                return await _repository.GetAsync(predicate);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<TEntity>> GetAllAsync()
        {

            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task UpdateAsync(TEntity entity)
        {

            try
            {
                await _repository.UpdateAsync(entity);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<TEntity> GetByIdAsync(Guid Id)
        {
            try
            {
                return await _repository.GetByIdAsync(Id);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _repository.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
