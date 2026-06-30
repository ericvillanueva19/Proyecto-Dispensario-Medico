# Descripción de Módulos: Dispensario Médico UNAPEC

Este documento detalla el propósito de cada módulo desarrollado dentro del sistema del Dispensario Médico, explicando su función de negocio y cómo se relaciona con otros módulos. El sistema está diseñado en una arquitectura de 3 capas (UI, BLL, DAL) para asegurar mantenibilidad y desacoplamiento.

---

## 1. Módulo "Tipos de Fármacos"
**Propósito:** Es un catálogo (maestro) básico. Permite agrupar los medicamentos por su clasificación médica (Ej. Analgésicos, Antibióticos, Antipiréticos).
**Mecánica:** Los usuarios pueden agregar nuevas clasificaciones que alimentarán posteriormente el registro de medicamentos.
**Dependencias:** Módulo independiente.

## 2. Módulo "Marcas" (Laboratorios)
**Propósito:** Es un catálogo para registrar los fabricantes o laboratorios de los medicamentos (Ej. Pfizer, Bayer, Sued Fargesa).
**Mecánica:** Provee la gestión básica de Marcas, evitando la redundancia en los nombres para garantizar uniformidad.
**Dependencias:** Módulo independiente.

## 3. Módulo "Ubicaciones"
**Propósito:** Control logístico del inventario. Sirve para definir la ubicación física de un medicamento dentro de la farmacia o almacén del dispensario.
**Mecánica:** Permite identificar exactamente dónde se encuentra un producto mediante variables más precisas: "Estante", "Tramo" y "Celda".
**Dependencias:** Módulo independiente.

## 4. Módulo "Medicamentos"
**Propósito:** Es el registro consolidado del inventario de farmacia. Permite registrar un fármaco en el sistema detallando su nombre y dosis (Ej. Ibuprofeno 400mg).
**Mecánica:** Enlaza la información de los 3 módulos anteriores. Al crear un medicamento, el sistema exige indicar de qué *Tipo* es, qué *Marca* lo fabrica, y en qué *Ubicación* del almacén está guardado.
**Dependencias:** Consume información directamente de "Tipos de Fármacos", "Marcas" y "Ubicaciones".

## 5. Módulo "Pacientes"
**Propósito:** Gestor del registro clínico o ficha base de las personas que reciben servicios en el dispensario médico de UNAPEC.
**Mecánica:** Almacena datos como Nombre, Cédula (única por paciente) y Número de Carnet. Implementa una regla de negocio que clasifica al paciente estrictamente como: *Estudiante, Empleado, Profesor* u *Otros*.
**Dependencias:** Módulo independiente.

## 6. Módulo "Médicos"
**Propósito:** Gestor del personal de salud de la institución.
**Mecánica:** Almacena el listado de médicos, sus especialidades y la tanda en la que laboran (*Matutina, Vespertina, Nocturna, Jornada Completa*). Evita que un médico sea registrado dos veces validando la unicidad de su cédula.
**Dependencias:** Módulo independiente.

## 7. Módulo "Registro y Consulta de Visitas"
**Propósito:** Es el módulo **transaccional central** y núcleo de la aplicación. Representa la acción de atención médica, documentando cada encuentro entre un médico y un paciente.
**Mecánica:**
Este módulo se divide en dos secciones operativas (en un control de pestañas):
*   **A. Registro de Visita:** Permite crear la transacción de consulta médica, asociando *qué Médico* atendió a *qué Paciente*, *cuándo*, cuáles fueron los *Síntomas* presentados, qué *Recomendaciones* (diagnóstico) se emitieron y qué *Medicamento* específico le fue recetado del inventario de farmacia.
*   **B. Consulta Avanzada (Historial):** Es un buscador o reporteador dinámico. Permite al personal administrativo buscar visitas médicas anteriores aplicando múltiples filtros: ver todas las visitas de un Paciente en específico, consultar todo el historial de consultas hechas por un Médico en particular, o filtrar el registro de operaciones por fechas exactas.

**Dependencias:** Es el módulo de mayor acoplamiento. Consume obligatoriamente registros de "Médicos", "Pacientes" y "Medicamentos".

---

## 8. Módulo "Autenticación de Usuarios (Login)"
**Propósito:** Es la **puerta de entrada y control de acceso** al sistema. Verifica la identidad de cada usuario antes de permitirle operar cualquier módulo, y redirige su experiencia según el rol que tenga asignado.

**Mecánica:**
El módulo opera completamente **en memoria** (sin tabla SQL adicional) usando un repositorio estático simulado con tres usuarios de prueba precargados. El flujo de autenticación es el siguiente:

1.  El usuario ingresa su correo y contraseña en el formulario `FrmLogin`.
2.  La **BLL** (`UsuarioBLL`) aplica tres capas de seguridad antes de consultar los datos:
    *   **Sanitización:** valida el formato del correo con una expresión regular RFC-5322 y rechaza contraseñas con caracteres sospechosos de inyección.
    *   **Control de fuerza bruta:** bloquea temporalmente la cuenta por 30 segundos tras 3 intentos fallidos consecutivos. La UI muestra una cuenta regresiva en tiempo real.
    *   **Retardo anti-timing:** introduce una espera artificial de 1.5 segundos para dificultar ataques de temporización.
3.  La **DAL** (`UsuarioDAL`) busca el usuario por correo y compara la contraseña mediante su hash SHA-256 usando una comparación en tiempo constante (XOR bit a bit).
4.  Si las credenciales son válidas, el sistema abre el panel correspondiente al **rol** del usuario:
    *   **`FrmPanelAdmin`** (rol `admin`): Menú lateral con secciones de Usuarios, Reportes y Configuración.
    *   **`FrmPanelCliente`** (rol `cliente`): Menú lateral con secciones de Mis Pedidos, Facturas y Mi Perfil.
5.  Al cerrar sesión, `SesionActual.Limpiar()` nulifica la referencia del usuario en memoria RAM.

**Credenciales de prueba:**

| Correo | Contraseña | Rol |
|---|---|---|
| `admin@unapec.edu.do` | `Admin@2024` | admin |
| `cliente@unapec.edu.do` | `Cliente@2024` | cliente |
| `juan.perez@unapec.edu.do` | `Juan@5678` | cliente |

**Dependencias:** Módulo independiente (no requiere SQL Server). Es el punto de entrada principal del sistema; todos los demás módulos quedan protegidos detrás de este login.
