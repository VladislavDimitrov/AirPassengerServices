using AutoMapper;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service;
using Service.Contracts;
using Web.Utilities;

namespace Web
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
            services.AddControllersWithViews();
            services.AddDbContext<ClaimsDbContext>(options =>
            options.UseSqlServer(
                 Configuration.GetConnectionString("DefaultConnection")));
            services.AddSingleton(Configuration);
            services.AddDefaultIdentity<User>(a =>
            {
                a.Password.RequireDigit = false;
                a.Password.RequireUppercase = false;
                a.Password.RequireLowercase = false;
                a.Password.RequireNonAlphanumeric = false;
            })
                .AddDefaultUI()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ClaimsDbContext>();
            services.AddRazorPages();
            services.AddMvc();
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IClaimServices, ClaimServices>();
            services.AddScoped<IPhoneNumberServices, PhoneNumberServices>();
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
            app.Plant().Wait();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
