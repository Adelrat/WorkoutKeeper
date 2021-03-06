using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkoutKeeper.Models;

namespace WorkoutKeeper
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            // �������� ������ ����������� �� ����� ������������
            string connection1 = Configuration.GetConnectionString("DefaultConnection");
            // ��������� �������� MobileContext � �������� ������� � ����������
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connection1));
            // ���������� �� �������������
            string connection2 = Configuration.GetConnectionString("IdentityConnection");
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(connection2));
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>();

            services.AddTransient<ITrainingRepository, EFTrainingRepository>();
            services.AddControllersWithViews(mvcOtions =>
            {
                mvcOtions.EnableEndpointRouting = false;
            });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();

            app.UseAuthentication();    // ����������� ��������������
            app.UseAuthorization();

            //����������� � ��������������!!
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                 pattern: "{controller=Training}/{action=List}/{id?}");
                //endpoints.MapRazorPages();
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: null,
                    template: "{controller=Admin}/{action=AddEx}"                 
                    );
                routes.MapRoute(
                    name: null,
                    template: "{controller=Admin}/{action=IndexTrain}"
                    );
                //������ �������!
                routes.MapRoute(
                    name: null,
                    template: "{controller=Admin}/{action=IndexExercises}"
                    );
                routes.MapRoute(
                    name: null,
                    template: "{controller=CustomTraining}/{action=Index}"
                    );
                //routes.MapRoute(
                //     name: null,
                //     template: "{controller=Admin}/{action=Create}"
                //     );
            });
           

        }
    }
}
