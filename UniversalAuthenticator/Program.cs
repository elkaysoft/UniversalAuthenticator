using Microsoft.EntityFrameworkCore;
using UniversalAuthenticator.Common.Extensions;
using UniversalAuthenticator.Domain.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<UniversalAuthDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("UniversalAuthConnection"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ApplicationDependency.AddApplicationDI(builder.Services);
EntityDependency.AddPersistenceDI(builder.Services);


builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
