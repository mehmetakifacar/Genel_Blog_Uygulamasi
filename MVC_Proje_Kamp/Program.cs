using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

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
            //builder.Services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("RequireAdministratorRole", policy =>
            //        policy.RequireRole("Administrator"));
            //});

            //builder.Services.AddIdentity<Admin, IdentityRole>()
            //.AddEntityFrameworkStores<IdentityDbContext>()
            //.AddDefaultTokenProviders();


            //AddAuthentication
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.Cookie.Name = "Login";
                options.LoginPath = "/Login/Index";
                options.AccessDeniedPath = "/Login/Index";
            });



            //Authorization Check
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStatusCodePagesWithReExecute("/ErrorPage/Page404");
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession(); // Oturum yönetimini kullan


            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllerRoute(
                name: "Default",
                pattern: "{controller=Category}/{action=Index}/{id?}");

            app.Run();
        }


    }
}
