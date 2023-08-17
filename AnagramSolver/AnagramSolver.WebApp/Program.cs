using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DbFirst;
using AnagramSolver.EF.DbFirst.Entities;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using MyConfiguration = AnagramSolver.WebApp.MyConfiguration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IWordServer, WordServer>();

builder.Services.AddScoped<ISearchLogService, LogService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IWordLogService, LogService>();

builder.Services.AddScoped<IWordLogRepository, WordLogRepo>();
builder.Services.AddScoped<ISearchLogRepository, SearchLogRepo>();
builder.Services.AddScoped<IWordRepository, WordRepo>();
builder.Services.AddScoped<IPartOfSpeechRespository, PartOfSpeechRepo>();

builder.Services.AddScoped<MyConfiguration>();


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
builder.Services.AddDbContext<AnagramSolverDataContext>();

builder.Services.AddHttpClient();

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

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.UseSession();

app.Run();
