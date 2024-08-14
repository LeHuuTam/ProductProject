
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductManagement.Commands;
using ProductManagement.Data;
using ProductManagement.Queries;
using ProductManagement.Repositories;
using ProductManagement.Services;
using ProductManagement.Validators;
using System.Text;

namespace ProductManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            //Cors
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });
            //DB
            builder.Services.AddDbContext<ProductManagementDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

            //JWT
            var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            //DI
            builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddTransient<IValidator<CreateProductCommand>, CreateProductValidator>();
            builder.Services.AddTransient<IValidator<UpdateProductCommand>, UpdateProductValidator>();
            builder.Services.AddTransient<IValidator<DeleteProductCommand>, DeleteProductValidator>();
            builder.Services.AddTransient<IValidator<GetByIdProductQuery>, GetByIdProductValidator>();
            builder.Services.AddTransient<IValidator<FilterByPriceProductQuery>, FilterByPriceProductValidator>();
            builder.Services.AddTransient<IValidator<FilterByQuantityProductQuery>, FilterByQuantityProductValidator>();
            builder.Services.AddTransient<IValidator<LoginQuery>, LoginValidator>();
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowAllOrigins");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
