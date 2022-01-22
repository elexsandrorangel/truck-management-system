using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TruckManagement.Business.Implementation;
using TruckManagement.Business.Interfaces;
using TruckManagement.Business.Profiles;
using TruckManagement.Middleware;
using TruckManagement.Repository;
using TruckManagement.Repository.Contexts;
using TruckManagement.Repository.Interfaces;

namespace TruckManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsApi", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Truck's Management API", Version = "v1" });
            });

            #region Database Setup

            services.AddDbContext<TrucksDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("TrucksContext"))
            );
            #endregion Database Setup

            #region DI/IoC

            // Automapper profiles
            services.AddAutoMapper(typeof(MappingProfiles));

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #region Services

            services.AddTransient<ITruckBusiness, TruckBusiness>();
            
            #endregion Services

            #region Repository 

            services.AddTransient<ITruckRepository, TruckRepository>();

            #endregion Repository

            #endregion DI/IoC

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Truck's Management API v1"));
            }

            app.UseCors("CorsApi");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<AppExceptionMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // this will do the initial DB population
            InitializeDatabase(app);
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                // Apply all pending migrations
                scope.ServiceProvider.GetRequiredService<TrucksDbContext>().Database.Migrate();
            }
        }
    }
}
