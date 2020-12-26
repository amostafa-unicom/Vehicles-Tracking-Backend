using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using E_Vision.Core.UseCases.Ping;
using E_Vision.Core.UseCases.Vehicle.VehicleGetAllUseCase;
using E_Vision.SharedKernel.CleanArchHandlers;
using E_Vision.SharedKernel.Dto;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace VehicleSimulator_WS
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private List<int> Vehicle = new List<int>();
        private WorkerSettings Setting { get; }
        public IVehicleGetAllUseCase VehicleGetAllUseCase { get; set; }
        public IPingUseCase PingUseCase { get; set; }
        public Worker(WorkerSettings settings, ILogger<Worker> logger, IServiceProvider services)
        {
            _logger = logger;
            Setting = settings;
            VehicleGetAllUseCase = services.CreateScope().ServiceProvider.GetRequiredService<IVehicleGetAllUseCase>();
            PingUseCase = services.CreateScope().ServiceProvider.GetRequiredService<IPingUseCase>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //Get All Vehicles And Store It Inside List Object
            OutputPort<ListResultDto<int>> _presenter = new OutputPort<ListResultDto<int>>();
            await VehicleGetAllUseCase.HandleUseCase(_presenter);
            if (_presenter.Result.IsSuccess && (_presenter.Result.Data?.Any() ?? default))
                Vehicle = _presenter.Result.Data;

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await DoWork();
                await Task.Delay(Setting.WorkingInterval, stoppingToken);
            }
        }

        private async Task DoWork()
        {
            var t = new Random().Next(0, Vehicle.Count);
            //Console.WriteLine(t);
            // Ping vehicle random
            Ping(t);
                 

        }
        private async Task Ping(int index)
        {
            OutputPort<ResultDto<bool>> _presenter = new OutputPort<ResultDto<bool>>();
             await PingUseCase.HandleUseCase(new PingInputDto { VehicleId = Vehicle[index] }, _presenter);
        }
    }
}
