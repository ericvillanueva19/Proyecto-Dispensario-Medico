-- Database/TipoFarmaco.sql
CREATE TABLE TipoFarmaco (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Descripcion VARCHAR(100) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1
);
GO

-- PROCEDURE: Insertar
CREATE PROCEDURE sp_TipoFarmaco_Insertar
    @Descripcion VARCHAR(100),
    @Estado BIT
AS
BEGIN
    INSERT INTO TipoFarmaco (Descripcion, Estado)
    VALUES (@Descripcion, @Estado)
END
GO

-- PROCEDURE: Actualizar
CREATE PROCEDURE sp_TipoFarmaco_Actualizar
    @ID INT,
    @Descripcion VARCHAR(100),
    @Estado BIT
AS
BEGIN
    UPDATE TipoFarmaco
    SET Descripcion = @Descripcion,
        Estado = @Estado
    WHERE ID = @ID
END
GO

-- PROCEDURE: Inactivar (Soft delete / Cambiar estado)
CREATE PROCEDURE sp_TipoFarmaco_Inactivar
    @ID INT
AS
BEGIN
    UPDATE TipoFarmaco
    SET Estado = 0
    WHERE ID = @ID
END
GO

-- PROCEDURE: Listar
CREATE PROCEDURE sp_TipoFarmaco_Listar
AS
BEGIN
    SELECT ID, Descripcion, Estado
    FROM TipoFarmaco
    ORDER BY Descripcion ASC
END
GO

-- PROCEDURE: Verificar si existe (para BLL)
CREATE PROCEDURE sp_TipoFarmaco_Existe
    @Descripcion VARCHAR(100),
    @ID INT = 0 -- 0 si es insercion, ID si es modificacion
AS
BEGIN
    IF @ID = 0
        SELECT COUNT(*) FROM TipoFarmaco WHERE Descripcion = @Descripcion
    ELSE
        SELECT COUNT(*) FROM TipoFarmaco WHERE Descripcion = @Descripcion AND ID <> @ID
END
GO
