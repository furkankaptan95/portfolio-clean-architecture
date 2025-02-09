using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Business_Logic.Services;
using PortfolioApp.Application.Use_Cases.Auth.Handlers;
using PortfolioApp.Core.Interfaces;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

var builder = WebApplication.CreateBuilder(args);

var authDbConnectionString = builder.Configuration.GetConnectionString("AuthDb");
builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(authDbConnectionString));

var dataDbConnectionString = builder.Configuration.GetConnectionString("DataDb");
builder.Services.AddDbContext<DataDbContext>(options =>
    options.UseSqlServer(dataDbConnectionString));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterHandler).Assembly));
builder.Services.AddScoped<IAuthService,AuthService>();
// Add services to the container.

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
using var context = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
context.Database.EnsureCreated();

app.Run();
