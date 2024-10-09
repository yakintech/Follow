using FluentValidation;
using FluentValidation.AspNetCore;
using Follow.API.DTO.BlogCategory;
using Follow.API.Validators;
using Follow.Business.Repository;
using Follow.Data.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using Follow.API.Middlewares;
using Follow.API.DTO.WebUser;
using Follow.API.Mapping.Models;
using Follow.API.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddFluentValidation();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentValidationAutoValidation();

//logger ekle
builder.Services.AddLogging();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
               builder =>
               {
                   //builder.WithOrigins("http://127.0.0.1:5500")
                   builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});



builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
            factory: partition => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 500,
                Window = TimeSpan.FromMinutes(1)
            }));
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "garanti",
            ValidAudience = "garanti",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("loremipsumloremipsumloremipsumloremipsumloremipsumloremipsumloremipsum")),
            ClockSkew = TimeSpan.Zero
        };
    });


builder.Services.AddRepositories();
builder.Services.AddValidators();


builder.Services.AddAutoMapper(typeof(CreateWebUserProfile));




var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseMiddleware<ExceptionHandlingMiddleware>();

}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowAll");
app.UseRateLimiter();
app.MapControllers();

app.Run();
