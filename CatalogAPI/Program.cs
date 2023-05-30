using CatalogAPI.Context;
using Microsoft.EntityFrameworkCore;
using dotenv.net;
using CatalogAPI.Repository;
using AutoMapper;
using CatalogAPI.DTOs.Mappings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

DotEnv.Load();
IDictionary<string, string> envV = DotEnv.Read();

string? MyPGConnection = $"Server={envV["APP_SERVERNAME"]};Port={envV["APP_PORT"]};User Id={envV["APP_USERNAME"]};Password={envV["APP_PASSWORD"]};Database={envV["APP_DATABASE"]};";

builder.Services.AddDbContext<CatalogAPIContext>(option => option.UseNpgsql(MyPGConnection));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<CatalogAPIContext>().AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters{
  ValidateIssuer = true,
  ValidateAudience = true,
  ValidateLifetime = true,
  ValidAudience = envV["AUDIENCE_TOKEN"],
  ValidIssuer = envV["ISSUER_TOKEN"],
  ValidateIssuerSigningKey = true,
  IssuerSigningKey = new SymmetricSecurityKey(
    Encoding.UTF8.GetBytes(envV["JWT_KEY"])
  )
});


//configurações do automapper
var mappingConfig = new MapperConfiguration(mc => {
  mc.AddProfile(new MappingProfile());
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


var app = builder.Build();

app.UseCors(opt => opt.AllowAnyOrigin());
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
