using Mailjet.ConsoleApplication;
using MatchScore.Data;
using MatchScore.Helpers;
using MatchScore.Helpers.Contracts;
using MatchScore.Repositories;
using MatchScore.Repositories.Contracts;
using MatchScore.Services;
using MatchScore.Services.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchScore
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
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            {
                // This prevents the application from crashing when displaying mutually related entities
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "MatchScore API", Version = "v1" });
            });

            // EF DbContext
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"));
                options.EnableSensitiveDataLogging();
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(800);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.Configure<CloudinaryConfig>(Configuration.GetSection("CloudinarySettings"));
            services.Configure<EmailSenderConfig>(Configuration.GetSection("EmailSenderSettings"));

            services.AddHttpContextAccessor();

            // Repositories
            services.AddScoped<ITournamentsRepository, TournamentsRepository>();
            services.AddScoped<IMatchesRepository, MatchesRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<ICountriesRepository, CountriesRepository>();
            services.AddScoped<IPlayersRepository, PlayersRepository>();
            services.AddScoped<IRolesRepository, RolesRepository>();
            services.AddScoped<ISportClubsRepository, SportClubsRepository>();
            services.AddScoped<IRequestsRepository, RequestsRepository>();

            // Services
            services.AddScoped<ITournamentsService, TournamentsService>();
            services.AddScoped<ICountriesService, CountriesService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IPlayersService, PlayersService>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<ISportClubsService, SportClubsService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IMatchesService, MatchesService>();
            services.AddScoped<IRequestsService, RequestsService>();
            services.AddScoped<IPhotoService, PhotoService>();

            // Helpers
            services.AddScoped<IModelMapper, ModelMapper>();
            services.AddScoped<IAuthManager, AuthManager>();
            services.AddScoped<IEmailSender, EmailSender>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseSession();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "MatchScore API V1");
                options.RoutePrefix = "api/swagger";
            });
            //For static 

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

        }
    }
}
