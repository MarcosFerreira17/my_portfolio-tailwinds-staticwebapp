using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MWF.Blog.Domain.Services;
using MWF.Blog.Domain.Interfaces;
using MWF.Blog.Infraestructure.DataContext;
using MWF.Blog.Infraestructure.Repositories;
using FluentValidation.AspNetCore;
using System.Globalization;

namespace MWF.Blog.Host.Configurations;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<MWF.BlogDbContext>(options =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IMWF.BlogRepository, MWF.BlogRepository>();
        services.AddScoped<IMWF.BlogService, MWF.BlogService>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
    }

    public static void ConfigureCors(this IServiceCollection services) =>
    services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy", builder =>
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
    });
}
