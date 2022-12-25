using jsreport.AspNetCore;
using jsreport.Local;
using jsreport.Binary;
using Microsoft.EntityFrameworkCore;
using ReportGenerate.DataAccess.Data;
using ReportGenerate.DataAccess.Repository;
using ReportGenerate.DataAccess.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var conncetionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(conncetionString));

builder.Services.AddScoped<IProductReposotory, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddJsReport(new LocalReporting().KillRunningJsReportProcesses().UseBinary(JsReportBinary.GetBinary()).Configure(cfg => {
    cfg.HttpPort = 3000;
    return cfg;
}).AsUtility().Create());

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
