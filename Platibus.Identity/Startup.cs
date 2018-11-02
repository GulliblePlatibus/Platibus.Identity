using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IdentityServer4.Test;
using IdentityModel;
using Platibus.Identity.Handlers;
using Platibus.Identity.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace Platibus.Identity
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
            //For fun swagger settings...
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Management.API Auth", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "application",
                    Description = "This API uses the Management.API login Oauth2 Client Credentials flow",
                    TokenUrl = "https://qa-auth-management-identity.azurewebsite.net/connect/token",
                    Scopes = new Dictionary<string, string> { { "scope.fullacces", "Acces to all api-endpoints" } }
                });
                c.SwaggerDoc("v1", new Info { Title = "Management Backend", Version = "v1", Description = "Management API for use with prior agreement" });
            });
            
			services.AddMvc();

			services.AddIdentityServer()
					.AddDeveloperSigningCredential()
			        .AddInMemoryIdentityResources(IdentityConfig.GetIdentityResources())
					.AddInMemoryApiResources(IdentityConfig.GetApiResources())
					.AddInMemoryClients(IdentityConfig.GetClients())
			        .AddTestUsers(IdentityConfig.GetUsers());
             
            services.AddTransient<ICreateUserHandler,CreateUserHandler>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IConnectionString, IdentityServerSQlConfiq>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            //Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            //Enabled API to deliver swagger UI on http://{serverUrl}/swagger;
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
            });
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
            
			//app.UseMvcWithDefaultRoute();

			app.UseIdentityServer();
        }
    }
}
