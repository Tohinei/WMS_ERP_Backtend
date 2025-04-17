using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WMS_ERP_Backend.DaoProject.Connection;
using WMS_ERP_Backend.DaoProject.Dao;
using WMS_ERP_Backend.DaoProject.IDao;
using WMS_ERP_Backend.DaoProject.Services;
using WMS_ERP_Backend.Services;

var builder = WebApplication.CreateBuilder(args);

var key = "mysecretkey";

builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "wms.com",
            ValidAudience = "wms.com",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddScoped<DbConnection>();

builder.Services.AddScoped<IUserDao, UserDao>();
builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<IRoleDao, RoleDao>();
builder.Services.AddScoped<RoleService>();

builder.Services.AddScoped<IMenuDao, MenuDao>();
builder.Services.AddScoped<MenuService>();

builder.Services.AddScoped<ISessionDao, SessionDao>();
builder.Services.AddScoped<SessionService>();

builder.Services.AddScoped<JwtService>(provider => new JwtService(key));

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
