using System.Reflection;
using MakeGreyImageAPI.Infrastructure.Context;
using MakeGreyImageAPI.Interfaces;
using MakeGreyImageAPI.Managers;
using MakeGreyImageAPI.Services;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using FluentValidation;
using MakeGreyImageAPI.Entities;
using MakeGreyImageAPI.Loggers;
using MakeGreyImageAPI.Middlewares;
using MakeGreyImageAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MakeGreyImageAPI.Infrastructure.Generics;
using ILogger = MakeGreyImageAPI.Interfaces.ILogger;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationManager().AddJsonFile("appsettings.json").Build();
var authOptions = configuration.GetSection("AuthOptions").Get<AuthOptions>();

builder.Services.AddDbContext<DataDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DataDbContext")));
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<DataDbContext>();

builder.Services.AddSingleton(authOptions);
builder.Services.AddScoped<IImageManager, ImageManager>();
builder.Services.AddScoped<IGenericRepository<Guid, LocalImage>, GenericRepository<Guid, LocalImage>>();
builder.Services.AddScoped<IGenericRepository<Guid, LocalImageConvertTask>, GenericRepository<Guid, LocalImageConvertTask>>();
builder.Services.AddScoped<IGenericRepository<Guid, ApplicationUser>, GenericRepository<Guid, ApplicationUser>>();
builder.Services.AddScoped<ImageService>();
builder.Services.AddScoped<LocalImageConvertTaskService>();
builder.Services.AddSingleton<ILogger, SerilogLogger>();

builder.Services.AddScoped<ApplicationUserService>();
builder.Services.AddScoped<ApplicationUserAdminService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();

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

builder.Services.AddAuthentication(option =>
    {
        // Fixing 404 error when adding an attribute Authorize to controller
        option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = authOptions.Issuer,
            ValidateAudience = true,
            ValidAudience = authOptions.Audience,
            ValidateLifetime = true,
            IssuerSigningKey = authOptions.GetSymmetricSecurityKey(authOptions.Key),
            ValidateIssuerSigningKey = true,
        };
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
    
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()  
                {  
                    Name = "Authorization",  
                    Type = SecuritySchemeType.ApiKey,  
                    Scheme = "Bearer",  
                    BearerFormat = "JWT",  
                    In = ParameterLocation.Header,  
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",  
                });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement  
    {  
        {  
            new OpenApiSecurityScheme  
            {  
                Reference = new OpenApiReference  
                {  
                    Type = ReferenceType.SecurityScheme,  
                    Id = "Bearer"  
                }  
            },  
            new string[] {}
        }  
    });
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<LoggerMiddleware>();
app.Run();