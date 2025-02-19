CREATE PROCEDURE GetClientesPaginados
    @PageNumber INT,
    @PageSize INT
AS
BEGIN
    SELECT c.Id, c.Nombre, c.Telefono, p.Nombre AS PaisNombre
    FROM Clientes c
    INNER JOIN Paises p ON c.PaisId = p.Id
    ORDER BY c.Id
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END