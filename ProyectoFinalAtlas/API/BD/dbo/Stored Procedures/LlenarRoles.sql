CREATE PROCEDURE LlenarRoles
AS
BEGIN
    INSERT INTO Roles (ID, NombreRol)
    VALUES (1, 'Administrador'),
           (2, 'Profesor'),
           (3, 'Estudiante')
END
