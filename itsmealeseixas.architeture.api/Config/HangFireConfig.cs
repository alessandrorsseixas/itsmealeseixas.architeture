using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Hangfire;

namespace itsmealeseixas.architeture.api.Config
{
    public static class HangFireConfig
    {
        public static IServiceCollection HangfireConfig(this IServiceCollection services, string connectionString)
        {

            services.AddHangfire(configuration => configuration
             .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
             .UseSimpleAssemblyNameTypeSerializer()
             .UseRecommendedSerializerSettings()
             .UseSqlServerStorage(connectionString));
            return services;
        }

        public static IApplicationBuilder UseHangFireServerConfig(this IApplicationBuilder app)
        {
            app.UseHangfireDashboard();
            return app;
        }
    }
}
