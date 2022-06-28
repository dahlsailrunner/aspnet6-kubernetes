using System.Reflection;
using CarvedRock.UI;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddDemoDuendeIdentityServer();

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
app.UseStaticFiles();

app.UseSerilogRequestLogging();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages().RequireAuthorization();

app.Run();
