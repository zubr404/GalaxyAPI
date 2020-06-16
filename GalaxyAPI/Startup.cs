using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GalaxyAPI.Filters;
using GalaxyAPI.Models;
using GalaxyAPI.Repositories;
using GalaxyAPI.Services.Authentication;
using GalaxyAPI.Services.Translation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace GalaxyAPI
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
            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataBaseContext>(options => options.UseNpgsql(connection));
            services.AddTransient<UserRepository>();
            services.AddTransient<ProjectRepository>();
            services.AddTransient<ProjectUserRepository>();
            services.AddTransient<WorkRepository>();
            services.AddTransient<SourceRepository>();
            services.AddTransient<TargetRepository>();
            services.AddTransient<WorkFile>();

            services.AddMvc(options => options.Filters.Add(typeof(ValidModelActionFilter)));
            services.AddMvc(options => options.Filters.Add(typeof(ExceptFilterAttribute)));


            var signingKey = new SigningSymmetricKey(AuthOptions.KEY);
            services.AddSingleton<IJwtSigningEncodingKey>(signingKey);

            services.AddControllers();

            const string jwtSchemeName = "JwtBearer";
            var signingDecodingKey = (IJwtSigningDecodingKey)signingKey;
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = jwtSchemeName;
                    options.DefaultChallengeScheme = jwtSchemeName;
                })
                .AddJwtBearer(jwtSchemeName, jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.ISSUER,
                        ValidateAudience = true,
                        ValidAudience = AuthOptions.AUDIENCE,
                        ValidateLifetime = true,
                        IssuerSigningKey = signingDecodingKey.GetKey(),
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.FromMinutes(AuthOptions.LIFETIME)
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
