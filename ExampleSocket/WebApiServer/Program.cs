
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using WebApiServer.Data;
using WebApiServer.Data.Entities.Identity;
using WebApiServer.Mapper;
using FluentValidation.AspNetCore;

namespace WebApiServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppEFContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
            //options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));
            // Add services to the container.

            builder.Services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<AppEFContext>().AddDefaultTokenProviders();


            builder.Services.AddAutoMapper(typeof(AppMapProfile));

            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Program>());

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            //}
            
            var dir = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            if(!Directory.Exists(dir)) //якщо папка не існує я її створюю
                Directory.CreateDirectory(dir);

            //Надаю доступ до папки на север
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider=new PhysicalFileProvider(dir),
                RequestPath = "/images"
            });

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}