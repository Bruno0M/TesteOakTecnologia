namespace TesteOakTecnologia.Application.DTOs.ProductDTOs
{
    public record ProductResponse(
        int Id,
        int UserId,
        string Name,
        string Description,
        decimal Amount,
        bool AvailableSale);
}