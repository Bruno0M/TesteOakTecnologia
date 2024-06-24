namespace TesteOakTecnologia.Application.DTOs.UserDTOs
{
    public record UserResponse(
        int UserId,
        string Name,
        string Email,
        string Token)
    {
        public UserResponse(int UserId, string Name, string Email) : this(UserId, Name, Email, string.Empty) { }
    }
}
