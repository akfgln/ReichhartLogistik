using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ReichhartLogistik.Data.Utility;

namespace ReichhartLogistik.Data.Entities
{
    public partial interface IRepository<T> where T : BaseEntity
    {

        Task<T> GetByIdAsync(int? id, bool includeDeleted = true);

        Task<IList<T>> GetByIdsAsync(IList<int> ids, bool includeDeleted = true);

        Task<IList<T>> GetAllAsync(bool includeDeleted = true);

        Task<IPagedList<T>> GetAllPagedAsync(int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false, bool includeDeleted = true);

        Task InsertAsync(T entity, bool pushEvent = true);

        Task InsertAsync(IList<T> entities, bool pushEvent = true);

        Task UpdateAsync(T entity, bool pushEvent = true);

        Task UpdateAsync(IList<T> entities, bool pushEvent = true);

        Task DeleteAsync(T entity, bool pushEvent = true);

        Task DeleteAsync(IList<T> entities, bool pushEvent = true);

        IQueryable<T> Table { get; }
    }
}
