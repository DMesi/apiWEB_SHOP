
using apiWS.Models;
using apiWS.IRepository;
using apiWS.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiWS.Dto;
using Microsoft.AspNetCore.Identity;
using apiWS.Services;
using AspNetCoreRateLimit;

namespace apiWS
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
            services.AddDbContext<DataBaseContext>(options =>

            options.UseSqlServer(Configuration.GetConnectionString("sqlConnections"))

            );

            services.AddMemoryCache(); // koliko puta ko cemu pristupa
            
           services.ConfigureRateLimiting();

            services.AddHttpContextAccessor();



            services.ConfigureHttpCacheHeaders();
           
            


            


            services.AddResponseCaching();

            services.AddAuthentication();

            // ovo je u novo napravljenoj klasi ServiceExtensions
            services.ConfigureIdentity();
            services.ConfigureJWT(Configuration);


           


            //CORS dozvola
            services.AddCors(o => {

                o.AddPolicy("DozvoliSveCORS", builder =>

                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            });

            services.AddIdentityCore<ApiUser>(q => q.User.RequireUniqueEmail = true);


            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAuthManager, AuthManager>();

            services.AddAutoMapper(typeof(MapperInitilizer));

            services.AddControllers(config =>
            {
                config.CacheProfiles.Add("120SecondsDuration", new CacheProfile
                {

                    Duration = 120

                });
            });



                services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "apiWS", Version = "v1" });
            });


            services.ConfigureVersioning();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "apiWS v1"));

                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/ProductsAPI/swagger.json", "Prod"));
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/LocationAPI/swagger.json", "Loc"));
                


            }

            app.ConfigureExceptionHandler();

            app.UseHttpsRedirection();

           
            app.UseCors("DozvoliSveCORS"); // dozvola, definisana gore


            app.UseResponseCaching();
            app.UseHttpCacheHeaders();

            app.UseIpRateLimiting();
            
            
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
