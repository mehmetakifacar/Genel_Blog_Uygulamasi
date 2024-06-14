using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Concrete.Repositories;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using MVC_Proje_Kamp.Controllers;
using System.Security.Claims;

namespace MVC_Proje_Kamp
{


    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            var connectionString = configuration.GetConnectionString("mssqlconnection");



            builder.Services.AddControllersWithViews();
            builder.Services.AddSession();

            builder.Services.AddScoped<AdminManager>();
            builder.Services.AddScoped<IAdminDal, EfAdminDal>();

            builder.Services.AddScoped<WriterManager>();
            builder.Services.AddScoped<IWriterService, WriterManager>();
            builder.Services.AddScoped<IWriterDal, EfWriterDal>();

            builder.Services.AddScoped<IContentService, ContentManager>();
            builder.Services.AddScoped<IContentDal, EfContentDal>();


            builder.Services.AddHttpContextAccessor();




            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = "AdminScheme";
            })
            .AddCookie("AdminScheme", options =>
             {
                 options.LoginPath = "/Login/Index";
                 options.AccessDeniedPath = "/Login/Index";
                 options.Cookie.Name = "AdminScheme";
             })
            .AddCookie("WriterScheme", options =>
            {
                options.LoginPath = "/WriterPanelLogin/WriterLogin";
                options.AccessDeniedPath = "/WriterPanelLogin/WriterLogin";
                options.Cookie.Name = "WriterScheme";
                options.LogoutPath = "/WriterPanelLogin/WriterLogOut";
            });




            // Authorization Check
            builder.Services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStatusCodePagesWithReExecute("/ErrorPage/Page404");
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "Default",
                pattern: "{controller=Category}/{action=Index}/{id?}");

            app.Run();
        }
    }

}


