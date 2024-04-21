using Adapters.BaseApi;
using Microsoft.AspNetCore.Authentication.Cookies;
using Services.AuthenticationService;
using Services.CommonService;
using Services.MiddleWares;
using Services.PayrollService;
using Services.PermissionService;
using Services.UserService;
using Services.WorksiteService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie.Name = "Pusula.UI";
    options.LoginPath = "/Authentication/Login";
    options.AccessDeniedPath = "/Authentication/AccessDenied";

});

builder.Services.AddHttpClient();

builder.Services.AddScoped<IBaseApiAdapter, BaseApiAdapter>();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IWorksiteService, WorksiteService>();
builder.Services.AddScoped<ICommonService, CommonService>();
builder.Services.AddScoped<IPayrollService, PayrollService>();


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

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
