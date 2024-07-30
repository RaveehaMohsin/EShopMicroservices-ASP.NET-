using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;
using OrderingAPI;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container

builder.Services
    .AddApplicationService()
    .AddInfrastrutureService(builder.Configuration)
    .AddApiServices();


var app = builder.Build();

//Configure https requests pipeline

app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.Run();
