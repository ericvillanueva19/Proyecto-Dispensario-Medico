-- Database/Medicamento.sql
CREATE TABLE Medicamento (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Descripcion VARCHAR(150) NOT NULL,
    TipoFarmacoID INT NOT NULL,
    MarcaID INT NOT NULL,
    UbicacionID INT NOT NULL,
    Dosis VARCHAR(100) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Medicamento_TipoFarmaco FOREIGN KEY (TipoFarmacoID) REFERENCES TipoFarmaco(ID),
    CONSTRAINT FK_Medicamento_Marca FOREIGN KEY (MarcaID) REFERENCES Marca(ID),
    CONSTRAINT FK_Medicamento_Ubicacion FOREIGN KEY (UbicacionID) REFERENCES Ubicacion(ID)
);
GO

-- PROCEDURE: Insertar
CREATE PROCEDURE sp_Medicamento_Insertar
    @Descripcion VARCHAR(150),
    @TipoFarmacoID INT,
    @MarcaID INT,
    @UbicacionID INT,
    @Dosis VARCHAR(100),
    @Estado BIT
AS
BEGIN
    INSERT INTO Medicamento (Descripcion, TipoFarmacoID, MarcaID, UbicacionID, Dosis, Estado)
    VALUES (@Descripcion, @TipoFarmacoID, @MarcaID, @UbicacionID, @Dosis, @Estado)
END
GO

-- PROCEDURE: Actualizar
CREATE PROCEDURE sp_Medicamento_Actualizar
    @ID INT,
    @Descripcion VARCHAR(150),
    @TipoFarmacoID INT,
    @MarcaID INT,
    @UbicacionID INT,
    @Dosis VARCHAR(100),
    @Estado BIT
AS
BEGIN
    UPDATE Medicamento
    SET Descripcion = @Descripcion,
        TipoFarmacoID = @TipoFarmacoID,
        MarcaID = @MarcaID,
        UbicacionID = @UbicacionID,
        Dosis = @Dosis,
        Estado = @Estado
    WHERE ID = @ID
END
GO

-- PROCEDURE: Inactivar (Soft delete)
CREATE PROCEDURE sp_Medicamento_Inactivar
    @ID INT
AS
BEGIN
    UPDATE Medicamento
    SET Estado = 0
    WHERE ID = @ID
END
GO

-- PROCEDURE: Listar (con INNER JOINs para nombres legibles)
CREATE PROCEDURE sp_Medicamento_Listar
AS
BEGIN
    SELECT 
        m.ID, 
        m.Descripcion, 
        m.TipoFarmacoID,
        tf.Descripcion AS TipoFarmaco,
        m.MarcaID,
        ma.Descripcion AS Marca,
        m.UbicacionID,
        u.Descripcion AS Ubicacion,
        m.Dosis, 
        m.Estado
    FROM Medicamento m
    INNER JOIN TipoFarmaco tf ON m.TipoFarmacoID = tf.ID
    INNER JOIN Marca ma ON m.MarcaID = ma.ID
    INNER JOIN Ubicacion u ON m.UbicacionID = u.ID
    ORDER BY m.Descripcion ASC
END
GO

-- PROCEDURE: Verificar si existe
CREATE PROCEDURE sp_Medicamento_Existe
    @Descripcion VARCHAR(150),
    @ID INT = 0 
AS
BEGIN
    IF @ID = 0
        SELECT COUNT(*) FROM Medicamento WHERE Descripcion = @Descripcion
    ELSE
        SELECT COUNT(*) FROM Medicamento WHERE Descripcion = @Descripcion AND ID <> @ID
END
GO
