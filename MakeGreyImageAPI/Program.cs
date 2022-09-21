using MakeGreyImageAPI.Interfaces;
using MakeGreyImageAPI.Managers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IImageManager, ImageManager>();
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

app.MapControllers();

app.Run();