using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DbFirst;
using AnagramSolver.EF.DbFirst.Entities;
using Microsoft.EntityFrameworkCore;
using MyConfiguration = AnagramSolver.WebApp.MyConfiguration;

// WEB RAZOR

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IWordServer, WordServer>();

builder.Services.AddScoped<ISearchLogService, LogService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IWordLogService, LogService>();

builder.Services.AddScoped<IWordLogRepository, WordLogRepo>();
builder.Services.AddScoped<ISearchLogRepository, SearchLogRepo>();
builder.Services.AddScoped<IWordRepository, WordRepo>();
builder.Services.AddScoped<IPartOfSpeechRespository, PartOfSpeechRepo>();
builder.Services.AddSingleton<ITimeProvider, AnagramSolver.BusinessLogic.TimeProvider>();
builder.Services.AddScoped<IMyConfiguration, AnagramSolver.BusinessLogic.MyConfiguration>();


builder.Services.AddScoped<MyConfiguration>();


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

builder.Services.AddDbContext<AnagramSolverDataContext>(
        options => options.UseSqlServer("Data Source=.\\MSSQLSERVER01;Initial Catalog=AnagramSolverData;Integrated Security=True;TrustServerCertificate=true"));

builder.Services.AddHttpClient();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

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
app.UseCors();
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.UseSession();

app.Run();

