using FastEndpoints;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MysteryCaseApplication.Querries.Cases;
using MysteryCaseInfrastructure;
using MysteryCaseShared.Mapping;

var builder = WebApplication.CreateBuilder(args);


MappingConfig.Configure();
builder.Services.AddMapster();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MysteryCaseDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<GetCaseListCommand>());

builder.Services.AddFastEndpoints();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseFastEndpoints();
app.MapControllers();
app.Run();
