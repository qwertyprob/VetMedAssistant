using Medcard.Server.Components;
using Medcard.DbAccessLayer;
using Medcard.DbAccessLayer.Dto;
using Medcard.DbAccessLayer.Entities;
using Medcard.DbAccessLayer.Interfaces;
using Medcard.DbAccessLayer.Mapping;
using Medcard.DbAccessLayer.Repositories;
using Medcard.Bl.Abstraction;
using Medcard.Bl.Mapping;
using Medcard.Bl.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Diagnostics.Metrics;
using Medcard.Server.Dependency;

var builder = WebApplication.CreateBuilder(args);

//All services without db
builder.Services.AddServices();

builder.Services.AddScoped<AppDbContext>();
// Connect to the database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MedcardConnectionString"))
    .EnableSensitiveDataLogging(false));

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}


app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// Map server-side and WebAssembly components
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Medcard.Client._Imports).Assembly);




app.Run();
