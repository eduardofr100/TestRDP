# TestRDP
Este protecto es la incorporación de dos soluciones de Visual Studio.
Los cuales son un WebApi y un WPF anbso creados en net.core 8, los cuales se encargan del manejo de personas y sus respectivas facturas
que estan referenciadas a las personas, utilizando la EnitytFramework para el manejo dela informacion de la base de datos.

## Estructura y Script de la Base de Datos
Este proyecto contiene una base de datos para gestionar personas y sus facturas asociadas.
Esta base de datos fue creada en SQL Server Management Studio 20

CREATE DATABASE TestRDP

CREATE TABLE Persona (
    idPersona INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(50),
    apellidoPaterno VARCHAR(50),
    apellidoMaterno VARCHAR(50),
    identificacion VARCHAR(20)
);

CREATE TABLE Factura (
    idFactura INT PRIMARY KEY IDENTITY(1,1),
    fecha DATE,
    monto DECIMAL(10, 2),
    personaId INT,
    FOREIGN KEY (personaId) REFERENCES Persona(idPersona)
);

INSERT INTO Persona (nombre, apellidoPaterno, apellidoMaterno, identificacion) VALUES
('Juan', 'Pérez', 'García', 'ID12345'),
('María', 'López', 'Martínez', 'ID23456'),
('Carlos', 'Sánchez', 'Rodríguez', 'ID34567'),
('Ana', 'González', 'Hernández', 'ID45678'),
('Luis', 'Ramírez', 'Díaz', 'ID56789'),
('Lucía', 'Torres', 'Jiménez', 'ID67890'),
('Pedro', 'Flores', 'Morales', 'ID78901'),
('Laura', 'Ortiz', 'Castillo', 'ID89012'),
('Jorge', 'Ruiz', 'Reyes', 'ID90123'),
('Sofía', 'Cruz', 'Vargas', 'ID01234');

INSERT INTO Factura (fecha, monto, personaId) VALUES
('2025-01-10', 150.75, 1),
('2025-02-15', 300.50, 2),
('2025-03-20', 450.00, 3),
('2025-04-05', 120.00, 1),
('2025-04-15', 210.35, 4),
('2025-05-01', 340.20, 5),
('2025-05-03', 95.00, 2),
('2025-05-06', 560.80, 6),
('2025-05-07', 275.45, 3),
('2025-05-08', 135.60, 7);

Una vez creada base de datos con sus respectivas tableas y la insersion de datos ejecutar los 
siguientes ALTER a la tabla Factura para realizar una migracion de EnitytFramework y asi poder 
realizar la eliminacion de los registros

ALTER TABLE Factura
DROP CONSTRAINT FK__Factura__persona__398D8EEE;

ALTER TABLE Factura
ADD CONSTRAINT FK_Factura_Persona_Cascade
FOREIGN KEY (personaId) REFERENCES Persona(idPersona)
ON DELETE CASCADE;

### Adicionales

Comando para utilizar el Scaffolding en EnitytFramework

Scaffold-DbContext "Server=*name*;Database=*bd*;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer - OutputDir Models





