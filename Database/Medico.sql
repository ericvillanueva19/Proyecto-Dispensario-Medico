-- Database/Medico.sql
CREATE TABLE Medico (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(150) NOT NULL,
    Cedula VARCHAR(20) NOT NULL,
    Tanda_Labor VARCHAR(50) NOT NULL,
    Especialidad VARCHAR(100) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1
);
GO

-- PROCEDURE: Insertar
CREATE PROCEDURE sp_Medico_Insertar
    @Nombre VARCHAR(150),
    @Cedula VARCHAR(20),
    @Tanda_Labor VARCHAR(50),
    @Especialidad VARCHAR(100),
    @Estado BIT
AS
BEGIN
    INSERT INTO Medico (Nombre, Cedula, Tanda_Labor, Especialidad, Estado)
    VALUES (@Nombre, @Cedula, @Tanda_Labor, @Especialidad, @Estado)
END
GO

-- PROCEDURE: Actualizar
CREATE PROCEDURE sp_Medico_Actualizar
    @ID INT,
    @Nombre VARCHAR(150),
    @Cedula VARCHAR(20),
    @Tanda_Labor VARCHAR(50),
    @Especialidad VARCHAR(100),
    @Estado BIT
AS
BEGIN
    UPDATE Medico
    SET Nombre = @Nombre,
        Cedula = @Cedula,
        Tanda_Labor = @Tanda_Labor,
        Especialidad = @Especialidad,
        Estado = @Estado
    WHERE ID = @ID
END
GO

-- PROCEDURE: Inactivar (Soft delete)
CREATE PROCEDURE sp_Medico_Inactivar
    @ID INT
AS
BEGIN
    UPDATE Medico
    SET Estado = 0
    WHERE ID = @ID
END
GO

-- PROCEDURE: Listar
CREATE PROCEDURE sp_Medico_Listar
AS
BEGIN
    SELECT ID, Nombre, Cedula, Tanda_Labor, Especialidad, Estado
    FROM Medico
    ORDER BY Nombre ASC
END
GO

-- PROCEDURE: Verificar si existe (por Cedula)
CREATE PROCEDURE sp_Medico_ExisteCedula
    @Cedula VARCHAR(20),
    @ID INT = 0 
AS
BEGIN
    IF @ID = 0
        SELECT COUNT(*) FROM Medico WHERE Cedula = @Cedula
    ELSE
        SELECT COUNT(*) FROM Medico WHERE Cedula = @Cedula AND ID <> @ID
END
GO
