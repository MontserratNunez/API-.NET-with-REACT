# API .NET - Sistema de Contribuyentes y Comprobantes Fiscales

## Descripción

Esta API está diseñada para gestionar contribuyentes y comprobantes fiscales. Permite registrar, consultar y eliminar contribuyentes, así como registrar comprobantes fiscales y calcular el ITBIS (impuesto sobre la transferencia de bienes industrializados y servicios).

## Estructura del Proyecto

- **Entities:** Contiene las clases `Contribuyente` y `ComprobanteFiscal`, que representan los datos del sistema.
- **Controllers:** Define los controladores de la API que exponen los endpoints de contribuyentes y comprobantes fiscales.
- **Services:** Provee la lógica de negocio y conecta con la base de datos usando Entity Framework.
- **Data:** Configuración de la base de datos usando Entity Framework Core.

## Requisitos

- .NET 6 SDK
- SQL Server
- Visual Studio o Visual Studio Code
- Serilog para el logging
- Swagger para la documentación de la API

## Configuración

1. Clonar el repositorio:

   ```bash
   git clone https://github.com/usuario/proyecto-api.git
   cd proyecto-api

2. Configurar la cadena de conexión a la base de datos en appsettings.json
    {
        "ConnectionStrings": {
            "Default": "Server=localhost;Database=NombreDB;User Id=usuario;Password=contraseña;"
        }
    }

3. Restaurar los paquetes NuGet:

    dotnet restore

4. Aplicar las migraciones a la base de datos:

    dotnet ef database update

5. Ejecutar el proyecto:

    dotnet run

6. Acceder a la documentación de la API en Swagger:
    Visite https://localhost:5001/swagger para explorar los endpoints de la API.

## Endpoints
- **GET** /api/contribuyente: Retorna la lista de contribuyentes, con filtros opcionales.
- **GET** /api/contribuyente/{rncCedula}/comprobantes: Retorna los comprobantes fiscales asociados a un contribuyente específico, así como el total del ITBIS acumulado.
- **POST** /api/contribuyente/addContribuyente: Agrega un nuevo contribuyente al sistema.
- **POST** /api/contribuyente/addComprobante: Registra un nuevo comprobante fiscal.
- **DELETE** /api/contribuyente/deleteContribuyente/{rncCedula}: Elimina un contribuyente si no tiene comprobantes fiscales asociados.
- **DELETE** /api/contribuyente/deleteComprobante/{rncCedula}: Elimina un comprobante fiscal.

## Logs
La aplicación utiliza Serilog para el registro de logs tanto en la consola como en archivos. Los logs se almacenan en la ruta logs/log.txt, con archivos rotativos por día.

## Seguridad
Se ha configurado CORS para permitir el acceso desde cualquier origen. En caso de necesitar restricciones más estrictas, se puede modificar esta configuración en el archivo Program.cs.