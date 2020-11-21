using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManchesterFans.Application.FacadPatterns;
using ManchesterFans.Application.Interfaces;
using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Application.Interfaces.FacadPatterns;
using ManchesterFans.Application.Services.Pages.FacadPattern;
using ManchesterFans.Application.Services.Site.FacadPatterns;
using ManchesterFans.Application.Services.Users.FacadPattern;
using ManchesterFans.Persistance.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EndPoint.Site
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



            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = new PathString("/");
                options.ExpireTimeSpan = TimeSpan.FromMinutes(43200.0);
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Master", policy => policy.RequireRole("10"));
                options.AddPolicy("Admin", policy => policy.RequireRole("5", "6", "7", "8", "9", "10"));
                options.AddPolicy("User", policy => policy.RequireRole("1", "2", "3", "4", "5", "6", "7", "8", "9", "10"));
            });


            services.AddControllersWithViews();

            services.AddScoped<IDataBaseContext, DataBaseContext>();

            services.AddScoped<IUserFacad, UserFacad>();

            services.AddScoped<IPageFacad, PageFacad>();

            services.AddScoped<ISiteFacad, SiteFacad>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
