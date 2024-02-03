using Microsoft.EntityFrameworkCore;
using PlatformService.Data;

namespace PlatformService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddDbContext<AppDbContext>(opt =>
            opt.UseInMemoryDatabase("InMem"));

        builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        PrepDb.PrepPopulation(app);

        app.MapControllers();

        app.Run();
    }
}

