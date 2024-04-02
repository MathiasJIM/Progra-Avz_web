CREATE PROCEDURE [ObtenerUsuario]
    @CorreoElectronico NVARCHAR(255)
AS
BEGIN
    SELECT *
    FROM Usuarios
    WHERE CorreoElectronico = @CorreoElectronico;
END
