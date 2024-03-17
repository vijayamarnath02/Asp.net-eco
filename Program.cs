
using E_Commers.DataBase;
using E_Commers.Repositer;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<DatabaseConnection>(options =>
         options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddScoped<IUserCreateData, UserCreateData>();
        builder.Services.AddScoped<IProductData, ProductData>();
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseCors(policy =>
        {
            policy
                   .WithOrigins("http://localhost:4200","http://localhost:8101")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}