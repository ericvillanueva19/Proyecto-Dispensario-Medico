# Contexto del Desarrollo: Módulo "Tipos de Fármacos", "Marcas", "Ubicaciones", "Medicamentos", "Pacientes", "Médicos" y "Registro/Consulta de Visitas"

Este documento resume el trabajo realizado para la implementación de los módulos del sistema *Dispensario Médico UNAPEC*. El desarrollo siguió los requerimientos establecidos para una aplicación C# WinForms (.NET Framework) con SQL Server, implementando una arquitectura multicapas totalmente desacoplada.

## 1. Arquitectura y Patrón de Diseño
El código se estructuró dividiendo las responsabilidades lógicas en tres capas principales:

*   **UI (Capa de Presentación):** Formularios Windows (WinForms) para la interacción con el usuario.
*   **BLL (Capa Lógica de Negocio):** Clases que procesan reglas del negocio y validaciones antes de interactuar con los datos.
*   **DAL (Capa de Acceso a Datos):** Clases encargadas de conectarse con SQL Server (mediante ADO.NET puro) e invocar Procedimientos Almacenados.
*   **Entities (Entidades de Negocio):** Objetos de transferencia de datos (POCOs) que viajan entre las diferentes capas.

---

## 2. Trabajo Realizado: Módulo "Tipos de Fármacos"

### A. Agente Arquitecto BD (Base de Datos)
**Archivo generado:** `Database/TipoFarmaco.sql`
*   **Tabla:** Se creó la tabla `TipoFarmaco` con los campos `ID` (INT PK Identity), `Descripcion` (VARCHAR 100), y `Estado` (BIT, por defecto activo).
*   **Procedimientos Almacenados (Stored Procedures):**
    *   `sp_TipoFarmaco_Insertar`
    *   `sp_TipoFarmaco_Actualizar`
    *   `sp_TipoFarmaco_Inactivar`: Implementa el borrado lógico (Soft delete) cambiando el campo Estado a 0.
    *   `sp_TipoFarmaco_Listar`: Devuelve los registros ordenados alfabéticamente para cargar el DataGridView.
    *   `sp_TipoFarmaco_Existe`: Procedimiento auxiliar que verifica en BD si una descripción ya está registrada (útil para la BLL y evitar duplicados).

### B. Agente Backend Dev (Lógica y Acceso a Datos)
**Archivos generados:** `Entities/TipoFarmaco.cs`, `DAL/TipoFarmacoDAL.cs`, `BLL/TipoFarmacoBLL.cs`
*   **Entidad (`TipoFarmaco.cs`):** Clase simple (POCO) con las propiedades `ID`, `Descripcion`, y `Estado`.
*   **Acceso a Datos (`TipoFarmacoDAL.cs`):** 
    *   Se implementó ADO.NET con `SqlConnection` y `SqlCommand`.
    *   Se configuró explícitamente para llamar a los Stored Procedures (CommandType.StoredProcedure).
*   **Lógica de Negocio (`TipoFarmacoBLL.cs`):** 
    *   Valida descripciones duplicadas antes de insertar o modificar.

### C. Agente Frontend Dev (Presentación / WinForms)
**Archivos generados:** `UI/FrmTipoFarmaco.cs`, `UI/FrmTipoFarmaco.Designer.cs`
*   **Interfaz Gráfica:** 
    *   Formulario WinForm clásico con controles de texto, checkbox, botones y DataGridView.
*   **Código Behind (`FrmTipoFarmaco.cs`):**
    *   Atrapa excepciones de la BLL y muestra mensajes al usuario. El DataGridView tiene implementado CellDoubleClick para editar.

---

## 3. Trabajo Realizado: Módulo "Marcas" (Laboratorios)

Al igual que en el módulo de Tipos de Fármacos, se siguió la misma arquitectura multicapas para las Marcas.

### A. Agente Arquitecto BD (Base de Datos)
**Archivo generado:** `Database/Marca.sql`
*   **Tabla:** `Marca` con `ID`, `Descripcion`, y `Estado`.
*   **Stored Procedures:** `sp_Marca_Insertar`, `sp_Marca_Actualizar`, `sp_Marca_Inactivar`, `sp_Marca_Listar`, y `sp_Marca_Existe`.

### B. Agente Backend Dev (Lógica y Acceso a Datos)
**Archivos generados:** `Entities/Marca.cs`, `DAL/MarcaDAL.cs`, `BLL/MarcaBLL.cs`
*   **Entidad (`Marca.cs`):** Estructura que representa la marca con ID, Descripción y Estado.
*   **DAL (`MarcaDAL.cs`):** Ejecuta la persistencia llamando a los SPs definidos en la BD.
*   **BLL (`MarcaBLL.cs`):** Valida longitud, datos vacíos y repetición de descripción en la base de datos (con método `Existe`) antes del guardado.

### C. Agente Frontend Dev (Presentación / WinForms)
**Archivos generados:** `UI/FrmMarca.cs`, `UI/FrmMarca.Designer.cs`
*   **Interfaz Gráfica:** Formulario maestro diseñado para gestionar Marcas con DataGridView (con doble click para editar), y los botones CRUD correspondientes.
*   **Conexión Lógica:** Gestiona la carga automática de la tabla al iniciar, e interviene con la capa BLL para validar inserciones e inactivaciones.

---

## 4. Trabajo Realizado: Módulo "Ubicaciones"

Este módulo expande el registro con campos técnicos adicionales para el control físico.

### A. Agente Arquitecto BD (Base de Datos)
**Archivo generado:** `Database/Ubicacion.sql`
*   **Tabla:** `Ubicacion` con `ID`, `Descripcion`, `Estante`, `Tramo`, `Celda`, y `Estado`.
*   **Stored Procedures:** Se definieron `sp_Ubicacion_Insertar`, `sp_Ubicacion_Actualizar`, `sp_Ubicacion_Inactivar`, `sp_Ubicacion_Listar`, y `sp_Ubicacion_Existe`, integrando los nuevos campos técnicos.

### B. Agente Backend Dev (Lógica y Acceso a Datos)
**Archivos generados:** `Entities/Ubicacion.cs`, `DAL/UbicacionDAL.cs`, `BLL/UbicacionBLL.cs`
*   **Entidad (`Ubicacion.cs`):** Incorporación de las nuevas propiedades string para Estante, Tramo y Celda.
*   **DAL (`UbicacionDAL.cs`):** Actualización de comandos SQL (SqlCommand) para agregar los nuevos parámetros en los procesos de Inserción y Actualización.
*   **BLL (`UbicacionBLL.cs`):** Incorpora validación rigurosa asegurando que ninguno de los campos técnicos (Estante, Tramo, Celda) llegue vacío a la base de datos ni exceda los 50 caracteres (según límite del VARCHAR).

### C. Agente Frontend Dev (Presentación / WinForms)
**Archivos generados:** `UI/FrmUbicacion.cs`, `UI/FrmUbicacion.Designer.cs`
*   **Interfaz Gráfica:** Formulario WinForm estructurado con labels y textboxes individuales para la Descripción y los 3 atributos físicos, ajustando la interfaz para mostrarlos apropiadamente junto a la grilla y controles de acción.
*   **Conexión Lógica:** Se programó el llenado de todos estos campos tras el `CellDoubleClick` en la grilla y la recolección de los mismos hacia la entidad al momento de guardar.

---

## 5. Trabajo Realizado: Módulo "Medicamentos"

Este es el módulo central del sistema, que consume todos los catálogos anteriores vinculándolos mediante llaves foráneas.

### A. Agente Arquitecto BD (Base de Datos)
**Archivo generado:** `Database/Medicamento.sql`
*   **Tabla:** `Medicamento` con llaves foráneas a `TipoFarmaco`, `Marca` y `Ubicacion`.
*   **Stored Procedures:** Se implementaron los SPs CRUD estándar. El SP `sp_Medicamento_Listar` se diseñó utilizando `INNER JOIN`s hacia las 3 tablas de catálogos para extraer las descripciones textuales y hacerlas amigables para el usuario final en la interfaz, sin comprometer la normalización.

### B. Agente Backend Dev (Lógica y Acceso a Datos)
**Archivos generados:** `Entities/Medicamento.cs`, `DAL/MedicamentoDAL.cs`, `BLL/MedicamentoBLL.cs`
*   **Entidad (`Medicamento.cs`):** Se crearon propiedades para manejar los IDs (`TipoFarmacoID`, etc.) así como propiedades accesorias tipo string (`TipoFarmacoDescripcion`, etc.) para facilitar el mapeo hacia la UI (DataGridView).
*   **BLL (`MedicamentoBLL.cs`):** Valida que la descripción o dosis no lleguen vacías o excedan límites. Principalmente, verifica que las selecciones de las 3 llaves foráneas sean `> 0`, garantizando la integridad relacional de los datos a nivel de negocio.

### C. Agente Frontend Dev (Presentación / WinForms)
**Archivos generados:** `UI/FrmMedicamento.cs`, `UI/FrmMedicamento.Designer.cs`
*   **Interfaz Gráfica:** Un formulario orquestador con tres `ComboBoxes` (DropDownLists), TextBoxes para Descripción y Dosis, y el datagrid.
*   **Conexión Lógica:** Al arrancar el formulario (`Load`), se inicializan internamente instancias de las BLLs previas (`TipoFarmacoBLL`, `MarcaBLL`, `UbicacionBLL`) para llenar los combos mediante el método `CargarCombos()`. Además, se ocultaron las columnas de IDs en el DataGridView para solo mostrar las descripciones extraídas por los JOINs del SQL.

---

## 6. Trabajo Realizado: Módulo "Pacientes"

Este módulo gestiona la información de los pacientes que visitan el dispensario médico, aplicando reglas institucionales.

### A. Agente Arquitecto BD (Base de Datos)
**Archivo generado:** `Database/Paciente.sql`
*   **Tabla:** Se generó la tabla `Paciente` incluyendo campos como `Nombre`, `Cedula`, `No_Carnet` y `TipoPaciente`.
*   **Stored Procedures:** Se programaron los SPs estándar (Insertar, Actualizar, Inactivar, Listar). Se incluyó adicionalmente un `sp_Paciente_ExisteCedula` para validar la unicidad de las cédulas al registrar.

### B. Agente Backend Dev (Lógica y Acceso a Datos)
**Archivos generados:** `Entities/Paciente.cs`, `DAL/PacienteDAL.cs`, `BLL/PacienteBLL.cs`
*   **Entidad (`Paciente.cs`):** Representa al paciente en memoria.
*   **BLL (`PacienteBLL.cs`):** Se incluyó una regla de negocio estricta (`tiposPermitidos`) donde el campo `TipoPaciente` es evaluado contra una lista fija: "Estudiante", "Empleado", "Profesor" u "Otros". De enviar un tipo distinto, la BLL rechazará el guardado. También verifica que la Cédula no se duplique usando la función expuesta por la DAL.

### C. Agente Frontend Dev (Presentación / WinForms)
**Archivos generados:** `UI/FrmPaciente.cs`, `UI/FrmPaciente.Designer.cs`
*   **Interfaz Gráfica:** Formulario WinForm provisto de TextBox para Nombre, Cédula y No. Carnet. Se agregó un `ComboBox` que se pre-llena estáticamente con los 4 tipos institucionales.
*   **Conexión Lógica:** El formulario envía la opción textual seleccionada del combo hacia la entidad para que la BLL la valide y posteriormente la DAL la persista.

---

## 7. Trabajo Realizado: Módulo "Médicos"

Este módulo gestiona la información del personal médico de la institución.

### A. Agente Arquitecto BD (Base de Datos)
**Archivo generado:** `Database/Medico.sql`
*   **Tabla:** Se generó la tabla `Medico` incluyendo campos como `Nombre`, `Cedula`, `Tanda_Labor` y `Especialidad`.
*   **Stored Procedures:** Se programaron los SPs estándar (Insertar, Actualizar, Inactivar, Listar) junto a `sp_Medico_ExisteCedula` para el control de redundancia.

### B. Agente Backend Dev (Lógica y Acceso a Datos)
**Archivos generados:** `Entities/Medico.cs`, `DAL/MedicoDAL.cs`, `BLL/MedicoBLL.cs`
*   **Entidad (`Medico.cs`):** Entidad POCO que transporta la data del médico.
*   **BLL (`MedicoBLL.cs`):** Lógica que obliga a llenar datos cruciales. Lanza excepciones si no se ha establecido la Tanda Laboral o si se exceden los topes de caracteres permitidos (ej. Tanda_Labor > 50). También usa el método `ExisteCedula` desde la DAL para evitar médicos repetidos.

### C. Agente Frontend Dev (Presentación / WinForms)
**Archivos generados:** `UI/FrmMedico.cs`, `UI/FrmMedico.Designer.cs`
*   **Interfaz Gráfica:** Un WinForm completo que dispone campos de texto para los datos y un `ComboBox` para la Tanda Laboral.
*   **Conexión Lógica:** El ComboBox se precarga con opciones como: "Matutina (Mañana)", "Vespertina (Tarde)", "Nocturna (Noche)" y "Jornada Completa". El formulario controla el flujo CRUD vinculándolo con el DataGrid y mostrando alertas de la capa BLL.

---

## 8. Trabajo Realizado: Módulo "Registro y Consultas de Visitas"

Este módulo es el núcleo transaccional del sistema, unificando a Médicos, Pacientes y Medicamentos en un solo flujo con capacidades de búsqueda dinámica.

### A. Agente Arquitecto BD (Base de Datos)
**Archivo generado:** `Database/RegistroVisitas.sql`
*   **Tabla:** Se generó la tabla `RegistroVisitas` con las llaves foráneas correspondientes a Médico, Paciente y Medicamento, además de campos para Fecha, Hora, Síntomas y Recomendaciones.
*   **Stored Procedures:** 
    *   `sp_RegistroVisita_Insertar`: Almacena la visita en la BD.
    *   `sp_RegistroVisita_Consultar`: Es un SP **Dinámico** que utiliza `INNER JOINs` para extraer los nombres/descripciones y aplica lógica `(@Parametro IS NULL OR Columna = @Parametro)` para filtrar registros solo por los criterios provistos (Médico, Paciente, o Fecha).

### B. Agente Backend Dev (Lógica y Acceso a Datos)
**Archivos generados:** `Entities/RegistroVisita.cs`, `DAL/RegistroVisitaDAL.cs`, `BLL/RegistroVisitaBLL.cs`
*   **Entidad (`RegistroVisita.cs`):** Incorpora las propiedades nativas y campos extendidos (`MedicoNombre`, `PacienteNombre`, `MedicamentoDescripcion`) requeridos por la vista.
*   **DAL (`RegistroVisitaDAL.cs`):** Procesa la insersión y, sobre todo, gestiona la consulta convirtiendo los parámetros `null` de C# en `DBNull.Value` para que la búsqueda dinámica en SQL Server interprete correctamente qué filtros aplicar.
*   **BLL (`RegistroVisitaBLL.cs`):** Valida de manera estricta que los campos foráneos no viajen nulos (validación `> 0`) y que los campos de texto extenso (Sintomas y Recomendaciones) no estén vacíos antes de enviarlos a la BD.

### C. Agente Frontend Dev (Presentación / WinForms)
**Archivos generados:** `UI/FrmRegistroVisitas.cs`, `UI/FrmRegistroVisitas.Designer.cs`
*   **Interfaz Gráfica:** Se implementó un control `TabControl` con dos apartados:
    *   **Registro de Visita:** Donde se seleccionan desde ComboBoxes el Médico, Paciente y Medicamento (instanciando a sus respectivos BLLs en el load), junto a controles DateTimePicker y cuadros de texto multi-línea.
    *   **Consulta Avanzada:** Un panel con ComboBoxes para filtrar por Paciente o Médico, y un CheckBox que activa un filtro por Fecha, mostrando los resultados consolidados en un `DataGridView`.
*   **Conexión Lógica:** Envía los valores de los DropDowns a la capa BLL, limpiando los controles en caso de éxito, e invoca el motor de búsqueda renderizando el resultado amigablemente, ocultando los IDs crudos en la grilla y exponiendo solo el texto legible.

---

## Próximos Pasos (To-Do)
Para que los módulos funcionen en el entorno final:
1.  **Ejecutar Scripts DB:** Correr los 7 scripts SQL generados en la base de datos `DispensarioMedico`.
2.  **Configurar Connection String:** Centralizar la conexión que actualmente está referenciada en el código de las DALs.
3.  **Integrar a Visual Studio:** Asegurar que los formularios y clases estén incluidos en el `.csproj` del proyecto y enlazar el menú principal (`MDIParent`).
