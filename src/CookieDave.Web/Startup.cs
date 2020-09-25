using System;
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
                //options.AddPolicy(AtLeastTier1, p => p.RequireRole(Tier1, Tier2, Admin));

                //options.AddPolicy(AtLeastTier2, p => p.RequireRole(Tier2, Admin));

                //options.AddPolicy(AdminOnly, p => p.RequireRole(Admin));

                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    // user has to be authenticated to view a page by default
                    .RequireAuthenticatedUser()
                    .Build();
            });

            // set all page Policy rules
            services.AddRazorPages(x =>
            {
                x.Conventions.AllowAnonymousToPage("/Index");
                x.Conventions.AllowAnonymousToPage("/Anonymous");
                x.Conventions.AllowAnonymousToPage("/ThrowException");
                x.Conventions.AllowAnonymousToPage("/Error");
                x.Conventions.AllowAnonymousToPage("/Account/Login");
                x.Conventions.AllowAnonymousToPage("/Account/Logout");

                //x.Conventions.AuthorizePage("/Tier1RoleNeeded", AtLeastTier1);
                //x.Conventions.AuthorizePage("/Tier2RoleNeeded", AtLeastTier2);
                //x.Conventions.AuthorizePage("/AdminRoleNeeded", AdminOnly);
            });

            services.AddHttpContextAccessor();
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
