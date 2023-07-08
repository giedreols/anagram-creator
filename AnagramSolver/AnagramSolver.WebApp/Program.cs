using AnagramSolver.BusinessLogic;
using AnagramSolver.Cli;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.DbActions;
using AnagramSolver.EF.DbFirst;
using AnagramSolver.WebApp.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IAnagramGenerator, AnagramSolver.BusinessLogic.AnagramActions.AnagramGenerator>();
builder.Services.AddScoped<IWordRepository, DataBaseActions>();
builder.Services.AddScoped<MyConfiguration>();
builder.Services.AddScoped<DbAccess>();
builder.Services.AddScoped<IHelpers, Helpers>();

builder.Services.AddScoped<ICacheActions, DbFirstCacheActions>();
builder.Services.AddScoped<ISearchLogActions, DbFirstSearchLogActions>();
builder.Services.AddScoped<IWordsActions, DbFirstWordsActions>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

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
	pattern: "{controller=Home}/{action=Index}");

app.UseSession();

app.Run();
