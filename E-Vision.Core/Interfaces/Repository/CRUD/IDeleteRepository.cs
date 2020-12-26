using System;
using System.Linq.Expressions;

namespace E_Vision.Core.Interfaces.Repository.CRUD
{
    public interface IDeleteRepository<T> where T : class
    {
        #region Delete
        void BulkHardDelete(Expression<Func<T, bool>> filter = null);
        #endregion
    }
}
