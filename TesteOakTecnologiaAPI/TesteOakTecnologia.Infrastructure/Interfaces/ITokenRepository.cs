using TesteOakTecnologia.Domain.Entities;

namespace TesteOakTecnologia.Infrastructure.Interfaces
{
    public interface ITokenRepository
    {
        public string GenerateToken(User user);
    }
}
