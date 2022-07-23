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
                options.Password.RequireDigit = false;//�ifrede rakam bulunmal� m� bulunmamal� m�
                options.Password.RequiredLength = 5;//�ifre uzunlu�u
                options.Password.RequiredUniqueChars = 0;//ka� �zel karakter olmal�
                options.Password.RequireNonAlphanumeric = false;//�zel karakterlerin eklenip eklenmeyece�i
                options.Password.RequireLowercase = false;//k���k harf zorunlulu�u
                options.Password.RequireUppercase = false;//b�y�k harf bulunmas�
                //username and email options
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrs�tu�vwxyzABCDEFGHI�JKLMNOPQRS�TU�VWXYZ0123456789-._@+*";//kullan�c� ad� olu�tuturken izin verilen �zel karakterler
                options.User.RequireUniqueEmail = true;//unique email
            }).AddEntityFrameworkStores<StokTakipDbContext>();
            #endregion

            #region Cookie Options
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/User/Login");//kullan�c� giri�i yapmadan admin areaya eri�mek istersen bu yola y�nlendirilir.
                options.LogoutPath = new PathString("/User/Logout");
                options.Cookie = new CookieBuilder
                {
                    Name = "StokTakip",
                    HttpOnly = true,// cookie  i�lemlerini sadece http �zerinden g�nderilmesini sa�lar.g�venlik.js ile cookie ye eri�emez
                    SameSite = SameSiteMode.Strict,//cookie bilgilerinin nereden geldi�ini kontrol eder.g�venlik. 
                    SecurePolicy = CookieSecurePolicy.SameAsRequest//g�venlik always(https) olmal�
                };
                options.SlidingExpiration = true;//cookie zaman� uzatma
                options.ExpireTimeSpan = System.TimeSpan.FromDays(7);//oturumun a��k kalma s�resi yar� yar�ya uzar
                options.AccessDeniedPath = new PathString("/User/AccessDenied");//giri� yapm��  kullan�c� yetkisi olmayan bir yere giri� yapmaya �al���rsa g�nderilece�i yol
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
