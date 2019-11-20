using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ASPNetCoreApplicationPractice.Models;
using ASPNetCoreApplicationPractice.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ASPNetCoreApplicationPractice
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>
            (
            options => options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection"))
            );
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 3;
                //options.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<AppDbContext>();
            services.AddAuthentication()
              .AddGoogle(options =>
              {
                  options.ClientId = "196275387351-has575ff9m45ka8ld17s0qtps42jkmtt.apps.googleusercontent.com";
                  options.ClientSecret = "MoYpnVChG8VeDiGCw4XYrLH0";
                  //options.UserInformationEndpoint= "https://www.googleapis.com/oauth2/v1/certs";
                  options.UserInformationEndpoint = "https://www.googleapis.com/oauth2/v2/userinfo";
                  options.ClaimActions.Clear();
                  options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                  options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                  options.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "given_name");
                  options.ClaimActions.MapJsonKey(ClaimTypes.Surname, "family_name");
                  options.ClaimActions.MapJsonKey("urn:google:profile", "link");
                  options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
              });
            //services.Configure<IdentityOptions>(options =>
            //{
            //    options.Password.RequiredLength = 10;
            //    options.Password.RequiredUniqueChars = 3;
            //    options.Password.RequireNonAlphanumeric = false;
            //});
            //Start Authorization 
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            }).AddXmlSerializerFormatters();
            //End Authorization
            //services.AddSingleton<IEmployeeRepository, BOEmployeeRepository>();

            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
            services.AddSingleton<IAuthorizationHandler, CanEditOnlyOtherAdminRolesAndClaimsHandler>();
            services.AddSingleton<IAuthorizationHandler, SuperAdminHandler>();

            //Configure Claim Policy for permission based authorization
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("DeleteRolePolicy",
            //        policy => policy.RequireClaim("Delete Role")
            //        .RequireClaim("Create Role")
            //        );
            //});
            //Role Claim Policy
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("AdminRolePolicy",
            //        policy => policy.RequireRole("Admin","User")
            //       );
            //});
            services.AddAuthorization(options =>
            {
                options.AddPolicy("DeleteRolePolicy",
                    policy => policy.RequireClaim("Delete Role"));
                //Assertion in Edit Role Policy
                //options.AddPolicy("EditRolePolicy",
                //    policy => policy.RequireAssertion(context =>
                //    context.User.IsInRole("Admin") &&
                //    context.User.HasClaim(claim => claim.Type == "Edit Role" && claim.Value == "true") ||
                //    context.User.IsInRole("Super Admin")
                //    ));
                //Custom Authorization Policy
                options.AddPolicy("EditRolePolicy",
                    policy => policy.AddRequirements(new ManageAdminRolesAndClaimsRequirement()));

                //options.AddPolicy("EditRolePolicy",
                //    policy => policy.RequireClaim("Edit Role", "true"));
                options.AddPolicy("AdminRolePolicy",
                    policy => policy.RequireRole("Admin"));
            });
            //Set Application Denied Configuration
            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.AccessDeniedPath = new PathString("/Administration/AccessDenied");
            //});
          
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
                ///Global Error Exception
                app.UseExceptionHandler("/Error");
                ///Centralized 404 Error
                // app.UseStatusCodePages(); 
                //app.UseStatusCodePagesWithRedirects("/Error/{0}");
                //Re-executes the pipeline and returns the original status code (404 for example);
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }
            app.UseStaticFiles();
            //app.UseMvcWithDefaultRoute();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            //app.UseMvc();
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
