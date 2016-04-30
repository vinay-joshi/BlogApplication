using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using SpecialSymbol.Models;
using SpecialSymbol.Models.Idenitty;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SpecialSymbol
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
             .AddJsonFile("config.json")
             .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);       
            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }
            builder.AddEnvironmentVariables();            
            Configuration = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<BlogDataContext>();
            services.AddTransient<FormatingService>();
            services.AddScoped<IdentityDataContext>();

            string identityConnectionString = "Data Source=localhost;Initial Catalog=AspNetBlog;Integrated Security=False;User Id=sa;Password=sql;MultipleActiveResultSets=True";
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<IdentityDataContext>(dbconfig =>
                dbconfig.UseSqlServer(identityConnectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDataContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {            
            //Add config files
            if (Configuration.Get<bool>("Debug"))
            {
                // show exception page when any run time exceptions come.
                app.UseDeveloperExceptionPage();

                //Show run time info to the users
                app.UseRuntimeInfoPage();

            }
            if (Configuration.Get<bool>("RecreateDatabase"))
            {
                var context = app.ApplicationServices.GetService<Models.BlogDataContext>();
                context.Database.EnsureDeleted();
                System.Threading.Thread.Sleep(2000);
                context.Database.EnsureCreated();
            }
            else
            {
                //Custome error page
                app.UseExceptionHandler("/home/error");
            }
            //use static file over the www root folder in this app
            app.UseStaticFiles();
            app.UseIdentity();
            app.UseMvc(router=>{
                router.MapRoute(
                   name: "Default",
                   template: "{controller}/{action}/{id?}",
                   defaults: new { controller = "Home", action = "Index" }
                   );
            });          
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
