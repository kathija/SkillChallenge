using InterviewTest.Models;
using InterviewTest.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add DbContext
builder.Services.AddDbContext<PersonContext>(opt => opt.UseInMemoryDatabase("People"), ServiceLifetime.Transient, ServiceLifetime.Transient);
builder.Services.AddDbContext<PlaceContext>(opt => opt.UseInMemoryDatabase("Places"), ServiceLifetime.Transient, ServiceLifetime.Transient);
builder.Services.AddDbContext<ThingContext>(opt => opt.UseInMemoryDatabase("Things"), ServiceLifetime.Transient, ServiceLifetime.Transient);

builder.Services.AddTransient<IPeopleService, PeopleService>();
builder.Services.AddTransient<IPlaceService, PlaceService>();
builder.Services.AddTransient<IThingsService, ThingsService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use((context, next) =>
{
    context.Response.Headers.Remove("X-Powered-By");
    return next();
});

app.UseStaticFiles();
app.UseRouting();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");




app.Run();
