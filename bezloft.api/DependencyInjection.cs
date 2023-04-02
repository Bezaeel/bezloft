using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace bezloft.api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddSwaggerGen(c => 
        {
            c.AddServer(new OpenApiServer
            {
                Url = builder.Configuration["Swagger:Server"],
                Description = builder.Environment.EnvironmentName,
            });
                
            c.SwaggerDoc("v1", new OpenApiInfo { Title = $"{builder.Environment.ApplicationName}", Version = builder.Configuration["Swagger:Version"] });
            var location = Assembly.GetEntryAssembly()!.Location;
            c.IncludeXmlComments(Path.ChangeExtension(location, "xml"));
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });
            c.CustomSchemaIds(x => x.FullName);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });
        
        services.AddCors(options =>
        {
            options.AddPolicy("corsPolicy", corsOption =>
            {
                corsOption.AllowAnyMethod();
                corsOption.AllowAnyHeader();
                corsOption.AllowCredentials();
                corsOption.SetIsOriginAllowed(origin =>
                {
                    if (string.IsNullOrWhiteSpace(origin)) return false;
                    if (origin.ToLower().StartsWith("http://localhost") && builder.Environment.IsDevelopment()) return true;
                    return false;
                }); // allow any origin
            });
        });

        return services;
    }
}