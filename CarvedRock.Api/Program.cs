using System.Data;
using System.Reflection;
using CarvedRock.Api;
using CarvedRock.Api.Data;
using CarvedRock.Api.DomainLogic;
using CarvedRock.Api.Profiles;
using CarvedRock.Api.Repository;
using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDemoDuendeJwtBearer();

builder.Services.AddDbContext<AdminContext>();
builder.Services.AddScoped<ICarvedRockRepository, CarvedRockRepository>();
builder.Services.AddScoped<IProductLogic, ProductLogic>();
builder.Services.AddAutoMapper(typeof(ProductProfile));

builder.Services.AddProblemDetails(ConfigureProblemDetails);
builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<ProductValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerOptions>();
builder.Services.AddSwaggerGen();

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
app.UseProblemDetails();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AdminContext>();
    context.Database.Migrate();

    if (app.Environment.IsDevelopment())
    {
        context.SeedInitialData();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.OAuthClientId("interactive.public.short");
        options.OAuthAppName("CarvedRock API");
        options.OAuthUsePkce();
    });
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers().RequireAuthorization();
if (app.Environment.IsDevelopment())
{
    app.MapFallback(() => Results.Redirect("/swagger"));
}

app.Run();

void ConfigureProblemDetails(ProblemDetailsOptions o)
{
    o.IncludeExceptionDetails = (_, _) => false;
    o.MapFluentValidationException();
    o.ValidationProblemStatusCode = StatusCodes.Status400BadRequest;
    o.MapToStatusCode<DBConcurrencyException>(StatusCodes.Status409Conflict);
    o.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);
    o.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
}
