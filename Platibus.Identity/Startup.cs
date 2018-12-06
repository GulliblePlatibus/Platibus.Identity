using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
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
using Platibus.Identity.Helpers;
using Platibus.Identity.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace Platibus.Identity
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _environment;
        
        public Startup(IHostingEnvironment env, IConfiguration config)
        {
            _configuration = config;
            _environment = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<WebAppConfiguration>(
                _configuration.GetSection(nameof(WebAppConfiguration)));
            
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

            var webAppConfig = _configuration.GetSection(nameof(WebAppConfiguration)).Get<WebAppConfiguration>();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(IdentityConfig.GetIdentityResources())
                .AddInMemoryApiResources(IdentityConfig.GetApiResources())
                .AddInMemoryClients(IdentityConfig.GetClients(webAppConfig.WebsiteUrl));
             
            services.AddTransient<IUserHandler, UserHandler>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IConnectionString, IdentityServerSQlConfiq>();
            
            services.AddCors();
            
            //Settings for Dapper fluentmap 
            // Multiple ID's
            // https://github.com/henkmollema/Dommel
            FluentMapper.Initialize(options =>
            {
                options.AddMap(new UserMap());
                options.ForDommel();
            });
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
                app.UseHsts();
            }

            var webAppConfig = _configuration.GetSection(nameof(WebAppConfiguration)).Get<WebAppConfiguration>();
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseCors(
                options => 
                    options.WithOrigins(webAppConfig.WebsiteUrl).AllowAnyMethod()
                    
            );
            
            app.UseMvc();
            
            
            
			app.UseIdentityServer();
        }
    }
}
