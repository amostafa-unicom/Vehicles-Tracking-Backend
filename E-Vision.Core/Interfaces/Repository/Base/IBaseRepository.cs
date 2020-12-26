using E_Vision.Core.Interfaces.Repository.CRUD;

namespace E_Vision.Core.Interfaces.Repository.Base
{
    public interface IBaseRepository<T> : ICreateRepository<T>, IUpdateRepository<T>, IDeleteRepository<T>, IRetrieveRepository<T> where T : class
    {
    }
}
