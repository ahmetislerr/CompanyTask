namespace CompanyTask.Domain.Entities
{
    public class Statu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StatuEnumId { get; set; }

        //Navigation Properties
        public List<AppUser> AppUsers { get; set; }
        public List<Address> Addresses { get; set; }
        public List<Department> Departments { get; set; }
        public List<Title> Titles { get; set; }
        public List<Company> Companies { get; set; }
    }
}
