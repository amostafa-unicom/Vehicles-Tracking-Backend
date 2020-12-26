using E_Vision.Core.Interfaces.Repository.CRUD;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Vision.Core.Interfaces.Repository.Vehicle
{
    public interface IVehicleRepository: ICreateRepository<Entities.Vehicle>, IRetrieveRepository<Entities.Vehicle>,IUpdateRepository<Entities.Vehicle>
    {
        Task<List<int>> GetDisconnectedVehicle(int second);
    }
}
