using Amazon;
using Amazon.S3;
using HealthAndMonetaryHarmony.Data;
using HealthAndMonetaryHarmony.Middleware;
using HealthAndMonetaryHarmony.Services;
using Microsoft.EntityFrameworkCore;
using PangeaCyber.Net;
using PangeaCyber.Net.AuthN;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<IAmazonS3>(sp =>
{
    var config = new AmazonS3Config
    {
        RegionEndpoint = RegionEndpoint.EUNorth1 // Set your region endpoint here
    };
    return new AmazonS3Client("your-access-key", "your-secret-key", config);
}); ;
builder.Services.AddTransient<S3Uploader>();


// Initialize Pangea AuthN client 
builder.Services.AddSingleton(provider =>
{
    var cfg = Config.FromEnvironment("authn");
    return new AuthNClient.Builder(cfg).Build();
});
builder.Services.AddScoped<PangeaAuthService>();

// Register PangeaConfigService
builder.Services.AddSingleton<PangeaConfigService>();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseMiddleware<CustomExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
