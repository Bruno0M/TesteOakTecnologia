namespace TesteOakTecnologia.Domain.Entities
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public bool AvailableSale { get; set; } = true;
    }
}
