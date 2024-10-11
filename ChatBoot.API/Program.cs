using Chatboot.Infrastructure.Persistence;
using Chatboot.Infrastructure.Repositories;
using OrderManagementService.API.Middleware;
using OrderManagementService.Application.Services.Whatsapp;
using OrderManagementService.Core.Interfaces.Repositories;
using OrderManagementService.Core.Interfaces.Whatsapp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using OrderManagementService.Core.Interfaces.Services;
using OrderManagementService.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApiVersioning(options => {
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(options => {
    options.GroupNameFormat = "'v'VVV"; 
    options.SubstituteApiVersionInUrl = true; 
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen( options => {

    options.SwaggerDoc("v1", new () { Version = "v1", Title = "ChatBoot API v1", Description = "API versión 1.0" });

    // Configuration for Swagger to recognize versioning in the controllers.
    options.DocInclusionPredicate((version, apiDescription) =>
    {
        var versions = apiDescription.GroupName?.Split(',');
        return versions != null && versions.Contains(version);
    });
});

builder.Services.AddDbContext<MessageDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency Inyection
builder.Services.AddScoped<IWhatsappService, WhatsappService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


var app = builder.Build();

//register middleware
app.UseMiddleware<CustomExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(swaggerUIOptions => {
        swaggerUIOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "ChatBoot API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
