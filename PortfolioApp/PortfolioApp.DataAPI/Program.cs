using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Business_Logic.Services;
using PortfolioApp.Application.Use_Cases.AboutMe.Handlers;
using PortfolioApp.Core.Interfaces;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

var builder = WebApplication.CreateBuilder(args);

var dataDbConnectionString = builder.Configuration.GetConnectionString("DataDb");
builder.Services.AddDbContext<DataDbContext>(options =>
    options.UseSqlServer(dataDbConnectionString));

// Add services to the container.

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateAboutMeHandler).Assembly));
builder.Services.AddScoped<IAboutMeService, AboutMeService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
using var context = scope.ServiceProvider.GetRequiredService<DataDbContext>();
context.Database.EnsureCreated();

app.Run();
