using E_Vision.Core.Interfaces.Repository.Vehicle;
using E_Vision.Core.UseCases.Base;
using E_Vision.Core.UseCases.Vehicle;
using E_Vision.SharedKernel.CleanArchHandlers;
using E_Vision.SharedKernel.Dto;
using E_Vision.SharedKernel.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Vision.Core.UseCases.Tracking.GetAllDisConnectedVehicleUseCase
{
    public class GetAllDisConnectedVehicleUseCase : BaseUseCase, IGetAllDisConnectedVehicleUseCase
    {
        #region Props
        public IVehicleRepository VehicleRepository { get; set; }
        private readonly FireBaseSettings fireBaseSettings;
        #endregion
        public GetAllDisConnectedVehicleUseCase(FireBaseSettings _fireBaseSettings)
        {
            fireBaseSettings = _fireBaseSettings;
        }
        public async Task<bool> HandleUseCase(IOutputPort<ListResultDto<int>> _response)
        {
            /// Get Disconnected Vehicles which didn't made request wihin 1 minute
            List<int> result = await VehicleRepository.GetDisconnectedVehicle(GetRequestInterval()) ;
            //if (result?.Any() ?? default)
            //{ 
                //Change vehicle status
                new SharedMethods().ChangeVehicleStatus(result, fireBaseSettings.DbURL, fireBaseSettings.DisconnectedUrl);
           // }

            _response.HandlePresenter(new ListResultDto<int>(result, result.Count));
            return true;
        }
         
    }
}
