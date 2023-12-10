using EdgyElegance.Api.Helpers;
using EdgyElegance.Api.Middlewares;
using EdgyElegance.Application;
using EdgyElegance.Infrastructure;
using EdgyElegance.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.InjectPersistenceServices();
builder.Services.InjectApplicationServices();
builder.Services.InjectInfrastructureServices();
builder.InjectServices();
builder.Seed();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
