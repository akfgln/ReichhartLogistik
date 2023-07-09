using Microsoft.EntityFrameworkCore;
using ReichhartLogistik.Data.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ReichhartLogistik.Data.Entities
{
    public partial class EntityRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {

        private readonly ApplicationDbContext _dbContext;

        private IQueryable<TEntity> DeletedFilter(bool includeDeleted)
        {
            var query = Table;
            if (!includeDeleted)
            {
                query = query.OfType<IDeletedEntity>().Where(entry => !entry.Deleted).OfType<TEntity>();
            }

            return query;
        }

        public EntityRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task DeleteAsync(TEntity entity, bool pushEvent = true)
        {
            switch (entity)
            {
                case null:
                    throw new ArgumentNullException(nameof(entity));

                case IDeletedEntity softDeletedEntity:
                    softDeletedEntity.Deleted = true;
                    await UpdateAsync(entity, false);
                    break;

                default:
                    _dbContext.Remove(entity);
                    break;
            }
            await _dbContext.SaveChangesAsync();
            ////event notification
            //if (pushEvent)
            //    await _eventService.EntityDeletedAsync(entity);
        }

        public async Task DeleteAsync(IList<TEntity> entities, bool pushEvent = true)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            if (entities.OfType<IDeletedEntity>().Any())
            {
                foreach (var entity in entities)
                {
                    if (entity is IDeletedEntity softDeletedEntity)
                    {
                        softDeletedEntity.Deleted = true;
                        await UpdateAsync(entity, false);
                    }
                }
            }
            else
            {
                using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                _dbContext.RemoveRange(entities);
                _dbContext.SaveChanges();
                transaction.Complete();
            }

            //event notification
            if (!pushEvent)
                return;

            //foreach (var entity in entities)
            //    await _eventService.EntityDeletedAsync(entity);
        }

        public async Task<IList<TEntity>> GetAllAsync(bool includeDeleted = true)
        {
            var query = DeletedFilter(includeDeleted);

            return await query.ToListAsync();
        }

        public async Task<IPagedList<TEntity>> GetAllPagedAsync(int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false, bool includeDeleted = true)
        {
            var query = DeletedFilter(includeDeleted);

            return await query.ToPagedListAsync(pageIndex, pageSize, getOnlyTotalCount);
        }

        public async Task<TEntity> GetByIdAsync(int id, bool includeDeleted = true)
        {
            if (id == 0)
                return null;
            
            return await DeletedFilter(includeDeleted).FirstOrDefaultAsync(entity => entity.Id == Convert.ToInt32(id));
        }

        public async Task<IList<TEntity>> GetByIdsAsync(IList<int> ids, bool includeDeleted = true)
        {
            if (!ids?.Any() ?? true)
                return new List<TEntity>();

            async Task<IList<TEntity>> getByIdsAsync()
            {
                var query = DeletedFilter(includeDeleted);

                var entries = await query.Where(entry => ids.Contains(entry.Id)).ToListAsync();

                var sortedEntries = new List<TEntity>();
                foreach (var id in ids)
                {
                    var sortedEntry = entries.Find(entry => entry.Id == id);
                    if (sortedEntry != null)
                        sortedEntries.Add(sortedEntry);
                }

                return sortedEntries;
            }
            return await getByIdsAsync();
        }

        public async Task InsertAsync(TEntity entity, bool pushEvent = true)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            ////event notification
            //if (pushEvent)
            //    await _evenService.EntityInsertedAsync(entity);
        }

        public async Task InsertAsync(IList<TEntity> entities, bool pushEvent = true)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            await _dbContext.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
            transaction.Complete();

            if (!pushEvent)
                return;

            ////event notification
            //foreach (var entity in entities)
            //    await _eventService.EntityInsertedAsync(entity);
        }

        public async Task UpdateAsync(TEntity entity, bool pushEvent = true)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();

            ////event notification
            //if (pushEvent)
            //    await _eventService.EntityUpdatedAsync(entity);
        }

        public async Task UpdateAsync(IList<TEntity> entities, bool pushEvent = true)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            if (entities.Count == 0)
                return;

            _dbContext.UpdateRange(entities);
            await _dbContext.SaveChangesAsync();

            //event notification
            if (!pushEvent)
                return;

            //foreach (var entity in entities)
            //    await _eventService.EntityUpdatedAsync(entity);
        }

        public IQueryable<TEntity> Table => _dbContext.Set<TEntity>().AsQueryable<TEntity>();
    }
}
