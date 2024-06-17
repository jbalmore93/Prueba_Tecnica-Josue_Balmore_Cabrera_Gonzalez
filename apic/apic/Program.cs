using apic.Modelo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;


internal class Program
{
    
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateSlimBuilder(args);
        
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("Cors", app =>
            {
                app.AllowAnyOrigin()
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