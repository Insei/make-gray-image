using System.Reflection;
using MakeGreyImageAPI.Infrastructure.Context;
using MakeGreyImageAPI.Infrastructure.Generics;
using MakeGreyImageAPI.Interfaces;
using MakeGreyImageAPI.Managers;
using MakeGreyImageAPI.Services;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using FluentValidation;
using MakeGreyImageAPI.Entities;
using MakeGreyImageAPI.Loggers;
using MakeGreyImageAPI.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ILogger = MakeGreyImageAPI.Interfaces.ILogger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataDbContext>(options =>
    options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ImageBase;Trusted_Connection=True;"), ServiceLifetime.Singleton);
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<DataDbContext>();

// We use asynchronous conversion which continues after the request is executed
builder.Services.AddSingleton<IImageManager, ImageManager>();
builder.Services.AddSingleton<IGenericRepository, GenericRepository>();
builder.Services.AddSingleton<ImageService>();
builder.Services.AddSingleton<LocalImageConvertTaskService>();
builder.Services.AddSingleton<ILogger, SerilogLogger>();
builder.Services.AddScoped<ApplicationUserService>();
builder.Services.AddScoped<ApplicationUserAdminService>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Make grey image API", Version = "v1" });
    
    //Determine base path for the application.
    var basePath = AppContext.BaseDirectory;

    //Set the comments path for the swagger json and ui.
    var xmlPath = Path.Combine( basePath, "MakeGreyImageAPI.xml"); 
    var xmlPathDto = Path.Combine( basePath, "MakeGreyImageAPI.DTOs.xml"); 
    c.IncludeXmlComments(xmlPathDto);
    c.IncludeXmlComments(xmlPath);
    
});

//it is necessary for requests from the host locale,
//to work without a signed certificate, and we allow the use of any HTTP methods and hiders
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCorsPolicy", corsBuilder =>
    {
        corsBuilder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseStaticFiles();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseStaticFiles();

app.UseAuthorization();
SeedDataDb.Initialize(app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider);
app.MapControllers();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<LoggerMiddleware>();
app.Run();