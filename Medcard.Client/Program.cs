using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Medcard.Bl.Abstraction; // Ensure you include the correct namespace for IMedcardService
using Medcard.Bl;
using Medcard.Bl.Services;
using Medcard.DbAccessLayer.Interfaces;
using Medcard.DbAccessLayer;
using Medcard.DbAccessLayer.Repositories; // Namespace for MedcardService

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<IMedcardRepository, MedcardRepository>();
builder.Services.AddScoped<IMedcardService, MedcardService>();





await builder.Build().RunAsync();
