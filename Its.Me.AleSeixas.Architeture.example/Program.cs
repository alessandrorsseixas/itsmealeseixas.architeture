using itsmealeseixas.architeture.utilities.Helpers;
using itsmealeseixas.architeture.utilities.SeedWorks;
using Sentry.Profiling;
using Serilog;
using itsmealeseixas.architeture.api.Config;


var appDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}/log/";
var logFilePath = Path.Combine(appDirectory, "apilogStart.txt");

var builder = WebApplication.CreateBuilder(args);
// Configuração do Serilog
builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection("ApplicationSettings"));

var appToken = builder.Configuration.GetSection("ApplicationSettings:AppToken").Value;
var sentry = builder.Configuration.GetSection("ApplicationSettings:Sentry").Value;
var project = builder.Configuration.GetSection("ApplicationSettings:Project").Value;
var corsOrigins = UtilsHelpers.Decrypt(builder.Configuration.GetSection("ApplicationSettings:CorsOrigins").Value, appToken);
var corsPolicy = UtilsHelpers.Decrypt(builder.Configuration.GetSection("ApplicationSettings:CorsPolicy").Value, appToken);
var databaseType = UtilsHelpers.Decrypt(builder.Configuration.GetSection("ApplicationSettings:DatabaseType").Value, appToken);
var connectionString = string.Empty;
var sqlServerConnection = UtilsHelpers.Decrypt(builder.Configuration.GetConnectionString("SqlServerConnection"), appToken);
var postgreslConnectionString = UtilsHelpers.Decrypt(builder.Configuration.GetConnectionString("PostgreslConnectionString"), appToken);


builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
    .WriteTo.File(logFilePath)
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(ctx.Configuration));
// Add services to the container.

// Configuração do Sentry
builder.WebHost.UseSentry(o =>
{
    o.Dsn = UtilsHelpers.Decrypt(sentry, appToken);
    o.Debug = true;
    o.TracesSampleRate = 1.0;
    o.ProfilesSampleRate = 1.0;
    o.AddIntegration(new ProfilingIntegration(TimeSpan.FromMilliseconds(500)));
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerConfig();


var app = builder.Build();

SentrySdk.CaptureMessage($"Hello Sentry App:{project}");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
