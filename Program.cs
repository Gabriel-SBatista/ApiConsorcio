using ApiConsorcio.Context;
using ApiConsorcio.Filters;
using ApiConsorcio.Models;
using ApiConsorcio.Repositories;
using ApiConsorcio.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string postGreConnection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(postGreConnection));
builder.Services.AddHttpClient();
builder.Services.AddScoped<AuthorizationFilterAdmin>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var httpClient = provider.GetService<IHttpClientFactory>();
    return new AuthorizationFilterAdmin(configuration, httpClient);
});
builder.Services.AddScoped<AuthorizationFilterUserCompany>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var httpClient = provider.GetService<IHttpClientFactory>();
    return new AuthorizationFilterUserCompany(configuration, httpClient);
});
builder.Services.AddScoped<LeadsService>();
builder.Services.AddScoped<LeadsRepository>();
builder.Services.AddScoped<ExcelService>();
builder.Services.AddScoped<ExportService>();
builder.Services.AddScoped<ExportRepository>();
builder.Services.AddScoped<IValidator<Lead>, LeadValidator>();
builder.Services.AddScoped<EmailService>(provider =>
{
    string smtpServer = builder.Configuration["Smtp:Server"];
    int smtpPort = int.Parse(builder.Configuration["Smtp:Port"]);
    string smtpUsername = builder.Configuration["Smtp:Username"];
    string smtpPassword = builder.Configuration["Smtp:Password"];

    return new EmailService(smtpServer, smtpPort, smtpUsername, smtpPassword);
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.MapControllers();

app.Run();
