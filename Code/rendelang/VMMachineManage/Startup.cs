using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.WebEncoders;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using VMMachineManage.Data;

namespace VMMachineManage
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

            // 注册数据库上下文
            services.AddDbContext<VMMachineManageContext>(options => options.UseSqlServer(Configuration.GetConnectionString("VMMachineContext")));
            services.Configure<WebEncoderOptions>(options => options.TextEncoderSettings = 
            new TextEncoderSettings(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs));
            // 添加Session
            services.AddSession();
            services.Configure<CookiePolicyOptions>(options =>
            {
                //这个lambda决定对于一个给定的请求是否需要用户对非必需cookie的同意。
                options.CheckConsentNeeded = context => false;//默认为true，改为false
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //services.AddControllersWithViews();
            //services.AddDbContext<VMMachineManageContext>(option =>
            //option.UseSqlServer(Configuration.GetConnectionString("VMMachineContext")));

            //services.AddDbContext<VMMachineManageContext>(options =>
            //        options.UseSqlServer(Configuration.GetConnectionString("VMMachineManageContext")));

            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(options => {
            //        options.LoginPath = "/Home/Index";
            //    });
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
                app.UseExceptionHandler("/Shared/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
