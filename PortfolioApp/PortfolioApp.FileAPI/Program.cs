using Microsoft.Extensions.FileProviders;
using PortfolioApp.FileAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

app.UseCors("AllowAllOrigins");

app.Use(async (context, next) =>
{
    if (context.Request.Method == "OPTIONS") // E�er gelen istek "OPTIONS" ise
    {
        context.Response.StatusCode = 200; // HTTP 200 OK d�nd�r
        await context.Response.CompleteAsync(); // Yan�t� tamamla ve i�lem yapmadan ��k
        return;
    }
    await next(); // Di�er middleware'lere devam et
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
