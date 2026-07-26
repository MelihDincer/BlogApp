using BlogApp.Data.Concrete.EfCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BlogContext>(options =>
{
    var config = builder.Configuration;
    var connectionString = config.GetConnectionString("sql_connection");
     options.UseSqlite(connectionString);
    // var version = new MySqlServerVersion(new Version(8,0,46));
    // options.UseMySql(connectionString, version);
});

var app = builder.Build();
SeedData.FillTestData(app);

app.MapDefaultControllerRoute();

// app.MapGet("/", () => "Hello World!");

app.Run();
