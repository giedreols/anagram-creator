using AnagramSolver.BusinessLogic;
using AnagramSolver.BusinessLogic.DictionaryActions;
using AnagramSolver.Cli;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.DbActions;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IAnagramGenerator, AnagramSolver.BusinessLogic.AnagramActions.AnagramGenerator>();
builder.Services.AddScoped<IWordRepository, DataBaseActions>();
builder.Services.AddScoped<MyConfiguration>();
builder.Services.AddScoped<DbAccess>();



// ar situr reikia?
builder.Services.AddScoped<ICacheActions, CacheActions>();
//builder.Services.AddScoped<IFileReader, FileReader>();
builder.Services.AddScoped<ISearchLogActions, SearchLogActions>();
builder.Services.AddScoped<IWordsActions, WordsActions>();


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
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseSession();

app.Run();
