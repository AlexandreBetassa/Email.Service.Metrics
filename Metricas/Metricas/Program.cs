using Metricas;
using Metricas.Services;
using Metricas.Utils;
using Microsoft.Extensions.Options;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddHealthChecks();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();

builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection(nameof(EmailConfig)));


builder.Services.AddScoped<EmailServices>();
builder.Services.AddSingleton<EmailConfig>(opt => opt.GetRequiredService<IOptions<EmailConfig>>().Value);

var app = builder.Build();

app.UseMetricServer();
app.UseHttpMetrics();

app.UsePrometheusConfiguration();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();

