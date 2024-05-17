using MultiTenancy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// For read header fro request:
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
// For Product Service:
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddTenancy(builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
