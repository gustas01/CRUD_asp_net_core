using CatalogAPI.Context;
using Microsoft.EntityFrameworkCore;
using dotenv.net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

DotEnv.Load();
IDictionary<string, string> envV = DotEnv.Read();

string? MyPGConnection = $"Server={envV["APP_SERVERNAME"]};Port={envV["APP_PORT"]};User Id={envV["APP_USERNAME"]};Password={envV["APP_PASSWORD"]};Database={envV["APP_DATABASE"]};";

builder.Services.AddDbContext<CatalogAPIContext>(option => option.UseNpgsql(MyPGConnection));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
