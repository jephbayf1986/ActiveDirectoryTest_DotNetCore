using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using testDotNetCoreAdApp.Data;
using testDotNetCoreAdApp.ErrorHandling;
using testDotNetCoreAdApp.Policies;

namespace testDotNetCoreAdApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string localHostOrigin = "_localHostDevOnly";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Data Context and Sql Server Service
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(Configuration["ConnectionStrings:Primary"]));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => 
                    {
                        options.Authority = Configuration["AzureAd:Instance"] + Configuration["AzureAd:TenantId"];
                        options.SaveToken = true;

                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidAudience = Configuration["AzureAd:ClientId"]
                        };
                    });

            services.AddAuthorization(options => options.AddPolicy(TestAdminPolicy.Name, TestAdminPolicy.Build));

            services.AddCors(options =>
            {
                options.AddPolicy(localHostOrigin, builder =>
                {
                    builder.WithOrigins("http://localhost:4200").AllowAnyHeader();
                });
            });

            services.AddHttpContextAccessor();

            services.AddSingleton<IMicrosoftGraphService, MicrosoftGraphService>();
            services.AddTransient<IClaimsTransformation, ClaimsTransformation>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        var error = context.Features.Get<IExceptionHandlerFeature>();

                        if (error != null)
                        {
                            ErrorHandler Response = new ErrorHandler(error.Error, true);

                            context.Response.StatusCode = Response.StatusCode;
                            context.Response.ContentType = "application/json";

                            await context.Response.WriteAsync(Response.Message.ToString(), Encoding.UTF8);
                        }
                    });
                });

                app.UseCors(localHostOrigin);
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            dbContext.Database.Migrate();

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseMvc();
        }
    }
}
