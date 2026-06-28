# Guía de Ejecución: Dispensario Médico UNAPEC

Este documento proporciona las instrucciones paso a paso para configurar y ejecutar correctamente el proyecto del Dispensario Médico desarrollado en C# WinForms y SQL Server.

## Requisitos Previos
1. **SQL Server**: Instancia de SQL Server (Express o Developer) ejecutándose localmente o en un servidor accesible.
2. **SQL Server Management Studio (SSMS)** o Azure Data Studio para ejecutar los scripts de base de datos.
3. **Visual Studio 2019 o superior**: Con la carga de trabajo "Desarrollo de escritorio de .NET" (WinForms) instalada.
4. **.NET Framework**: Versión 4.7.2 o superior instalada.

---

## Paso 1: Configurar la Base de Datos

El sistema no utilizará un ORM (como Entity Framework) para generar la base de datos automáticamente, sino que requiere la creación manual mediante los scripts proporcionados en la carpeta `Database`.

1. Abre SQL Server Management Studio (SSMS).
2. Conéctate a tu servidor de base de datos local (por ejemplo, `(localdb)\MSSQLLocalDB`, `localhost` o `.\SQLEXPRESS`).
3. Crea una nueva base de datos llamada `DispensarioMedico`:
   ```sql
   CREATE DATABASE DispensarioMedico;
   GO
   USE DispensarioMedico;
   GO
   ```
4. Ejecuta los scripts SQL provistos en la carpeta `Database` en el siguiente orden para evitar problemas con las llaves foráneas:
   1. `TipoFarmaco.sql`
   2. `Marca.sql`
   3. `Ubicacion.sql`
   4. `Medicamento.sql`
   5. `Paciente.sql`
   6. `Medico.sql`
   7. `RegistroVisitas.sql`

*(Cada script creará sus tablas correspondientes junto con todos sus Procedimientos Almacenados).*

---

## Paso 2: Configurar la Cadena de Conexión (Connection String)

Los archivos DAL (Data Access Layer) se comunican directamente con la base de datos a través de ADO.NET.

1. Abre la carpeta `DAL` y busca cualquier archivo (ej. `MedicamentoDAL.cs` o `RegistroVisitaDAL.cs`).
2. Identifica la línea que contiene la cadena de conexión:
   ```csharp
   private readonly string connectionString = "Server=TU_SERVIDOR;Database=DispensarioMedico;Integrated Security=True;";
   ```
3. Reemplaza `TU_SERVIDOR` por el nombre real de tu servidor SQL (ej. `localhost`, `.\SQLEXPRESS` o el nombre de tu máquina). 
*(Nota: Para un proyecto final en producción, lo ideal es mover esta cadena al archivo `App.config` del proyecto e instanciarla desde el `ConfigurationManager`).*

---

## Paso 3: Integrar los Archivos al Proyecto en Visual Studio

Si los archivos `.cs` fueron generados fuera de Visual Studio, debes incluirlos en el proyecto para que el compilador los reconozca.

1. Abre Visual Studio y selecciona "Crear un proyecto nuevo".
2. Selecciona **Aplicación de Windows Forms (.NET Framework)** en C#.
3. Nombra el proyecto `DispensarioMedico`.
4. Una vez creado el proyecto, ve al **Explorador de soluciones** (Solution Explorer).
5. Copia y pega las carpetas generadas (`Entities`, `DAL`, `BLL`, `UI`) directamente en el proyecto en Visual Studio, o haz clic derecho en el proyecto -> **Agregar** -> **Elemento existente** e incluye todos los archivos.
6. Asegúrate de que los espacios de nombres (`namespace DispensarioMedico.UI`, etc.) coincidan con el nombre de tu proyecto.

---

## Paso 4: Configurar el Formulario de Inicio

Por defecto, Visual Studio arranca con un `Form1`. Debes cambiar esto para que el proyecto arranque visualizando uno de los módulos principales.

1. En el Explorador de Soluciones, abre el archivo `Program.cs`.
2. Busca la línea de código dentro de `Main()` que dice:
   ```csharp
   Application.Run(new Form1());
   ```
3. Cámbiala por el formulario que deseas probar primero. Asegúrate de importar el namespace de la UI (`using DispensarioMedico.UI;`).
   ```csharp
   Application.Run(new FrmRegistroVisitas()); // o FrmMedicamento(), FrmPaciente(), etc.
   ```

*(Idealmente, debes crear un formulario contenedor (MDI Parent) con un menú principal que permita abrir los demás formularios, y configurar este formulario MDI en el `Program.cs`).*

---

## Paso 5: Compilar y Ejecutar

1. Compila la solución presionando **F6** o yendo a **Compilar** -> **Compilar solución** (Build Solution). Verifica que no haya errores de sintaxis en la ventana de Salida.
2. Presiona **F5** (o el botón "Iniciar" en la barra de herramientas) para correr la aplicación.
3. El formulario se abrirá. Puedes probar haciendo un insert o interactuando con el sistema; si la configuración de BD y la conexión son correctas, los datos se guardarán inmediatamente.
