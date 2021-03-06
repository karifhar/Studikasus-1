using EnrollmentService.Data;
using EnrollmentService.Data.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using EnrollmentService.Helpers;
using PlatformService.SyncDataServices.Http;
using EnrollmentService.SyncronousDataService.Http;

namespace EnrollmentService
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        private readonly IWebHostEnvironment _env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (_env.IsProduction()) 
            {
                Console.WriteLine("---Using Db in kubernertes (mssql-2017)----");
                services.AddDbContext<AppDbContext>(opt => opt
                .UseSqlServer(Configuration.GetConnectionString("EnrollConn")));
            }
            else 
            {
                Console.WriteLine("---Using Database LocalConnection----");
                services.AddDbContext<AppDbContext>(opt => opt
                .UseSqlServer(Configuration.GetConnectionString("LocalConnection")));
            }


            // identity confiq
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();
            // JWT config
            var tokenSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(tokenSettingsSection);
            var tokenSettings = tokenSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(tokenSettings.Secret);


            services.AddAuthentication(src =>
            {
                src.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                src.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };

            });

            services.AddScoped<IStudent, StudentRepo>();
            services.AddScoped<ICourse, CourseRepo>();
            services.AddScoped<IEnrollment, EnrollmentRepo>();
            services.AddScoped<IUser, UserRepo>();
            services.AddHttpClient<IPaymentDataClient, HttpPaymentDataClient>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            services.AddControllers().AddNewtonsoftJson(options => 
            options.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddXmlDataContractSerializerFormatters();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EnrollmentService", Version = "v1" });
                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header menggunakan bearer token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                c.AddSecurityDefinition("Bearer", securitySchema);
                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {securitySchema,new[]{"Bearer"} }
                };
                c.AddSecurityRequirement(securityRequirement);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EnrollmentService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            DbInitializer.PrepPopulation(app, env.IsProduction());
        }
    }
}
