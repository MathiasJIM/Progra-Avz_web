CREATE PROCEDURE AgregarUsuario
    @NombreUsuario NVARCHAR(255),
    @PasswordHash NVARCHAR(255),
    @CorreoElectronico NVARCHAR(255),
    @IDRol INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @UsuarioId UNIQUEIDENTIFIER;
    SET @UsuarioId = NEWID();

    INSERT INTO Usuarios (ID, NombreCompleto, CorreoElectronico, IDRol, IDEstadoCuenta, FechaRegistro, ContrasenaHash)
    VALUES (@UsuarioId, @NombreUsuario, @CorreoElectronico, @IDRol, 1, GETDATE(),@PasswordHash);

    SELECT @UsuarioId;
END
