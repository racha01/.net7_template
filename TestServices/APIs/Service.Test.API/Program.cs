using Common.AppSettings.TestAPI;
using Common.Options;
using DBContext.MongoDB;
using Domain.Test.UnitOfWorks;
using Proxy.Line.Settings;
using Proxy.Line;
using Proxy.Line.Authorization.Core;
using Proxy.Line.Authorization.Core.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddScoped<ILogicUnitOfWork, LogicUnitOfWork>();
builder.Services.AddScoped<IRepositoryUnit, RepositoryUnit>();
builder.Services.Configure<MongoDBOptions>(builder.Configuration.GetSection("MongoDBOptions"));
builder.Services.RegisterLineService(builder.Configuration.GetSection("ServiceSetting").GetSection($"{nameof(LineSetting)}").Get<LineSetting>());
builder.Services.AddScoped<IOAuthLineManager, OAuthLineManager>();

#region Version
builder.Services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

builder.Services.AddApiVersioning(
    options =>
    {
        options.ReportApiVersions = true;
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    });
#endregion

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
