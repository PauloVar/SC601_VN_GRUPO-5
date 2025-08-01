USE [ProyectoTareas];
GO

-- PRIORIDADES
INSERT INTO Prioridad (prioridad) VALUES 
('Alta'),
('Media'),
('Baja');

-- ESTADOS TAREA
INSERT INTO EstadoTarea (estado_tarea) VALUES 
('Pendiente'),
('En progreso'),
('Completada');

-- ESTADOS USUARIO
INSERT INTO Estado (estado) VALUES 
('Activo'),
('Inactivo');

-- ROLES
INSERT INTO Roles (rol) VALUES 
('Administrador'),
('Empleado');

-- USUARIOS
INSERT INTO Usuario (nombre, email, password, rolid, id_estado)
VALUES 
('Pablo', 'pablo@gmail.com', '1234', 1, 1),
('Juan', 'juan@gmail.com', '1234', 2, 1);


SELECT * FROM Prioridad;
SELECT * FROM Roles;
SELECT * FROM Estado;
SELECT * FROM Usuario;