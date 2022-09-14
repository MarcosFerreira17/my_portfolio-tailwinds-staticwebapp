using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MWF.Projects.Domain.Services;
using MWF.Projects.Domain.Interfaces;
using MWF.Projects.Infraestructure.DataContext;
using MWF.Projects.Infraestructure.Repositories;
using FluentValidation.AspNetCore;
using System.Globalization;

namespace MWF.Projects.Host.Configurations;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<MWF.ProjectsDbContext>(options =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IMWF.ProjectsRepository, MWF.ProjectsRepository>();
        services.AddScoped<IMWF.ProjectsService, MWF.ProjectsService>();
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
