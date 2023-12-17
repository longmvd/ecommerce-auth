using AuthService.BL;
using AuthService.BL.AuthBL;
using AuthService.DL;
using Ecommerce.DL;
using ECommerce.Common.Entities;
using ECommerce.Common.Extension;
using ECommerce.DL;
using ECommerce.DL.RoleDL;
using Steeltoe.Discovery.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IUserBL, UserBL>();
builder.Services.AddScoped<IUserDL, UserDL>();

builder.Services.AddScoped<IRoleBL, RoleBL>();
builder.Services.AddScoped<IRoleDL, RoleDL>();

builder.Services.AddSingleton<IAuthBL, AuthBL>();


DatabaseContext.ConnectionString = builder.Configuration.GetConnectionString("MySql");

builder.Services.AddControllers();

builder.Services.AddDiscoveryClient();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddBaseControllerConfig();

var MyAllowSpecificOrigins = "_MyAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors(MyAllowSpecificOrigins);


app.Run();
