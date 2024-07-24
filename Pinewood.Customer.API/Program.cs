using System.Text.Json.Serialization;
using Asp.Versioning;
using Asp.Versioning.Conventions;
using FluentValidation;
using Pinewood.Customer.Application.Core.DIContainer;
using Pinewood.Customer.Application.Core.DTOs;
using Pinewood.Customer.Application.Core.Validators;
using Pinewood.Customer.Infrastructure.DIContainer;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Json Enum Conversion
builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

//Loging - serilog
builder.Host.UseSerilog((c, loggerConfig) =>
{
    loggerConfig.ReadFrom.Configuration(c.Configuration);
}, false, true);

builder.Services.AddSingleton(Log.Logger);

//Validator
builder.Services.AddScoped<IValidator<CustomerInfoDto>, CustomerInfoValidator>();

//Application DI
builder.Services.AddAppServices(builder.Configuration);

//Infrastructure DI
builder.Services.AddInfrastructureServices(builder.Configuration);

//Api Version
builder.Services.AddApiVersioning(v =>
{
    v.DefaultApiVersion = new ApiVersion(1.0);
    v.AssumeDefaultVersionWhenUnspecified = true;
    v.ReportApiVersions = true;

}).AddMvc(o =>
{
    o.Conventions.Add(new VersionByNamespaceConvention());
}
);

//Adding cors
builder.Services.AddCors(option =>
{
    option.AddPolicy(name: "CustomerBlazorUI", policy =>
    {
        policy.WithOrigins("https://localhost:7167")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });

});

//Fluent Validation
builder.Services.AddValidatorsFromAssemblyContaining<CustomerInfoValidator>();

builder.Logging.AddRinLogger();

builder.Services.AddRin(); //Rin for dev tracing


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseRin();
    app.UseRinDiagnosticsHandler();

}

//Serilog - 
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors("CustomerBlazorUI");

app.UseAuthorization();

app.MapControllers();

app.Run();

