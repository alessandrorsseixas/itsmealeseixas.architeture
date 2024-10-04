using itsmealeseixas.architeture.utilities.SeedWorks.Interfaces;
using itsmealeseixas.architeture.utilities.Seedworks;
using itsmealeseixas.architeture.utilities.SeedWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itsmealeseixas.architeture.api.Config
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            //Architeture
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddScoped<osborn.app.architeture.api.SeedWorks.Interfaces.IUser, AspNetUserAdapter>();
            services.AddScoped<INotificator, Notificator>();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

 

            return services;
        }
    }
}
