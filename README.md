# Práctico Bloque 2 — Web API RESTful de Gestión de Productos en 3 Capas

Web API en ASP.NET Core (.NET 10) con arquitectura en tres capas, DTOs, validaciones con Data Annotations, inyección de dependencias y Swagger. Los datos se guardan en memoria.

## Requisitos

- [.NET SDK 10](https://dotnet.microsoft.com/download)

## Estructura

```
ProductosApi.slnx
Productos_PresentationLayer/        Controllers, Program.cs, Swagger
├── Controllers/ProductosController.cs
└── Program.cs
Productos_BusinessLayer/            Lógica de negocio y DTOs
├── DTOs/CreateProductoDto.cs
├── DTOs/ProductoDto.cs
├── Services/IProductoService.cs
└── Services/ProductoService.cs
Productos_DataAccessLayer/          Acceso a datos en memoria
├── Models/Producto.cs
├── Data/AlmacenProductos.cs
├── Repositories/IProductoRepository.cs
└── Repositories/ProductoRepositoryInMemory.cs
```

Flujo: `ProductosController` → `IProductoService` → `IProductoRepository` → `AlmacenProductos` (lista en memoria).

## Cómo ejecutarlo

```bash
git clone <URL-del-repositorio>
cd <carpeta-del-repositorio>
dotnet run --project Productos_PresentationLayer
```

Se abre Swagger en el navegador: <http://localhost:5129/swagger>. Si no se abre solo, ingresá a esa dirección manualmente.

## Endpoints (`/api/productos`)

| Método | Ruta | Respuestas |
|---|---|---|
| GET | `/api/productos` | 200 OK |
| GET | `/api/productos/{id}` | 200 OK / 404 Not Found |
| POST | `/api/productos` | 201 Created / 400 Bad Request |
| PUT | `/api/productos/{id}` | 200 OK / 400 Bad Request / 404 Not Found |
| DELETE | `/api/productos/{id}` | 204 No Content / 404 Not Found |

Body de ejemplo para POST y PUT:

```json
{
  "nombre": "Teclado mecánico",
  "precio": 60000,
  "categoria": "Tecnología"
}
```

Validaciones: `nombre` obligatorio entre 3 y 100 caracteres, `precio` mayor a 0, `categoria` obligatoria.

## Inyección de dependencias

En `Program.cs`:

- `AddScoped<IProductoService, ProductoService>()`
- `AddScoped<IProductoRepository, ProductoRepositoryInMemory>()`
- `AddSingleton<AlmacenProductos>()`

El repositorio es Scoped (una instancia nueva por solicitud), por eso la `List<Producto>` vive en `AlmacenProductos`, que es Singleton y se inyecta en el repositorio. Así los productos persisten entre solicitudes HTTP mientras la aplicación está en ejecución.

## Cómo probarlo en Swagger

1. `GET /api/productos`: devuelve los 3 productos iniciales.
2. `POST /api/productos` con el body de ejemplo: devuelve 201 con el producto creado (Id 4) y su ubicación.
3. `GET /api/productos/4`: confirma que el producto persiste.
4. `PUT /api/productos/4` con otros datos: devuelve 200 con el producto actualizado.
5. `DELETE /api/productos/4`: devuelve 204. Repetirlo devuelve 404.
6. Casos de error: `GET /api/productos/99` (404) y un `POST` con `"nombre": "ab"` o `"precio": 0` (400).
