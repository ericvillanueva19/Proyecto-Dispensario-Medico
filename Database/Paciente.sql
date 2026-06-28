-- Database/Paciente.sql
CREATE TABLE Paciente (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(150) NOT NULL,
    Cedula VARCHAR(20) NOT NULL,
    No_Carnet VARCHAR(50) NOT NULL,
    TipoPaciente VARCHAR(50) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1
);
GO

-- PROCEDURE: Insertar
CREATE PROCEDURE sp_Paciente_Insertar
    @Nombre VARCHAR(150),
    @Cedula VARCHAR(20),
    @No_Carnet VARCHAR(50),
    @TipoPaciente VARCHAR(50),
    @Estado BIT
AS
BEGIN
    INSERT INTO Paciente (Nombre, Cedula, No_Carnet, TipoPaciente, Estado)
    VALUES (@Nombre, @Cedula, @No_Carnet, @TipoPaciente, @Estado)
END
GO

-- PROCEDURE: Actualizar
CREATE PROCEDURE sp_Paciente_Actualizar
    @ID INT,
    @Nombre VARCHAR(150),
    @Cedula VARCHAR(20),
    @No_Carnet VARCHAR(50),
    @TipoPaciente VARCHAR(50),
    @Estado BIT
AS
BEGIN
    UPDATE Paciente
    SET Nombre = @Nombre,
        Cedula = @Cedula,
        No_Carnet = @No_Carnet,
        TipoPaciente = @TipoPaciente,
        Estado = @Estado
    WHERE ID = @ID
END
GO

-- PROCEDURE: Inactivar (Soft delete)
CREATE PROCEDURE sp_Paciente_Inactivar
    @ID INT
AS
BEGIN
    UPDATE Paciente
    SET Estado = 0
    WHERE ID = @ID
END
GO

-- PROCEDURE: Listar
CREATE PROCEDURE sp_Paciente_Listar
AS
BEGIN
    SELECT ID, Nombre, Cedula, No_Carnet, TipoPaciente, Estado
    FROM Paciente
    ORDER BY Nombre ASC
END
GO

-- PROCEDURE: Verificar si existe (por Cedula)
CREATE PROCEDURE sp_Paciente_ExisteCedula
    @Cedula VARCHAR(20),
    @ID INT = 0 
AS
BEGIN
    IF @ID = 0
        SELECT COUNT(*) FROM Paciente WHERE Cedula = @Cedula
    ELSE
        SELECT COUNT(*) FROM Paciente WHERE Cedula = @Cedula AND ID <> @ID
END
GO
