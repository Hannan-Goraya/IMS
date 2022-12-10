using IMS.BLL.Role;
using IMS.BLL.Users;
using IMS.DAL.Dapper;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IDapperRepository, DapperRepository>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IRoleServices, RoleServices>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x => x.LoginPath = "/Account/Login");

 builder.Services.ConfigureApplicationCookie(options =>
 {
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.SlidingExpiration = false;
    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
