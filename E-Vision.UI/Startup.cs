using System;
using Autofac;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using E_Vision.Core.Interfaces.UnitOfWork;
using E_Vision.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using E_Vision.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using E_Vision.SharedKernel.CleanArchHandlers;
using E_Vision.UI.Controllers;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using E_Vision.SharedKernel.Settings;

namespace E_Vision.UI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private string AllowedOrigins { get; } = "AllowedOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            #region Add Controllers + As Service To Implement Property Injection
            services.AddControllersWithViews().AddControllersAsServices().AddNewtonsoftJson();
            #endregion
            FireBaseSettings fireBaseSettings = new FireBaseSettings();
            Configuration.Bind("FireBaseSettings", fireBaseSettings);
            services.AddSingleton(fireBaseSettings);

            #region CORS
            IConfigurationSection originsSection = Configuration.GetSection(AllowedOrigins);
            string[] origns = originsSection.AsEnumerable().Where(s => s.Value != null).Select(a => a.Value).ToArray();

            services.AddCors(options =>
            {
                options.AddPolicy(AllowedOrigins,
                    builder =>
                    {
                        builder.WithOrigins(origns)
                               .AllowAnyMethod()
                               .AllowAnyHeader()
                               .AllowCredentials();
                    });
            });
            #endregion

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = " API", Version = "v1" });

                c.DescribeAllParametersInCamelCase();
                c.DescribeAllEnumsAsStrings();
                c.DescribeStringEnumsInCamelCase();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });


            });
            #endregion
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
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

            #region Register Controller For Property DI
            Type controllerBaseType = typeof(BaseController);
            builder.RegisterAssemblyTypes(typeof(Program).Assembly).Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType).PropertiesAutowired();
            #endregion
             
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region CORS
            app.UseCors(AllowedOrigins);
            #endregion
            #region AppBuilder 
            app.UseHttpsRedirection();
            app.UseRouting(); 
            #endregion

            #region Swagger
            IConfigurationSection SwaggerBasePath = Configuration.GetSection("SwaggerBasePath");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{ SwaggerBasePath.Value}/swagger/v1/swagger.json", "Api V1");
                c.RoutePrefix = string.Empty;
            });
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
