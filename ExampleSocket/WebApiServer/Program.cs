
using Microsoft.Extensions.FileProviders;

namespace WebApiServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

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