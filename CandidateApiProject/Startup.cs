using CandidateApiProject.Extensions;
using CandidateApiProject.Interface;
using CandidateApiProject.Models;
using CandidateApiProject.Services;
using CandidateApiProject.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Runtime;

namespace CandidateApiProject
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            configuration.GetSection("ApplicationSettings").Bind(MySettings.Setting);
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddJsonOptions(opt =>
                {
                    var serializerOptions = opt.JsonSerializerOptions;
                    serializerOptions.IgnoreNullValues = true;
                    serializerOptions.IgnoreReadOnlyProperties = false;
                    serializerOptions.WriteIndented = true;
                });

            // Configure the persistence in another layer

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "CandidateApiProject",
                    Description = "Swagger surface",
                    Contact = new OpenApiContact
                    {
                        Name = "Oğuz Ülkü",
                        Email = "oguz-ulku@hotmail.com",
                        Url = new Uri("http://www.oguzulku.com")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("http://www.oguzulku.com")
                    }
                });
            });

            RegisterServices(services);
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Candidate Api Project v1.0");
            });
        }

        private void RegisterServices(IServiceCollection services)
        {
            var connStr = Configuration.GetConnectionString("DefaultConnection");

            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));

            services.AddNHibernate(connStr);
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IPaymentProcess, PaymentService>();
            services.AddTransient<ITransactionService,TransactionService>();
            services.AddTransient<HttpClient>();
            services.AddTransient<IHttpService, HttpService>();
            services.AddTransient<TCKNAuthenticationService>();
        }
    }
}
