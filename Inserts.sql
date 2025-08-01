USE ProyectoTareas; 
GO 


INSERT INTO Roles (rol) VALUES
('Administrador'),
('Usuario'),
('Invitado');


INSERT INTO Estado (estado) VALUES
('Activo'),
('Inactivo'),
('Suspendido');


INSERT INTO Usuario (nombre, email, password, rolid, id_estado) VALUES
('Juan P�rez', 'juan.perez@example.com', 'password123', 1, 1),
('Ana G�mez', 'ana.gomez@example.com', 'password123', 2, 1),
('Carlos Ruiz', 'carlos.ruiz@example.com', 'password123', 2, 1);


INSERT INTO EstadoTarea (estado_tarea) VALUES
('Pendiente'),
('En Proceso'),
('Completada'),
('Cancelada'),
('En Revisi�n');


INSERT INTO Prioridad (prioridad) VALUES
('Baja'),
('Media'),
('Alta');


INSERT INTO Tarea (id_usuario, descripcion, fecha_hora_solicitud, fecha_hora_update, creada_por, update_por, id_estado_tarea, id_prioridad) VALUES
(1, 'Actualizar documentaci�n del proyecto', GETDATE(), NULL, 2, NULL, 1, 2),
(2, 'Corregir errores de validaci�n en el frontend', GETDATE(), NULL, 1, 3, 2, 3),
(3, 'Agregar pruebas unitarias al m�dulo de pagos', GETDATE(), NULL, 1, NULL, 1, 3),  -- corregido a 3
(1, 'Migrar base de datos a nueva estructura', GETDATE(), NULL, 3, 2, 4, 3),
(2, 'Preparar presentaci�n para el cliente', GETDATE(), NULL, 1, NULL, 3, 1);

