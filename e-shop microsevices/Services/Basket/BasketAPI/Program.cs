using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

//add services to the container
builder.Services.AddCarter();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);  //for CQRS
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>)); //for validation behaviour
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.Schema.For<ShoppingCart>().Identity(x => x.Username);
}).UseLightweightSessions();


builder.Services.AddScoped<IBasketRepository , BasketRepository>();

//FOR CACHE (REDIS) I HAVE TO INSTALL REDIS ON WINDOWS AND THEN IT WILL BE
//RUNNING ON LOCAL HOST WHICH I HAVE GIVEN A CONNECTION...BUT
//I AM JUST WORKING WITH THE DATABSE..NO DOCKER NO IMAGE NOTHING..SO JUST COMMENTING
//OUT THE REDIS PART

//builder.Services.Decorate<IBasketRepository, CashedBasketRepository>();

//builder.Services.AddStackExchangeRedisCache(options =>
//{
//    options.Configuration = builder.Configuration.GetConnectionString("Redis");
//});


builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("Database")!);


var app = builder.Build();


//Configure the https request pipeline

app.MapCarter();

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });


app.Run();
