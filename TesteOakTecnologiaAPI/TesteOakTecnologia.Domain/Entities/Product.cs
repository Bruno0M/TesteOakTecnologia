using System.Text.Json.Serialization;

namespace TesteOakTecnologia.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public bool AvailableSale { get; set; } = true;
    }
}
