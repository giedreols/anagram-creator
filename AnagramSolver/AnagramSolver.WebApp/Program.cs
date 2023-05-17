using AnagramSolver.BusinessLogic.DictionaryActions;
using AnagramSolver.Cli;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Data;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AnagramSolverWebAppContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("AnagamSolverWebAppContext") ?? throw new InvalidOperationException("Connection string 'AnagamSolverWebAppContext' not found.")));

// Add services to the container.
builder.Services.AddScoped<IFileReader, FileReader>();
builder.Services.AddScoped<IWordRepository, WordDictionary>();
builder.Services.AddScoped<IAnagramGenerator, AnagramSolver.BusinessLogic.AnagramActions.AnagramGenerator>();
builder.Services.AddScoped<MyConfiguration>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

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
