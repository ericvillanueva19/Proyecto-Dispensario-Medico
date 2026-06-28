-- Database/Ubicacion.sql
CREATE TABLE Ubicacion (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Descripcion VARCHAR(100) NOT NULL,
    Estante VARCHAR(50) NOT NULL,
    Tramo VARCHAR(50) NOT NULL,
    Celda VARCHAR(50) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1
);
GO

-- PROCEDURE: Insertar
CREATE PROCEDURE sp_Ubicacion_Insertar
    @Descripcion VARCHAR(100),
    @Estante VARCHAR(50),
    @Tramo VARCHAR(50),
    @Celda VARCHAR(50),
    @Estado BIT
AS
BEGIN
    INSERT INTO Ubicacion (Descripcion, Estante, Tramo, Celda, Estado)
    VALUES (@Descripcion, @Estante, @Tramo, @Celda, @Estado)
END
GO

-- PROCEDURE: Actualizar
CREATE PROCEDURE sp_Ubicacion_Actualizar
    @ID INT,
    @Descripcion VARCHAR(100),
    @Estante VARCHAR(50),
    @Tramo VARCHAR(50),
    @Celda VARCHAR(50),
    @Estado BIT
AS
BEGIN
    UPDATE Ubicacion
    SET Descripcion = @Descripcion,
        Estante = @Estante,
        Tramo = @Tramo,
        Celda = @Celda,
        Estado = @Estado
    WHERE ID = @ID
END
GO

-- PROCEDURE: Inactivar (Soft delete / Cambiar estado)
CREATE PROCEDURE sp_Ubicacion_Inactivar
    @ID INT
AS
BEGIN
    UPDATE Ubicacion
    SET Estado = 0
    WHERE ID = @ID
END
GO

-- PROCEDURE: Listar
CREATE PROCEDURE sp_Ubicacion_Listar
AS
BEGIN
    SELECT ID, Descripcion, Estante, Tramo, Celda, Estado
    FROM Ubicacion
    ORDER BY Descripcion ASC
END
GO

-- PROCEDURE: Verificar si existe (para BLL)
CREATE PROCEDURE sp_Ubicacion_Existe
    @Descripcion VARCHAR(100),
    @ID INT = 0 
AS
BEGIN
    IF @ID = 0
        SELECT COUNT(*) FROM Ubicacion WHERE Descripcion = @Descripcion
    ELSE
        SELECT COUNT(*) FROM Ubicacion WHERE Descripcion = @Descripcion AND ID <> @ID
END
GO
