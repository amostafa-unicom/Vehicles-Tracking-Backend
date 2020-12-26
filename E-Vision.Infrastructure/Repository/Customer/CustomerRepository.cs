using E_Vision.Core.Interfaces.Repository.Customer;
using E_Vision.Infrastructure.Context;
using E_Vision.Infrastructure.Repository.Base;

namespace E_Vision.Infrastructure.Repository.Customer
{
    public class CustomerRepository:BaseRepository<Core.Entities.Customer>,ICustomerRepository
    {
        public CustomerRepository(AppDbContext context):base(context)
        {

        }
    }
}
