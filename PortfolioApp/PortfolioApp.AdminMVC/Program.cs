using PortfolioApp.AdminMVC.Mappers;
using PortfolioApp.AdminMVC.Service;
using PortfolioApp.AdminMVC.Services;
using PortfolioApp.Core.Config;
using PortfolioApp.Core.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.Configure<FileApiSettings>(builder.Configuration.GetSection("FileApiSettings"));

var dataApiUrl = builder.Configuration.GetValue<string>("DataApiUrl");
if (string.IsNullOrWhiteSpace(dataApiUrl))
{
    throw new InvalidOperationException("DataApiUrl is required in appsettings.json");
}
builder.Services.AddHttpClient("dataApi", c =>
{
    c.BaseAddress = new Uri(dataApiUrl);
});

var fileApiUrl = builder.Configuration.GetValue<string>("FileApiUrl");
if (string.IsNullOrWhiteSpace(fileApiUrl))
{
    throw new InvalidOperationException("FileApiUrl is required in appsettings.json");
}
builder.Services.AddHttpClient("fileApi", c =>
{
    c.BaseAddress = new Uri(fileApiUrl);
});

builder.Services.AddScoped<HomeService>();
builder.Services.AddScoped<IAboutMeService, AboutMeService>();
builder.Services.AddScoped<IBlogPostService, BlogPostService>();
builder.Services.AddScoped<IEducationService, EducationService>();
builder.Services.AddScoped<IExperienceService, ExperienceService>();
builder.Services.AddScoped<IPersonalInfoService, PersonalInfoService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IContactMessageService,ContactMessageService>();

builder.Services.AddAutoMapper(typeof(ViewModelMappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
