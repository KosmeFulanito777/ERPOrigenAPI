using ERPOrigenAPI.Data;
using ERPOrigenAPI.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information() 
    .WriteTo.Console()          
    .WriteTo.File("Logs/api-log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("MSSQLServerDaniel")
    )
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var logger = context.RequestServices
            .GetRequiredService<ILogger<Program>>();

        var exception = context.Features
            .Get<IExceptionHandlerFeature>()?.Error;

        logger.LogError(exception, "Error no controlado");

        context.Response.StatusCode = 500;

        await context.Response.WriteAsJsonAsync(new
        {
            mensaje = "Error interno del servidor"
        });
    });
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
