using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WMS_ERP_Backend.DAOProject.DAO;
using WMS_ERP_Backend.DAOProject.IDAO;
using WMS_ERP_Backend.Data;
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

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Conn"))
);

builder.Services.AddScoped<IUserDAO, UserDAO>();
builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<IRoleDAO, RoleDAO>();
builder.Services.AddScoped<RoleService>();

builder.Services.AddScoped<IMenuDAO, MenuDAO>();
builder.Services.AddScoped<MenuService>();

builder.Services.AddScoped<ILinkDAO, LinkDAO>();
builder.Services.AddScoped<LinkService>();

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
