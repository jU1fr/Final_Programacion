USE Parcial_Final;


CREATE DATABASE Parcial_Final;
GO

USE Parcial_Final;
GO


CREATE TABLE Usuarios (
    UsuarioID INT PRIMARY KEY IDENTITY(1,1),
    Usuario VARCHAR(50) NOT NULL,
    Nombre VARCHAR(100) NOT NULL
);


CREATE TABLE Alumnos (
    Carnet VARCHAR(20) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Telefono VARCHAR(20),
    Grado VARCHAR(50),
    UsuarioID INT,
    FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID)
);


INSERT INTO Usuarios (Usuario, Nombre) VALUES ('jdoe', 'John Doe');
INSERT INTO Usuarios (Usuario, Nombre) VALUES ('asmith', 'Anna Smith');


ALTER TABLE Alumnos
ADD Estado BIT NOT NULL DEFAULT 1; -- Por defecto, los alumnos están activos


USE Parcial_Final;
CREATE TABLE RegistrosEliminados (
    ID INT PRIMARY KEY,
    TipoRegistro NVARCHAR(100),
    IDRegistro INT,
    FechaEliminacion DATETIME,
    -- Otras columnas según sea necesario
);
