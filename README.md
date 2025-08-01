# Proyecto PA - Grupo 5

## Integrantes del Grupo

- **Pablo Herrera Chacón**
- **Jose Julian Olivares Segura**
- **Elías Leblicq Pohehaus**
- **Paulo Vargas Valenciano**

## Especificación de la Arquitectura

El proyecto implementa una **arquitectura en capas** con separación de responsabilidades:

### Estructura del Proyecto

```
ProyectoPA_G5/
├── ProyectoPA_G5/          # Capa de Presentación (ASP.NET Core MVC)
├── Proyecto.BLL/           # Capa de Lógica de Negocio (Business Logic Layer)
├── Proyecto.DAL/           # Capa de Acceso a Datos (Data Access Layer)
└── Proyecto.ML/            # Capa de Modelos (Model Layer)
```

### Descripción de Capas

- **ProyectoPA_G5**: Capa de presentación que contiene los controladores, vistas y configuración de la aplicación web ASP.NET Core MVC.
- **Proyecto.BLL**: Contiene la lógica de negocio y reglas de la aplicación.
- **Proyecto.DAL**: Maneja el acceso a datos utilizando Entity Framework Core y Dapper.
- **Proyecto.ML**: Define los modelos de datos y entidades del dominio.

## Librerías/Paquetes NuGet Utilizados

### ProyectoPA_G5 (Capa de Presentación)
- **Microsoft.AspNetCore.Identity.EntityFrameworkCore** - Integración de Identity con Entity Framework Core
- **Microsoft.AspNetCore.Identity.UI** - Interfaz de usuario predeterminada para Identity
- **Microsoft.EntityFrameworkCore** - ORM Entity Framework Core
- **Microsoft.EntityFrameworkCore.Design** - Herramientas de diseño para EF Core
- **Microsoft.EntityFrameworkCore.SqlServer** - Proveedor SQL Server para EF Core
- **Microsoft.EntityFrameworkCore.Tools** - Herramientas de línea de comandos para EF Core
- **Microsoft.VisualStudio.Web.CodeGeneration.Design** - Herramientas de scaffolding para ASP.NET Core

### Proyecto.BLL (Capa de Lógica de Negocio)
- **AutoMapper** - Mapeo automático entre objetos

### Proyecto.DAL (Capa de Acceso a Datos)
- **Dapper** - Micro ORM para .NET
- **Microsoft.Data.SqlClient** - Cliente de datos para SQL Server
- **Microsoft.EntityFrameworkCore** - ORM Entity Framework Core
- **Microsoft.EntityFrameworkCore.Design** - Herramientas de diseño para EF Core
- **Microsoft.EntityFrameworkCore.SqlServer** - Proveedor SQL Server para EF Core
- **Microsoft.EntityFrameworkCore.Tools** - Herramientas de línea de comandos para EF Core

### Proyecto.ML (Capa de Modelos)
- **Microsoft.AspNet.Identity.EntityFramework** - Framework de Identity para Entity Framework
- **Microsoft.Extensions.Identity.Core** - Servicios core de Identity

## Tecnologías Utilizadas

- **.NET Core/ASP.NET Core MVC**
- **Entity Framework Core**
- **SQL Server**
- **ASP.NET Core Identity**
- **Dapper**
- **AutoMapper**

## Características Principales

- Arquitectura en capas bien definida
- Gestión de identidad y autenticación
- Acceso a datos híbrido (Entity Framework + Dapper)
- Mapeo automático de objetos
- Soporte para SQL Server
