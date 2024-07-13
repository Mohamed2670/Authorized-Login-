using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MwTesting;
using MwTesting.Authentication;
using MwTesting.Controllers;
using MwTesting.Data;
using MwTesting.Filters;
using MwTesting.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<UserContext>(options => options.UseNpgsql(
builder.Configuration.GetConnectionString("UserConnection")
));

builder.Services.AddScoped<IUserAc, SqlUserAc>();
builder.Services.AddScoped<IProductAc, SqlProductAc>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<LogActivityFilter>();
    options.Filters.Add<PermissionBasedAuthFilter>();
    // to add more global filters
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SuperUser", builder =>
    { builder.RequireRole("Admin"); }
    );
});
var jwtOptions = builder.Configuration.GetSection("jwt").Get<JwtOptions>();
builder.Services.AddSingleton(jwtOptions);
builder.Services.AddAuthentication()
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey)),

        };
    });
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<RateLimitingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ProfilingMiddleWare>();

app.Run();
