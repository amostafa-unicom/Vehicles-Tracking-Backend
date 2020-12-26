namespace E_Vision.Core.Interfaces.Repository.CRUD
{
    public interface IUpdateRepository<T> where T : class
    {
        void Update(T entity);
    }
}
