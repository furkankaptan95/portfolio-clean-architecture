using Microsoft.Extensions.FileProviders;
using PortfolioApp.FileAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

app.UseCors("AllowAllOrigins");

app.Use(async (context, next) =>
{
    if (context.Request.Method == "OPTIONS") // Eðer gelen istek "OPTIONS" ise
    {
        context.Response.StatusCode = 200; // HTTP 200 OK döndür
        await context.Response.CompleteAsync(); // Yanýtý tamamla ve iþlem yapmadan çýk
        return;
    }
    await next(); // Diðer middleware'lere devam et
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
if (!Directory.Exists(uploadsPath))
{
    Directory.CreateDirectory(uploadsPath);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "uploads")),
    RequestPath = "/uploads"
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
