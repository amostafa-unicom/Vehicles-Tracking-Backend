using System;
using System.Threading;
using System.Threading.Tasks;
using E_Vision.Core.UseCases.Tracking.GetAllDisConnectedVehicleUseCase;
using E_Vision.SharedKernel.CleanArchHandlers;
using E_Vision.SharedKernel.Dto;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace VehicleTracking_WS
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private WorkerSettings Setting { get; }
        public IGetAllDisConnectedVehicleUseCase GetAllDisConnectedVehicleUseCase { get; set; }
        public Worker(WorkerSettings settings, ILogger<Worker> logger, IServiceProvider services)
        {
            _logger = logger;
            Setting = settings;
            GetAllDisConnectedVehicleUseCase = services.CreateScope().ServiceProvider.GetRequiredService<IGetAllDisConnectedVehicleUseCase>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
         

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await DoWork();
                await Task.Delay(Setting.WorkingInterval, stoppingToken);
            }
        }

        private async Task DoWork()
        { 
            OutputPort<ListResultDto<int>> _presenter = new OutputPort<ListResultDto<int>>();
            var Result = await GetAllDisConnectedVehicleUseCase.HandleUseCase(_presenter);
            // send notification by signalR


        }
    }
}
