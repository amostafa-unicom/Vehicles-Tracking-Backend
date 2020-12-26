using E_Vision.SharedKernel.Enum;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace E_Vision.Core.Interfaces.Repository.CRUD
{
    public interface IRetrieveRepository<T> where T : class
    {
        #region Retreive

        #region GetById
        Task<T> GetByIdIfNotDeleted(int Id);
        #endregion

        #region GetList
        Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "");
        #endregion

        #region Get Paged
        Task<List<T>> GetPageAsync<TKey>(int PageNumeber, int PageSize, Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> sortingExpression, SharedKernelEnums.SortDirection sortDir = SharedKernelEnums.SortDirection.Ascending, string includeProperties = "");
        #endregion

        #region Get Individuals
        Task<int> GetCountAsync(Expression<Func<T, bool>> filter = null);
        Task<bool> GetAnyAsync(Expression<Func<T, bool>> filter = null);
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "");
        Task<T> GetLastOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "");
        #endregion

        #region Get Max
        Task<int> GetMaxAsync();
        #endregion

        #endregion
    }
}
