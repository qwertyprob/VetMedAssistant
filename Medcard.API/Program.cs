using Medcard.DbAccessLayer;
using Medcard.DbAccessLayer.Interfaces;
using Medcard.DbAccessLayer.Repositories;
using Medcard.Bl.Abstraction;
using Medcard.Bl.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Medcard.DbAccessLayer.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Register services and repositories for Dependency Injection
builder.Services.AddScoped<IMedcardRepository, MedcardRepository>();
builder.Services.AddScoped<IMedcardService, MedcardService>();
builder.Services.AddScoped<ISearchRepository, SearchRepository>();
builder.Services.AddScoped<ISearchService, SearchService>();

// Configure distributed memory cache
builder.Services.AddDistributedMemoryCache();

// Configure session settings
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Configure authentication using cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/api/auth/login"; // Set login path for the API
    });

// Configure database connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MedcardConnectionString"))
           .EnableSensitiveDataLogging(false));

// Configure AutoMapper (if required)
builder.Services.AddAutoMapper(typeof(Medcard.Bl.Mapping.MappingProfileBlazor));

// Add Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Medcard API",
        Version = "v1",
        Description = "An API for managing Medcard data, including owners, pets, drugs, and treatments.",
        Contact = new OpenApiContact
        {
            Name = "Support Team",
            Email = "support@example.com",
            Url = new Uri("https://example.com")
        }
    });

    // Optionally, include XML comments for more detailed API documentation
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Medcard API v1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root (optional)
    });
}
else
{
    app.UseExceptionHandler("/api/error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

// Map controllers
app.MapControllers();

app.Run();
