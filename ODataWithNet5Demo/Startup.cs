using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;
using Microsoft.OpenApi.Models;
using ODataWithNet5Demo.Models;
using System.Linq;

namespace ODataWithNet5Demo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "odata", Version = "v1" });
            });

            services.AddControllers().AddNewtonsoftJson();
            services.AddOData();
            services.AddMvc(o => o.EnableEndpointRouting = false);
            services.AddODataQueryFilter();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ODataWithNet5Demo v1"));
            }

            app.UseHttpsRedirection();

            app.UseMvc(routeBuilder =>
            {
                routeBuilder.Select().Filter().Expand().MaxTop(100).Count().OrderBy();
                routeBuilder.EnableDependencyInjection();
                routeBuilder.MapODataServiceRoute("odata", "odata", GetEdmModel());
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<Student>("Students");
            return builder.GetEdmModel();
        }
    }
}
