using Layer.Domain.Contract.Context;
using Layer.Domain.Contract.Contracts;
using Layer.Domain.Contract.Infrastucture;
using Layer.Domain.Service.Contracts.IGenericServices;
using Layer.Domain.Service.Contracts.IUserService;
using Layer.Domain.Service.Infrastucture.UserService;
using Layer.Domian.Entities.DB.Users;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericServices<>));
builder.Services.AddScoped(typeof(IGenericServices<>), typeof(GenericServices<>));
builder.Services.AddScoped<IUserService, UserService>();
var ConnectionString = builder.Configuration.GetConnectionString("ConnectionString");

var databaseSetting = new DBSetting();
databaseSetting.ConnectionString = ConnectionString;
builder.Configuration.Bind(nameof(DBSetting), databaseSetting);
builder.Services.AddSingleton(databaseSetting);

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
