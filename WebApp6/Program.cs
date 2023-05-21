using WebApp6.Services.HistoryService;
using WebApp6.Services.ManufacturerService;
using WebApp6.Services.ProductService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IProductService, ProductService>(); // Add singleton to save data state between requests
builder.Services.AddSingleton<ICategoryService, CategoryService>();
builder.Services.AddSingleton<IManufacturerService, ManufacturerService>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();