using System.Globalization;
using System.Reflection;
using CarvedRock.UI;
using Microsoft.AspNetCore.Localization;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddLocalization();
builder.Services.Configure<RequestLocalizationOptions>(options => {
    var supportedCultures = new List<CultureInfo> { new("en-US") };
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services.AddDemoDuendeIdentityServer();
builder.Services.AddUserAccessTokenHttpClient("cr_client", configureClient: client =>
{
    client.BaseAddress = new Uri("http://carvedrock.api/");
});

builder.Host.UseSerilog(((context, services, loggerConfig) =>
{
    loggerConfig
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .Enrich.WithProperty("Application", Assembly.GetEntryAssembly()?.GetName().Name)
        .WriteTo.Console();
}));

var app = builder.Build();

app.UseExceptionHandler("/Error");
app.UseHsts();
app.UseHttpsRedirection();
app.UseRequestLocalization();
app.UseStaticFiles();

app.UseSerilogRequestLogging();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages().RequireAuthorization();

app.Run();
