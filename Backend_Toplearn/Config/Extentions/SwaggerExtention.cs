using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Backend_Toplearn.Config.Extentions
{
    public static class SwaggerExtention
    {
        public static IServiceCollection AddOurSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API",
                    Description = "این API طراحی شده توسط سعید لطفی پور با Core 3.1.7",
                    Contact = new OpenApiContact()
                    {
                        Name = "saeed lotfi pour",
                        Url = new Uri("http://wwww.sayyehban.ir")
                    }
                });
                var securitySchema = new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                c.AddSecurityDefinition("Bearer", securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement();
                securityRequirement.Add(securitySchema, new[] {"Bearer"});
                c.AddSecurityRequirement(securityRequirement);
            });
            return services;
        }
    }
}