-- Database/Marca.sql
CREATE TABLE Marca (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Descripcion VARCHAR(100) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1
);
GO

-- PROCEDURE: Insertar
CREATE PROCEDURE sp_Marca_Insertar
    @Descripcion VARCHAR(100),
    @Estado BIT
AS
BEGIN
    INSERT INTO Marca (Descripcion, Estado)
    VALUES (@Descripcion, @Estado)
END
GO

-- PROCEDURE: Actualizar
CREATE PROCEDURE sp_Marca_Actualizar
    @ID INT,
    @Descripcion VARCHAR(100),
    @Estado BIT
AS
BEGIN
    UPDATE Marca
    SET Descripcion = @Descripcion,
        Estado = @Estado
    WHERE ID = @ID
END
GO

-- PROCEDURE: Inactivar (Soft delete / Cambiar estado)
CREATE PROCEDURE sp_Marca_Inactivar
    @ID INT
AS
BEGIN
    UPDATE Marca
    SET Estado = 0
    WHERE ID = @ID
END
GO

-- PROCEDURE: Listar
CREATE PROCEDURE sp_Marca_Listar
AS
BEGIN
    SELECT ID, Descripcion, Estado
    FROM Marca
    ORDER BY Descripcion ASC
END
GO

-- PROCEDURE: Verificar si existe (para BLL)
CREATE PROCEDURE sp_Marca_Existe
    @Descripcion VARCHAR(100),
    @ID INT = 0 
AS
BEGIN
    IF @ID = 0
        SELECT COUNT(*) FROM Marca WHERE Descripcion = @Descripcion
    ELSE
        SELECT COUNT(*) FROM Marca WHERE Descripcion = @Descripcion AND ID <> @ID
END
GO
