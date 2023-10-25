using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using ShopApp.Business.Abstract;
using ShopApp.Business.Concrete;
using ShopApp.Data.Abstract;
using ShopApp.Data.Concrete.EfCore;
using ShopApp.webui.EmailServices;
using ShopApp.webui.Identity;

namespace ShopApp.webui
{
    public class Startup
    {
        private IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options => options.UseSqlite("Data Source=shopDb"));
            services.AddIdentity<User,IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options => {

                // password
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 6;

                // lockout
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1); //3 yanlış denemeden sonra hesaba bloke edilir 1 dk sonra açılır
                options.Lockout.AllowedForNewUsers = true;

                // User
                options.User.RequireUniqueEmail = true;

                //signIn
                options.SignIn.RequireConfirmedEmail = false; // true yap
                options.SignIn.RequireConfirmedPhoneNumber = false; // true yap

            });

            services.ConfigureApplicationCookie(options => {
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                    Name = "Shoplication.Security.Token",
                    SameSite = SameSiteMode.Strict
                };

            });

            services.AddScoped<IProductRepository,EfCoreProductRepository>();
            services.AddScoped<IProductService,ProductManager>();

            services.AddScoped<ICategoryRepository,EfCoreCategoryRepository>();
            services.AddScoped<ICategoryService,CategoryManager>();

            services.AddScoped<IEmailSender,SmtpEmailSender>(i => 
            
                new SmtpEmailSender(

                    _configuration["EmailSender:Host"],
                    _configuration.GetValue<int>("EmailSender:Port"),
                    _configuration["EmailSender:UserName"],
                    _configuration.GetValue<bool>("EmailSender:EnableSSL"),
                    _configuration["EmailSender:Password"]

                ));

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles(); // wwwroot

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider
                (
                    Path.Combine(Directory.GetCurrentDirectory(),"node_modules")),
                    
                    RequestPath = "/modules"
            });

            if (env.IsDevelopment())
            {
                SeedDatabase.Seed();
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // ADMIN PRODUCTS
                endpoints.MapControllerRoute(
                    name:"adminproducts",
                    pattern:"/admin/adminproducts",
                    defaults: new {controller="Admin",action="ProductList"}
                );

                endpoints.MapControllerRoute(
                    name:"admineditproduct",
                    pattern:"/admin/adminproducts/{id?}",
                    defaults: new {controller="Admin",action="EditProduct"}
                );

                endpoints.MapControllerRoute(
                    name:"admincreateproduct",
                    pattern:"/admin/createproduct",
                    defaults: new {controller="Admin",action="CreateProduct"}
                );

                // ADMIN CATEGORİES
                endpoints.MapControllerRoute(
                    name:"admincategories",
                    pattern:"/admin/admincategories",
                    defaults: new {controller="Admin",action="CategoryList"}
                );

                endpoints.MapControllerRoute(
                    name:"admineditcategory",
                    pattern:"/admin/admincategories/{id?}",
                    defaults: new {controller="Admin",action="EditCategory"}
                );

                endpoints.MapControllerRoute(
                    name:"admincreatecategory",
                    pattern:"/admin/createcategory",
                    defaults: new {controller="Admin",action="CreateCategory"}
                );

                // SEARCH
                endpoints.MapControllerRoute(
                    name:"search",
                    pattern:"search",
                    defaults: new {controller="Shop",action="Search"}
                );

                endpoints.MapControllerRoute(
                    name:"products",
                    pattern:"products/{category?}",
                    defaults: new {controller="Shop",action="List"}
                );

                endpoints.MapControllerRoute(
                    name:"productdetails",
                    pattern:"{url}",
                    defaults: new {controller="Shop",action="details"}
                );

                endpoints.MapControllerRoute(
                    name:"products",
                    pattern:"products/{category?}",
                    defaults: new {controller="Shop",action="List"}
                );

                endpoints.MapControllerRoute(
                    name : "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
