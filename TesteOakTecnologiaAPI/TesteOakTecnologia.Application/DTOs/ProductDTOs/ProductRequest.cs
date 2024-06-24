namespace TesteOakTecnologia.Application.DTOs.ProductDTOs
{
    public record ProductRequest(
        int UserId,
        string Name,
        string Description,
        decimal Amount,
        bool AvailableSale);
}
