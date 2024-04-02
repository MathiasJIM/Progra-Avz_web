CREATE PROCEDURE LlenarEstadoCuenta
AS
BEGIN
    INSERT INTO EstadoCuenta (ID, Estado)
    VALUES (1, 'Activo'),
           (2, 'Inactivo')
END
