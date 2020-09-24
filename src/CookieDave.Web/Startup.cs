using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static CookieDave.Web.Data.CDRole;
using static CookieDave.Web.Data.CDPolicy;

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
                // using a class with constants instead of enum to avoid ToString()

                options.AddPolicy(Tier1Policy, p => p.RequireRole(Tier1Role, Tier2Role, AdminRole));

                options.AddPolicy(Tier2Policy, p => p.RequireRole(Tier2Role, AdminRole));

                options.AddPolicy(AdminPolicy, p => p.RequireRole(AdminRole));

                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    // user has to be authenticated to view a page by default
                    .RequireAuthenticatedUser()
                    .Build();
            });

            // set all page rules
            services.AddRazorPages(x =>
            {
                x.Conventions.AllowAnonymousToPage("/Index");
                x.Conventions.AllowAnonymousToPage("/Anonymous");
                x.Conventions.AllowAnonymousToPage("/ThrowException");
                x.Conventions.AllowAnonymousToPage("/Error");
                x.Conventions.AllowAnonymousToPage("/Account/Login");
                x.Conventions.AllowAnonymousToPage("/Account/Logout");

                x.Conventions.AuthorizePage("/Tier1RoleNeeded", Tier1Policy);

                x.Conventions.AuthorizePage("/Tier2RoleNeeded", Tier2Policy);

                x.Conventions.AuthorizePage("/AdminRoleNeeded", AdminPolicy);
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
