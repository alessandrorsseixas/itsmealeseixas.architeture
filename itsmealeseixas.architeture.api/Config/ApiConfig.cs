using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itsmealeseixas.architeture.api.Config
{
    public static class ApiConfig
    {
        public static IServiceCollection WebApiConfig(this IServiceCollection services)
        {
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
            services.AddResponseCompression(options => { options.Providers.Add<GzipCompressionProvider>(); });


            services.AddMvcCore().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>()
            .AddScoped<IUrlHelper>(x => x.GetRequiredService<IUrlHelperFactory>()
            .GetUrlHelper(x.GetRequiredService<IActionContextAccessor>().ActionContext));


            //.AddJsonOptions(opcoes =>
            // {
            //     opcoes.SerializerSettings.NullValueHandling =
            //         NullValueHandling.Ignore;
            // }); ;

            //services.AddOData();
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                                new HeaderApiVersionReader("x-api-version"),
                                                                new MediaTypeApiVersionReader("x-api-version"));
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;

            });

            return services;
        }


        public static IServiceCollection CorsConfig(this IServiceCollection services, string corsOrigins)
        {

            if (corsOrigins != null)
            {
                services.AddCors(options =>
                {
                    options.AddPolicy(name: "AllowAll",
                        builder =>
                        {
                            builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                        });
                    options.AddPolicy(name: "Production",
                        builder =>
                        {
                            builder.AllowCredentials().WithOrigins(corsOrigins.Split(";")).AllowAnyHeader().AllowAnyMethod();
                        });


                });

            }
            return services;
        }






    }
}
