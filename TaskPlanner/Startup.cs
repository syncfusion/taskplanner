using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskPlanner.Data;
using TaskPlanner.Models;
using TaskPlanner.Services;
using TaskPlanner.Base.Stories;
using TaskPlanner.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace TaskPlanner
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            ConfigurationSetup = Configuration;
        }

        /// <summary>
        /// Gets or sets the configuration
        /// </summary>
        public static IConfigurationRoot ConfigurationSetup { get; set; }

        /// <summary>
        /// Gets the configuration
        /// </summary>
        public IConfigurationRoot Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            TaskPlannerEntities.ConnectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IStoryBaseModel, StoryBaseModel>();
            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            });

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "Stories",
                    template: "projects/{projectId?}",
                    defaults: new { controller = "Stories", action = "Stories" });

                routes.MapRoute(
                    name: "StoriesList",
                    template: "storieslist/{projectId?}",
                    defaults: new { controller = "Stories", action = "StoriesList" });

				routes.MapRoute(
					name: "ProjectsTab",
					template: "projecttab/{projectId?}",
					defaults: new { controller = "Project", action = "LoadProject" });			

                routes.MapRoute(
                    name: "Add project",
                    template: "project/addproject",
                    defaults: new { controller = "Project", action = "Newproject" });

                routes.MapRoute(
                name: "Update Project",
                template: "project/updateproject",
                defaults: new { controller = "Project", action = "AddProjectAsync" });

				routes.MapRoute(
					name: "Share project",
					template: "project/shareproject",
					defaults: new { controller = "Project", action = "ShareProject" });

				routes.MapRoute(
					name: "Share Email",
					template: "project/shareemail/{projectId?}/{email?}",
					defaults: new { controller = "Project", action = "ShareEmail" });
			});
        }
    }
}
