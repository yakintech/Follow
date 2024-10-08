using FluentValidation;
using FluentValidation.AspNetCore;
using Follow.API.DTO.BlogCategory;
using Follow.API.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddFluentValidation();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidationAutoValidation();

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




builder.Services.AddScoped<IValidator<CreateBlogCategoryRequestDTO>, CreateBlogCategoryRequestValidator>();

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
