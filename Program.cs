//using System;
//using Kadelle_Liburd_C__Cumulative.Controllers;
//using Kadelle_Liburd_C__Cumulative.Models;

//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllersWithViews();

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

////Database
//builder.Services.AddScoped<SchoolDbContext>();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();

using Kadelle_Liburd_C__Cumulative.Controllers;
using Kadelle_Liburd_C__Cumulative.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<SchoolDbContext>();

//Swagger API Help pages
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Database
builder.Services.AddScoped<SchoolDbContext>();

builder.Services.AddScoped<TeacherAPIController>();

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

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
