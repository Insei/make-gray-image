using System.Reflection;
using MakeGreyImageAPI.Infrastructure.Context;
using MakeGreyImageAPI.Infrastructure.Generics;
using MakeGreyImageAPI.Interfaces;
using MakeGreyImageAPI.Managers;
using MakeGreyImageAPI.Services;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataDbContext>(options =>
    options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ImageBase;Trusted_Connection=True;"), ServiceLifetime.Singleton);
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//the AddSingleton service is created at the first request,
//and then each subsequent request will use the same instance
builder.Services.AddSingleton<IImageManager, ImageManager>();
builder.Services.AddSingleton<IGenericRepository, GenericRepository>();
builder.Services.AddSingleton<ImageService>();
builder.Services.AddSingleton<LocalImageConvertTaskService>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


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
    app.UseDeveloperExceptionPage();
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

app.Run();