using Microsoft.EntityFrameworkCore;
using TravelInsuranceWebSite.Extensions;
using TravelInsuranceWebSite.Filters;
using TravelInsurance.Repository.Ef;
using TravelInsurance.Service;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

builder.Services.AddControllersWithViews();
ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Set the license context

builder.Services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(10);//You can set Time
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")),
             ServiceLifetime.Transient);

builder.Services.AddAutoMapper(c => c.AddProfile<MappingProfile>(), typeof(Program));

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(HttpGlobalExceptionFilter));
}).AddNewtonsoftJson(

);

builder.Services.AddConfig();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.UseAuthorization();

app.Run();
