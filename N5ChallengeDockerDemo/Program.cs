using Microsoft.EntityFrameworkCore;
using N5ChallengeDockerDemo.Data;
using N5ChallengeDockerDemo.Interfaces;
using Nest;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IPermissionsRepository, PermissionsRepository>();

/* DATABASE CONTEXT DEPENDENCY INJECTION */
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
var connectionString = $"Data Source={dbHost}; Initial Catalog={dbName}; User ID=sa;Password={dbPassword};TrustServerCertificate=true;";
/* ==================================== */

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

// Elasticsearch configuration
var url = Environment.GetEnvironmentVariable("ELASTIC_URL");                //builder.Configuration["elasticsearch:url"];
var defaultIndex = Environment.GetEnvironmentVariable("ELASTIC_INDEX");   // builder.Configuration["elasticsearch:index"];

var settings = new ConnectionSettings(new Uri(url)).DefaultIndex(defaultIndex);

var client = new ElasticClient(settings);

builder.Services.AddSingleton<IElasticClient>(client);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configure serilog
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File("logs.txt", rollingInterval: RollingInterval.Day);
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
