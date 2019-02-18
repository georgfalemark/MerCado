using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mercado.nu.Models
{
    public class Organization
    {
        public Guid OrganizationId { get; set; }
        public string Name { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public Person Contactperson { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PathLogoImage { get; set; }

        public Guid PersonId { get; set; }
        public List<Person> Persons { get; set; }

        public Guid MarketResearchId { get; set; }
        public List<MarketResearch> MarketResearches { get; set; }




    }
}
