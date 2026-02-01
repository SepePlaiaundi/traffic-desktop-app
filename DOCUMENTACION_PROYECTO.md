# Documentación del Proyecto: Traffic Desktop App

## 1. Introducción

**Traffic Desktop App** es una solución integral desarrollada en WPF (.NET) para la monitorización y gestión de incidencias de tráfico en tiempo real. La aplicación permite visualizar cámaras de tráfico, gestionar un histórico de incidencias y generar reportes analíticos para la toma de decisiones.

## 2. Guía de Usuario

### Pantalla Principal (Dashboard)

El centro de control ofrece una vista rápida de:

* **Estado de Cámaras**: Conexión en vivo con los puntos de monitorización.
* **Métricas**: Resumen visual de las incidencias del mes.

### Gestión de Incidencias

Para registrar un suceso:

1. Acceda al apartado de **Incidencias**.
2. Pulse **"Crear Incidencia"**.
3. Rellene los datos de ubicación, tipo (Accidente, Obras, Retención) y descripción.
4. El sistema almacenará y notificará el registro inmediatamente.

### Reportes

La aplicación incluye un motor de generación de PDF (**QuestPDF**) que permite exportar el estado actual del tráfico con un solo clic.

## 3. Arquitectura Técnica (MVVM)

El proyecto ha sido estructurado siguiendo el patrón **Model-View-ViewModel**, garantizando un código limpio, testeable y mantenible.

* **Models**: Clases de datos puras (`Incidence`, `Camera`, `User`).
* **Views**: Definición de la interfaz en XAML para una separación total del diseño y la lógica.
* **ViewModels**: Lógica de presentación y comunicación con los servicios.
* **Services**: Capa de abstracción para el acceso a datos (API y base de datos).

## 4. Tecnologías Utilizadas

* **Lenguaje**: C# / .NET
* **Interfaz**: WPF (Windows Presentation Foundation)
* **Estilos**: Diccionarios de recursos XAML para un diseño moderno y minimalista.
* **Librerías Clave**:
  * `Newtonsoft.Json`: Para el manejo de datos API.
  * `QuestPDF`: Para la generación de documentación técnica.
  * `LiveCharts`: Para las representaciones gráficas en el Dashboard.

## 5. Instrucciones de Instalación

1. Clonar el repositorio.
2. Abrir `TrafficDesktopApp.sln` en Visual Studio 2022.
3. Restaurar los paquetes NuGet.
4. Ejecutar el proyecto (F5).

---

*Documentación generada para la entrega del proyecto final.*
