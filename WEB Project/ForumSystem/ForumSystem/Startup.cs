using ForumSystem.Data;
using ForumSystem.Helpers;
using ForumSystem.Helpers.Contracts;
using ForumSystem.Repositories;
using ForumSystem.Repositories.Contracts;
using ForumSystem.Services;
using ForumSystem.Services.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem
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
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            {
                // This prevents the application from crashing when displaying mutually related entities
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "ForumSystem API", Version = "v1" });
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

            services.AddHttpContextAccessor();

            // Repositories
            services.AddScoped<ICommentsRepository, CommentsRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IRolesRepository, RolesRepository>();
            services.AddScoped<IPostsRepository, PostsRepository>();
            services.AddScoped<IPhoneNumbersRepository, PhoneNumbersRepository>();

            // Services
            services.AddScoped<ICommentsService, CommentsService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<IPostsService, PostsService>();

            services.AddScoped<IPhoneNumbersService, PhoneNumbersService>();

            // Helpers
            services.AddScoped<IModelMapper, ModelMapper>();
            services.AddScoped<IAuthManager, AuthManager>();
            services.AddScoped<IUserAuthorChecker, UserAuthorChecker>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseRouting();

            app.UseSession();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "ForumSystem API V1");
                options.RoutePrefix = "api/swagger";
            });
            //For static files , like images.
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
