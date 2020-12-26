using E_Vision.Core.Interfaces.Repository.Vehicle;
using E_Vision.Core.UseCases.Base;
using E_Vision.Core.UseCases.Vehicle;
using E_Vision.SharedKernel.CleanArchHandlers;
using E_Vision.SharedKernel.Dto;
using E_Vision.SharedKernel.Exceptions;
using E_Vision.SharedKernel.Settings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static E_Vision.SharedKernel.Enum.SharedKernelEnums;

namespace E_Vision.Core.UseCases.Ping
{
    public class PingUseCase : BaseUseCase, IPingUseCase
    {
        #region Props
        public IVehicleRepository VehicleRepository { get; set; }
        private readonly FireBaseSettings fireBaseSettings;
        #endregion
        public PingUseCase(FireBaseSettings _fireBaseSettings)
        {
            fireBaseSettings = _fireBaseSettings;
        }
        public async Task<bool> HandleUseCase(PingInputDto request, IOutputPort<ResultDto<bool>> outputPort)
        {
            #region Validate Request Input
            if (request == default || request.VehicleId == default)
                throw new ValidationsException(" Invalid request");
            #endregion

            // Get Vehicle
            Entities.Vehicle vehicle = await GetVehicle(request.VehicleId);
            
            //vehicle not found
            if(vehicle==default)
                throw new ValidationsException(" Invalid vehicle ID");
            //Add New vehicle status
            vehicle.VehicleTracking.Add(new Entities.VehicleTracking { RequestTime = DateTime.UtcNow });
            vehicle.LastPingTime = DateTime.UtcNow;
            //update vehicle
            VehicleRepository.Update(vehicle);
            //commit changes

            //Change vehicle status
            new SharedMethods().ChangeVehicleStatus(new List<int> { request.VehicleId}, fireBaseSettings.DbURL, fireBaseSettings.ConnectedUrl); 

            bool saveResult = await UnitOfWork.Commit();

            outputPort.HandlePresenter(new ResultDto<bool>(saveResult));
            return true;
        }

        private async Task<Entities.Vehicle> GetVehicle(int id)
        {
            return await VehicleRepository.GetFirstOrDefaultAsync(x => x.IsDeleted == (byte)DeleteStatus.NotDeleted && x.Id == id, $"{nameof(Entities.VehicleTracking)}");
        }
    }
}
