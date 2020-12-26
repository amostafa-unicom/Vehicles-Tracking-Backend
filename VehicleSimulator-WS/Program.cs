using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using E_Vision.Core.Interfaces.UnitOfWork;
using E_Vision.Infrastructure;
using E_Vision.Infrastructure.Context;
using E_Vision.SharedKernel.CleanArchHandlers;
using E_Vision.SharedKernel.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace VehicleSimulator_WS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
             .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .UseWindowsService()
            .ConfigureLogging(loggerFactory => loggerFactory.AddEventLog())
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration configuration = hostContext.Configuration;
                    services.AddHostedService<Worker>();
                    WorkerSettings workerSettings = new WorkerSettings();
                    configuration.Bind("WorkerSettings", workerSettings);
                    services.AddSingleton(workerSettings);

                    FireBaseSettings fireBaseSettings = new FireBaseSettings();
                    configuration.Bind("FireBaseSettings", fireBaseSettings);
                    services.AddSingleton(fireBaseSettings);
                })
             .ConfigureContainer<ContainerBuilder>((hostContext, builder) =>
             {
                 IConfiguration Configuration = hostContext.Configuration;
                 #region Register DB Context
                 builder.Register(c =>
                 {
                     return new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(Configuration.GetConnectionString("DBConString")).Options);
                 }).InstancePerLifetimeScope();
                 #endregion

                 #region Register Unit Of Work
                 builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
                 #endregion


                 #region Register The Genric Output Port
                 builder.RegisterGeneric(typeof(OutputPort<>)).PropertiesAutowired();
                 builder.RegisterGeneric(typeof(OutputPort<>)).As(typeof(IOutputPort<>)).InstancePerLifetimeScope().PropertiesAutowired();
                 #endregion

                 #region Register HttpContextAccessor In Order To Access The Http Context Inside A Class Library (Electricity.Correspondence.Core Project)
                 builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance().PropertiesAutowired();
                 #endregion


                 #region Register All Repositories & UseCases
                 builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).PublicOnly().Where(t => t.IsClass && t.Name.ToLower().EndsWith("usecase")).AsImplementedInterfaces().PropertiesAutowired().InstancePerLifetimeScope();
                 builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).PublicOnly().Where(t => t.IsClass && t.Name.ToLower().EndsWith("repository")).AsImplementedInterfaces().PropertiesAutowired().InstancePerLifetimeScope();
                 #endregion

 
             });
    }
}
