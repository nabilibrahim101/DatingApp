using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) //b- we are going to haveily use Dependency Injection
        {
            // Anything we add as a service is available to be injected into any other part of our application.
            services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            // we need to add cores as a service, so that we make it available into our HTTP pipeline. [we are doing this because the values are not showing in the browser for securtiy issues.]
            // once it is here we can use it in our pipeline (below)
            services.AddCors();
            /*
                And inside the startup class we're gonna add this as a service and when we do this it is going to be available
                for injection throughout the rest of our application services.
                the order is not important in "ConfigureServices" but important in "Configure"
             */

            /*
             services.AddSingleton: means create a single instance of our repository throughout the application.
              It creates the instance for the first time an then reuses the same object in all of the calls.
             might cause issues with concurrent requests.
            */
             /*
             services.AddTransient:  useful for lightweight stateless services because
            these are created each time they are requested.
            So each time a request comes from our repository then a new instance of that repository is created so
            great for lightweight state the services but not really suitable for what we're doing.
            
            services.AddScoped: it is in the middle of the 2 above:
            means that the service is created once per request within the scope and it's equivalent to a singleton but in the current
            scope itself for example it creates one instance for each HTTP request but it uses the same instance
            of a cause within the same web request it's suitable for auth repository that we're creating here.
            */
            //with this below line, it will make it available for injection and specifically in our controllers.
            //we'll be injecting the "IAuthRepository" into our controllers and then it gets the implementation logic from the "AuthRepository".
            //and this means the code inside our controllers would never need to change even if we change the implementation

            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                            .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // app.UseHsts();
            }

            // app.UseHttpsRedirection();
            // The order is important here, it should be before app.UseMVC()
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseMvc(); 
            // MVC is the framework we aere using and it is Middlewhere.
            // Middleware is the software that connects network based requests generated by a client to the back end data the client is requresting.
            // So this app.UseMvc() sits in between our client request and our API end points 
            // and it routes our request to the correct controller

            

        }
    }
}
