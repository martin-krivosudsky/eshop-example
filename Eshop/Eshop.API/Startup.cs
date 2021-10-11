using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Eshop.Core.Mocks;
using Eshop.Core;
using Eshop.Core.DAL;
using Eshop.Core.Services;
using Eshop.Services;
using Eshop.DAL;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.API
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
            services.AddDbContext<EshopDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), c => c.MigrationsAssembly("Eshop.API")));

            services.AddScoped(typeof(IProductDAL), typeof(ProductDAL));
            services.AddScoped(typeof(IProductService), typeof(ProductService));

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(2, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV"; 
                options.SubstituteApiVersionInUrl = true;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Eshop API", Version = "v1" });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "Eshop API", Version = "v2" });
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, EshopDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                dbContext.TruncateTables().InitializeTestDatabase();

                app.UseCors(builder => builder
                   .WithOrigins("https://localhost:44308")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials());
            }

            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Eshop API v1");
                    options.SwaggerEndpoint("/swagger/v2/swagger.json", "Eshop API v2");
                });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
