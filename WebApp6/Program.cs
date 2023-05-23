using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using System.Text;
using WebApp6.Services.AuthService;
using WebApp6.Services.HistoryService;
using WebApp6.Services.ManufacturerService;
using WebApp6.Services.PasswordService;
using WebApp6.Services.ProductService;
using WebApp6.Services.V1;
using WebApp6.Services.V2;
using WebApp6.Services.V3;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IProductService, ProductService>(); // Add singleton to save data state between requests
builder.Services.AddSingleton<ICategoryService, CategoryService>();
builder.Services.AddSingleton<IManufacturerService, ManufacturerService>();
builder.Services.AddSingleton<IPasswordService, PasswordService>(); // Add singleton to be consumed by other singleton services
builder.Services.AddSingleton<IAuthService, AuthService>();

builder.Services.AddScoped<INumberService, NumberService>(); // Add scoped objects because they are the same within a request,
builder.Services.AddScoped<IStringService, StringService>(); // but different across different requests
builder.Services.AddScoped<IExcelService, ExcelService>();

//builder.Services.AddApiVersioning(apiVersioningOptions =>
//{
//    apiVersioningOptions.ApiVersionReader = new UrlSegmentApiVersionReader();
//});
// Add ApiExplorer to discover versions
//builder.Services.AddVersionedApiExplorer(setup =>
//{
//    setup.GroupNameFormat = "'v'VVV";
//    setup.SubstituteApiVersionInUrl = true;
//});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

// Auth
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Authorization:TokenKey").Value!)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// This is to generate the Default UI of Swagger Documentation
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API v1.0", Version = "v1" });
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "My API v2.0", Version = "v2" });
    c.SwaggerDoc("v3", new OpenApiInfo { Title = "My API v3.0", Version = "v3" });

    // To Enable authorization using Swagger (JWT)
    c.AddSecurityDefinition(
        name: "token",
        securityScheme: new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer",
            In = ParameterLocation.Header,
            Name = HeaderNames.Authorization
        }
    );

    c.AddSecurityRequirement(
        securityRequirement: new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "token"
                    },
                },
                Array.Empty<string>()
            }
        }
    );
}
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1.0");
        opt.SwaggerEndpoint("/swagger/v2/swagger.json", "My API V2.0");
        opt.SwaggerEndpoint("/swagger/v3/swagger.json", "My API V3.0");
        opt.RoutePrefix = "swagger";
        opt.DocumentTitle = "My API";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.UseApiVersioning();

app.MapControllers();

app.Run();