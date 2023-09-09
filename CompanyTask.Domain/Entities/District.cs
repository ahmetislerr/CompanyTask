namespace CompanyTask.Domain.Entities
{
    public class District
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? CityId { get; set; }

        // Navigation Properties
        public List<Address>? Addresses { get; set; }
        public City? City { get; set; }
    }
}
