using Productos_BusinessLayer.Services;
using Productos_DataAccessLayer.Data;
using Productos_DataAccessLayer.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Almacenamiento en memoria compartido entre solicitudes (Singleton)
builder.Services.AddSingleton<AlmacenProductos>();

// Inyección de dependencias: interfaces e implementaciones
builder.Services.AddScoped<IProductoRepository, ProductoRepositoryInMemory>();
builder.Services.AddScoped<IProductoService, ProductoService>();

builder.Services.AddControllers();

// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
