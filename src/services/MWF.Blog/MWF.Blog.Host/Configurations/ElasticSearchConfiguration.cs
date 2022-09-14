using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Serilog.Sinks.Elasticsearch;

namespace MWF.Blog.Host.Configurations;

public static class ElasticSearchConfiguration
{
    public static ElasticsearchSinkOptions ConfigureELS(IConfigurationRoot configuration, string env)
    {
        return new ElasticsearchSinkOptions(new Uri(configuration["ELKConfiguration:Uri"]))
        {
            //Must be renamed == AutoRegisterMWF.Blog, the project has a conflict with namespaces 
            AutoRegisterMWF.Blog = true,
            IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower()}-{env.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
        };
    }
}
