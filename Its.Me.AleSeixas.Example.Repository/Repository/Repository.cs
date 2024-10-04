using Its.Me.AleSeixas.Example.Repository.Context;
using itsmealeseixas.architeture.domain.Seedworks.Abrastracts;
using itsmealeseixas.architeture.repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Its.Me.AleSeixas.Example.Repository.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly ApiDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(ApiDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            try
            {
                DbSet.Add(entity);
                await SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public virtual async Task AddRangeAsync(List<TEntity> entities)
        {
            await DbSet.AddRangeAsync(entities);
            await SaveChangesAsync();
        }
        public virtual async Task UpdateAsync(TEntity entity)
        {
            // Encontra a entidade existente no contexto de rastreamento
            var existingEntity = await DbSet.FindAsync(entity.Identifier); // Supondo que a entidade tenha uma propriedade Id

            if (existingEntity != null)
            {
                // Atualiza os valores da entidade existente com os valores da nova entidade
                Db.Entry(existingEntity).CurrentValues.SetValues(entity);

                // Salva as alterações no banco de dados
                await SaveChangesAsync();
            }
            else
            {
                throw new Exception("Entidade não encontrada.");
            }
        }
        public virtual async Task DeleteAsync(TEntity entity)
        {
            DbSet.Remove(entity);
        }
        public virtual async Task<bool> ExistAsync(TEntity entity)
        {
            return await DbSet.AsNoTracking().AnyAsync(x => x.Identifier == entity.Identifier);
        }
        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            try
            {
                return await DbSet.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public virtual async Task<TEntity> GetByIdAsync(Guid Id)
        {
            return await DbSet.AsNoTracking().Where(x => x.Identifier == Id).SingleOrDefaultAsync();
        }
        public async Task SaveChangesAsync()
        {

            await Db.SaveChangesAsync();
        }
    }
}
