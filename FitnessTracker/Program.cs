using Fitness.DataAccess.Data;
using Fitness.DataAccess.Repositories.Interfaces;
using Fitness.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using FitnessTracker.Services;
using FitnessTracker.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddHttpClient("nutritionapi", client =>
{
	client.BaseAddress = new Uri("https://api.nal.usda.gov/fdc/v1");
});
builder.Services.AddScoped<IUSDAFoodService, USDAFoodService>();
builder.Services.AddHttpClient("caloriesapi", client =>
{
	client.BaseAddress = new Uri("https://nutrition-calculator.p.rapidapi.com/api");
});
builder.Services.AddScoped<IFitnessCalculatorService, FitnessCalculatorService>();


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
