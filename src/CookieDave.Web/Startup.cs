using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CookieDave.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            services.AddAuthorization(options =>
            {
                // all authenticated users are in the role of User (set in Login.cshtml.cs)
                options.AddPolicy("Admin", p => p.RequireRole("Admin"));

                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    // user has to be authenticated to view a page by default
                    .RequireAuthenticatedUser()
                    .Build();
            });

            // set all page rules (except default authenticated User role)
            services.AddRazorPages(x =>
            {
                x.Conventions.AllowAnonymousToPage("/Index");
                x.Conventions.AllowAnonymousToPage("/Privacy");
                x.Conventions.AllowAnonymousToPage("/ThrowException");
                x.Conventions.AllowAnonymousToPage("/Error");
                x.Conventions.AllowAnonymousToPage("/Account/Login");
                x.Conventions.AllowAnonymousToPage("/Account/Logout");

                // all authenticated users have the User role so this is not needed
                //x.Conventions.AuthorizePage("/UserRoleNeeded", "User");

                x.Conventions.AuthorizePage("/AdminRoleNeeded", "Admin");
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Strict });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
