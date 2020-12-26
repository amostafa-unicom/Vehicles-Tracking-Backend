using E_Vision.Core.Interfaces.Repository.Vehicle;
using E_Vision.Core.UseCases.Base;
using E_Vision.SharedKernel.CleanArchHandlers;
using E_Vision.SharedKernel.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static E_Vision.SharedKernel.Enum.SharedKernelEnums;

namespace E_Vision.Core.UseCases.Vehicle.VehicleGetAllUseCase
{
    public class VehicleGetAllUseCase : BaseUseCase, IVehicleGetAllUseCase
    {
        #region Props
        public IVehicleRepository VehicleRepository { get; set; }
        #endregion
        public async Task<bool> HandleUseCase(IOutputPort<ListResultDto<int>> _response)
        {
            List<Entities.Vehicle> vehicles = await VehicleRepository.GetWhereAsync(x => x.IsDeleted == (byte)DeleteStatus.NotDeleted);
            if (vehicles?.Any() ?? default)
                _response.HandlePresenter(new ListResultDto<int>(vehicles.Select(x => x.Id).ToList(), vehicles.Count));
            else
                _response.HandlePresenter(new ListResultDto<int>());

            return true;
        }
    }
}
