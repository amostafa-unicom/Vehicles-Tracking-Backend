using E_Vision.Core.Interfaces.Repository.CRUD;

namespace E_Vision.Core.Interfaces.Repository.Vehicle
{
    public interface IVehicleTrackingRepository: ICreateRepository<Entities.VehicleTracking>, IRetrieveRepository<Entities.VehicleTracking>
    {
    }
}
