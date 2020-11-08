using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using ProdukterProjekt.Core.ApplicationService;
using ProdukterProjekt.Core.ApplicationService.Services;
using ProdukterProjekt.Core.DomainService;
using ProdukterProjekt.Core.Entity;
using ProdukterProjekt.Infrastructure.Data;
using ProdukterProjekt.UI.Helpers;

namespace ProdukterProjekt.UI
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
            

            services.AddDbContext<ProductContext>(
                opt => opt.UseSqlite("Data Source=Product.db"));

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();

            services.AddControllers();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = JWTSecurityKey.Key,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Product Swag",
                        Description = "Product Swag to be changed",
                        Version = "v1"
                    });
                //var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var filePath = Path.Combine(AppContext.BaseDirectory, fileName);
                //opt.IncludeXmlComments(filePath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var ctx = scope.ServiceProvider.GetService<ProductContext>();
                    //ctx.Database.EnsureDeleted();
                    ctx.Database.EnsureCreated();

                    string password = "1234";
                    byte[] passwordHashUser, passwordSaltUser, passwordHashAdmin, passwordSaltAdmin;

                    CreatePasswordHash(password, out passwordHashUser, out passwordSaltUser);
                    CreatePasswordHash(password, out passwordHashAdmin, out passwordSaltAdmin);


                    var user1 = ctx.Add(new User()
                    {
                        userName = "User",
                        passwordHash = passwordHashUser,
                        passwordSalt = passwordSaltUser,
                        isAdmin = false
                    });
                    var user2 = ctx.Add(new User()
                    {
                        userName = "Admin",
                        passwordHash = passwordHashAdmin,
                        passwordSalt = passwordSaltAdmin,
                        isAdmin = true
                    });
                    var product1 = ctx.Add(new Product
                    {
                        Name = "htrhpr",
                        Color = "shtrh",
                        CreatedDate = DateTime.Now,
                        Price = 2.0,
                        Ptype = "sgnfgnf"
                    });

                    ctx.SaveChanges();
                }
            }



            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "swagger to be changed");
                opt.RoutePrefix = "";
            });

            app.UseAuthentication();
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using( var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
