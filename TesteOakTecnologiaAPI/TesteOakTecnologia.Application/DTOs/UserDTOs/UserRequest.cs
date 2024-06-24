namespace TesteOakTecnologia.Application.DTOs.UserDTOs
{
    public record UserRequest(
        string Name,
        string Email,
        string Password,
        string ConfirmPassword);
}
