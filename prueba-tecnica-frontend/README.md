# Aplicación Web - Frontend en React para la Gestión de Contribuyentes y Comprobantes Fiscales

## Descripción del Proyecto

Esta aplicación web frontend ha sido desarrollada utilizando React y está diseñada para interactuar con una API .NET. Permite gestionar contribuyentes y comprobantes fiscales. Los usuarios pueden agregar nuevos contribuyentes y comprobantes, consultar listas de contribuyentes, filtrar por diferentes criterios y ver detalles de los comprobantes fiscales asociados.

## Estructura del Proyecto

- **App.js:** Configura el enrutamiento de la aplicación mediante `react-router-dom`.
- **components/contribuyentesLista.js:** Componente para listar y filtrar contribuyentes.
- **components/comprobantesPage.js:** Muestra los comprobantes fiscales y el total de ITBIS de un contribuyente.
- **components/addContribuyente.js:** Formulario para registrar nuevos contribuyentes.
- **components/addComprobante.js:** Formulario para registrar nuevos comprobantes fiscales.
- **components/inicio.js:** Página de inicio con acceso rápido a las funcionalidades principales del sistema.

## Requisitos del Sistema

- Node.js (versión 14.x o superior).
- npm o yarn.
- La API de backend en .NET ejecutándose localmente en `http://localhost:5298`.

## Configuración Inicial

1. Clonar el repositorio:

   ```bash
   git clone https://github.com/usuario/proyecto-frontend.git
   cd proyecto-frontend

2. Instalar las dependencias:

    npm install

3. Configurar el archivo .env con la URL de la API en el backend:

    REACT_APP_API_URL=http://localhost:5298/api

4. Ejecutar la aplicación en modo de desarrollo:

    npm start
    Esto abrirá la aplicación en http://localhost:3000.

## Funcionalidades Principales
1. **Página de Inicio**: Ofrece acceso rápido a las principales funcionalidades de la aplicación, como la lista de contribuyentes, la adición de nuevos contribuyentes y comprobantes fiscales.

2. **Listado de Contribuyentes**:

- Filtrado por nombre, tipo de contribuyente (Persona Física o Jurídica) y estatus (Activo o Inactivo).
- Navegación a la página de detalles de cada contribuyente.

3. **Agregar Contribuyente**:

Formulario para registrar nuevos contribuyentes, validando los datos como el nombre, tipo de contribuyente, RNC/Cédula y estatus.

4. **Agregar Comprobante**:

Formulario que permite registrar nuevos comprobantes fiscales, calculando automáticamente el ITBIS basado en el monto ingresado.

5. **Detalles del Contribuyente**:

- Muestra información completa sobre el contribuyente y lista los comprobantes fiscales asociados.
- Incluye el cálculo total del ITBIS acumulado de los comprobantes.

## Interacción con la API
La aplicación utiliza Axios para realizar las solicitudes HTTP a la API de .NET. Los servicios interactúan con los siguientes endpoints:

- **GET** /api/contribuyente: Recupera la lista de contribuyentes.
- **GET** /api/contribuyente/{rncCedula}: Obtiene los detalles de un contribuyente específico.
- **GET** /api/contribuyente/{rncCedula}/comprobantes: Lista los comprobantes fiscales de un contribuyente y calcula el total del ITBIS.
- **POST** /api/contribuyente/addContribuyente: Registra un nuevo contribuyente.
- **POST** /api/contribuyente/addComprobante: Registra un nuevo comprobante fiscal.
