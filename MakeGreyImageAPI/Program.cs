using MakeGreyImageAPI.Controllers;
using MakeGreyImageAPI.Interfaces;
using MakeGreyImageAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// builder.Services.AddControllers();

#pragma warning disable CA1416
builder.Services.AddScoped<IImageService, ImageService>();
#pragma warning restore CA1416

// #pragma warning disable CA1416
// builder.Services.AddScoped<ImageController>();
// #pragma warning restore CA1416

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

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

// Configure the HTTP request pipeline.
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

app.MapControllers();

app.Run();