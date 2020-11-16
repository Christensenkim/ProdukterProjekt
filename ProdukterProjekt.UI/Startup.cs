using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using ProdukterProjekt.Core.ApplicationService;
using ProdukterProjekt.Core.ApplicationService.Services;
using ProdukterProjekt.Core.DomainService;
using ProdukterProjekt.Infrastructure.Data;
using ProdukterProjekt.UI.Helpers;

namespace ProdukterProjekt.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            byte[] secretBytes = new byte[40];
            Random rand = new Random();
            rand.NextBytes(secretBytes);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretBytes),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });

            services.AddDbContext<ProductContext>(
                opt => opt.UseSqlite("Data Source=Product.db"));

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddSingleton<IAuthenicationHelper>(new AuthenicationHelper(secretBytes));

            services.AddCors(opt =>
                opt.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                })
            );

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

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var ctx = scope.ServiceProvider.GetService<ProductContext>();

                    ctx.Database.EnsureDeleted();
                    ctx.Database.EnsureCreated();

                    ctx.SeedDB();
                    ctx.SaveChanges();
                }
            }else
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var ctx = scope.ServiceProvider.GetService<ProductContext>();

                    //ctx.Database.EnsureDeleted();
                    ctx.Database.EnsureCreated();

                    ctx.SeedDB();
                    ctx.SaveChanges();
                }
            }

            app.UseSwagger();

            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "swagger to be changed");
                opt.RoutePrefix = "";
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });




        }


    }
}
