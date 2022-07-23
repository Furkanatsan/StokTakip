using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StokTakip.Bll.Abstract;
using StokTakip.Bll.AutoMapper.Profiles;
using StokTakip.Bll.Concrete;
using StokTakip.Dal.Context;
using StokTakip.Dal.Core.UnitOfWork.Abstract;
using StokTakip.Dal.Core.UnitOfWork.Concrete;
using StokTakip.Dal.Entities;
using StokTakip.Mvc.AutoMapper.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StokTakip.Mvc
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
            #region Identity Options
            services.AddIdentity<User, Role>(options=> {
                //password options
                options.Password.RequireDigit = false;//þifrede rakam bulunmalý mý bulunmamalý mý
                options.Password.RequiredLength = 5;//þifre uzunluðu
                options.Password.RequiredUniqueChars = 0;//kaç özel karakter olmalý
                options.Password.RequireNonAlphanumeric = false;//özel karakterlerin eklenip eklenmeyeceði
                options.Password.RequireLowercase = false;//küçük harf zorunluluðu
                options.Password.RequireUppercase = false;//büyük harf bulunmasý
                //username and email options
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrsþtuüvwxyzABCDEFGHIÝJKLMNOPQRSÞTUÜVWXYZ0123456789-._@+*";//kullanýcý adý oluþtuturken izin verilen özel karakterler
                options.User.RequireUniqueEmail = true;//unique email
            }).AddEntityFrameworkStores<StokTakipDbContext>();
            #endregion

            #region Cookie Options
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/User/Login");//kullanýcý giriþi yapmadan admin areaya eriþmek istersen bu yola yönlendirilir.
                options.LogoutPath = new PathString("/User/Logout");
                options.Cookie = new CookieBuilder
                {
                    Name = "StokTakip",
                    HttpOnly = true,// cookie  iþlemlerini sadece http üzerinden gönderilmesini saðlar.güvenlik.js ile cookie ye eriþemez
                    SameSite = SameSiteMode.Strict,//cookie bilgilerinin nereden geldiðini kontrol eder.güvenlik. 
                    SecurePolicy = CookieSecurePolicy.SameAsRequest//güvenlik always(https) olmalý
                };
                options.SlidingExpiration = true;//cookie zamaný uzatma
                options.ExpireTimeSpan = System.TimeSpan.FromDays(7);//oturumun açýk kalma süresi yarý yarýya uzar
                options.AccessDeniedPath = new PathString("/User/AccessDenied");//giriþ yapmýþ  kullanýcý yetkisi olmayan bir yere giriþ yapmaya çalýþýrsa gönderileceði yol
            });
            #endregion

            services.AddControllersWithViews();
            services.AddSession();
            services.AddAutoMapper(typeof(CategoryProfile), typeof(AuthorProfile), typeof(BookProfile),typeof(ViewModelsProfile), typeof(UserProfile));
            services.AddDbContext<StokTakipDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IBookService, BookManager>();
            services.AddScoped<IAuthorService, AuthorManager>();
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
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
