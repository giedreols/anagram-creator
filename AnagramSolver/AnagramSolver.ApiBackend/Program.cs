using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DbFirst;
using AnagramSolver.EF.DbFirst.Entities;
using Microsoft.EntityFrameworkCore;

// API BACKEND

var builder = WebApplication.CreateBuilder();

builder.Services.AddScoped<IWordServer, WordServer>();

builder.Services.AddScoped<ISearchLogService, LogService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IWordLogService, LogService>();

builder.Services.AddScoped<IWordLogRepository, WordLogRepo>();
builder.Services.AddScoped<ISearchLogRepository, SearchLogRepo>();
builder.Services.AddScoped<IWordRepository, WordRepo>();
builder.Services.AddScoped<IPartOfSpeechRespository, PartOfSpeechRepo>();
builder.Services.AddSingleton<ITimeProvider, AnagramSolver.BusinessLogic.TimeProvider>();
builder.Services.AddScoped<IConfigReader, AnagramSolver.Contracts.ConfigReader>();


builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

builder.Services.AddDbContext<AnagramSolverDataContext>(
    options => options
        .UseSqlServer("Data Source=.;Initial Catalog=AnagramSolverData;Integrated Security=True;TrustServerCertificate=true"));

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseSession();

app.Run();
