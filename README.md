# ERPOrigenAPI

![.NET 10](https://img.shields.io/badge/.NET-10-512BD4?logo=dotnet)
![SQL Server 2026](https://img.shields.io/badge/SQL_Server-2026-CC2927?logo=microsoft-sql-server)
![C# 14](https://img.shields.io/badge/C%23-14-239120?logo=csharp)

**ERPOrigenAPI** es una RESTful API desarrollada como prueba de concepto (PoC) para demostrar la integración fluida entre el framework **.NET 10** y el motor de base de datos **SQL Server Express 2026**. El proyecto destaca por la implementación de una arquitectura limpia, manejo centralizado de flujos y lógica transversal robusta.

---

## 🚀 Ficha Técnica

| Componente | Especificación |
| :--- | :--- |
| **Framework** | .NET 10 |
| **Lenguaje** | C# 14 |
| **Motor de BD** | SQL Server Express 2026 |
| **Arquitectura** | MVC (Model-View-Controller) |
| **Protocolo** | HTTPS / REST |

---

## 🏗️ Arquitectura de Software

### 1. Estructura de Capas
El proyecto aplica una separación de responsabilidades clara para facilitar el mantenimiento:
- **Controllers**: Orquestación de peticiones y respuestas HTTP.
- **Models**: Definición de entidades de negocio y esquemas de datos.
- **Data**: Capa de persistencia y contexto de base de datos mediante *Entity Framework Core*.

### 2. Flujo de Datos Directo
Con fines demostrativos, esta API opera mediante el paso directo de **Entidades de Dominio**, omitiendo el uso de DTOs (Data Transfer Objects). Esto permite validar de forma simplificada la integridad de la conexión y la serialización nativa del framework.

### 3. Gestión Transversal (Cross-Cutting Concerns)
Implementación de lógica mediante **Middlewares** para mantener un código limpio:
- **Global Exception Handling**: Captura centralizada de errores para evitar bloques `try-catch` redundantes.
- **Middleware de Logging**: Registro automático de telemetría y eventos de las solicitudes (requests/responses).

### 4. Versionado
Se incluye **API Versioning** (vía encabezados o rutas), lo que garantiza la escalabilidad del servicio y la coexistencia de múltiples versiones del contrato de la API.

---

## 🔐 Seguridad y Comunicaciones

> [!IMPORTANT]  
> Este proyecto está configurado exclusivamente para un **entorno de desarrollo controlado**.

- **Autenticación**: Acceso sin encabezados complejos (Stateless/No-Auth).
- **Protocolo**: Comunicación directa vía HTTPS con payloads en JSON plano.
- **Data Integrity**: Procesamiento de información en formato original sin desencriptación intermedia.
- **Data Privacy**: La cadena de conexión (*connection string*) reside en texto plano en los archivos de configuración (sin Azure Key Vault u otros gestores de secretos).

---

## 💻 Requisitos del Sistema

Para ejecutar este proyecto, asegúrate de contar con:
1. **Runtime**: [.NET 10 SDK](https://dotnet.microsoft.com/download) o superior.
2. **Base de Datos**: Instancia activa de SQL Server Express 2026.
3. **Pruebas**: Postman, Insomnia o el Swagger UI incorporado en la solución.

---

## 🛠️ Instalación y Configuración

1. **Clonar el repositorio:**
   ```bash
   git clone [https://github.com/KosmeFulanito777/ERPOrigenAPI.git](https://github.com/KosmeFulanito777/ERPOrigenAPI.git)
