using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Assignment1.Repository;
using Assignment1.Service;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Assignment1.Controllers;
using Assignment;
using Microsoft.Extensions.Configuration;

namespace Assignment1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; set; }
        public void ConfigureServices(IServiceCollection service)
        {
            service.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });



            service.Configure<ConnectionString>(Configuration.GetSection("ConnectionString"));
            service.AddScoped<IActorRepository, ActorRepository>();
            service.AddScoped<IActorService, ActorService>();

            service.AddScoped<IMovieRepository,MovieRepository>();
            service.AddScoped<IMovieService, MovieService>();

            service.AddScoped<IProducerRepository, ProducerRepository>();
            service.AddScoped<IProducerService, ProducerService>();

            service.AddScoped<IGenreRepository, GenreRepository>();
            service.AddScoped<IGenreService, GenreService>();

            service.AddScoped<IReviewRepository, ReviewRepository>();
            service.AddScoped<IReviewService, ReviewService>();

            service.AddControllers().AddNewtonsoftJson();

        }
        public void Configure(IApplicationBuilder app,IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();  
            }
            app.UseCors("AllowAllOrigins");

            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Welcome to the Movie App API!");
                });
            });
            
        }
    }
}
