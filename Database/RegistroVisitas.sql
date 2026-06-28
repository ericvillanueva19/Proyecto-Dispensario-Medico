-- Database/RegistroVisitas.sql
CREATE TABLE RegistroVisitas (
    ID_Visita INT PRIMARY KEY IDENTITY(1,1),
    MedicoID INT NOT NULL,
    PacienteID INT NOT NULL,
    Fecha_Visita DATE NOT NULL,
    Hora_Visita TIME NOT NULL,
    Sintomas VARCHAR(MAX) NOT NULL,
    MedicamentoID INT NOT NULL,
    Recomendaciones VARCHAR(MAX) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1
);
GO

-- PROCEDURE: Insertar
CREATE PROCEDURE sp_RegistroVisita_Insertar
    @MedicoID INT,
    @PacienteID INT,
    @Fecha_Visita DATE,
    @Hora_Visita TIME,
    @Sintomas VARCHAR(MAX),
    @MedicamentoID INT,
    @Recomendaciones VARCHAR(MAX),
    @Estado BIT
AS
BEGIN
    INSERT INTO RegistroVisitas (MedicoID, PacienteID, Fecha_Visita, Hora_Visita, Sintomas, MedicamentoID, Recomendaciones, Estado)
    VALUES (@MedicoID, @PacienteID, @Fecha_Visita, @Hora_Visita, @Sintomas, @MedicamentoID, @Recomendaciones, @Estado)
END
GO

-- PROCEDURE: Consulta Dinámica
CREATE PROCEDURE sp_RegistroVisita_Consultar
    @MedicoID INT = NULL,
    @PacienteID INT = NULL,
    @Fecha DATE = NULL
AS
BEGIN
    SELECT 
        v.ID_Visita,
        v.MedicoID,
        m.Nombre AS MedicoNombre,
        v.PacienteID,
        p.Nombre AS PacienteNombre,
        v.Fecha_Visita,
        v.Hora_Visita,
        v.Sintomas,
        v.MedicamentoID,
        med.Descripcion AS MedicamentoDescripcion,
        v.Recomendaciones,
        v.Estado
    FROM RegistroVisitas v
    INNER JOIN Medico m ON v.MedicoID = m.ID
    INNER JOIN Paciente p ON v.PacienteID = p.ID
    INNER JOIN Medicamento med ON v.MedicamentoID = med.ID
    WHERE v.Estado = 1
      AND (@MedicoID IS NULL OR v.MedicoID = @MedicoID)
      AND (@PacienteID IS NULL OR v.PacienteID = @PacienteID)
      AND (@Fecha IS NULL OR v.Fecha_Visita = @Fecha)
    ORDER BY v.Fecha_Visita DESC, v.Hora_Visita DESC
END
GO
