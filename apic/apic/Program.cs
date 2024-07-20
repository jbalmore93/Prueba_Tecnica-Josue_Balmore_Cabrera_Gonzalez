using apic.Modelo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;


internal class Program
{
<<<<<<< HEAD

=======
>>>>>>> c3e1970557781cd35ecb238f0c9214c178d47ab1
    private static string ip = "http://localhost:5173";
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateSlimBuilder(args);
        
        builder.Services.AddCors(policy =>
        {
            options.AddPolicy("Cors", policy =>
            {
                policy.WithOrigins(ip)
                   .AllowAnyHeader()
                   .AllowAnyMethod();
            });
        });
        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.TypeInfoResolver = MyJsonContext.Default;
        });

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
     
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseCors("Cors");

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
[JsonSerializable(typeof(Usuario))]
[JsonSerializable(typeof(Ruleta))]
[JsonSerializable(typeof(ValidationProblemDetails))]
public partial class MyJsonContext : JsonSerializerContext
{
}
