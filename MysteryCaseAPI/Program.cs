using FastEndpoints;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MysteryCaseAPI.Hubs;
using MysteryCaseApplication.Interfaces;
using MysteryCaseApplication.Querries.Cases;
using MysteryCaseInfrastructure;
using MysteryCaseInfrastructure.Interface;
using MysteryCaseInfrastructure.Services;
using MysteryCaseShared.Mapping;
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

MappingConfig.Configure();
builder.Services.AddMapster();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<MysteryCaseDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<ILeaderboardService, LeaderboardService>();
builder.Services.AddSingleton<IDapperContext, DapperContext>();

var hangfireConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSqlServerStorage(hangfireConnectionString, new SqlServerStorageOptions
    {
        QueuePollInterval = TimeSpan.FromSeconds(15),
        SchemaName = "Hangfire",
        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
        UseRecommendedIsolationLevel = true,
        DisableGlobalLocks = true
    })
);

builder.Services.AddHangfireServer();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<GetCaseListCommand>());

builder.Services.AddFusionCache();

builder.Services.AddFastEndpoints();

builder.Services.AddSignalR();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  
}


app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new IDashboardAuthorizationFilter[] { }
});


var leaderboardService = app.Services
    .CreateScope()
    .ServiceProvider
    .GetRequiredService<ILeaderboardService>();

RecurringJob.AddOrUpdate(
    "calculate-leaderboard",
    () => leaderboardService.CalculateLeaderboardAsync(CancellationToken.None),
    Cron.Minutely());


app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<CaseHub>("/casehub");
});

app.UseFastEndpoints();


app.Run();
