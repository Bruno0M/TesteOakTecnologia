namespace TesteOakTecnologia.Application.DTOs.UserDTOs
{
    public record UserResponse(
        string Name,
        string Email,
        string Token)
    {
        public UserResponse(string Name, string Email) : this(Name, Email, string.Empty) { }
    }
}
