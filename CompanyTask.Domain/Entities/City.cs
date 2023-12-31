﻿namespace CompanyTask.Domain.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }

        //Navigation Properties
        public Country Country { get; set; }
        public List<District>? Districts { get; set; }
    }
}
