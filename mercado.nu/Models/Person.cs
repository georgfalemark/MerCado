using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mercado.nu.Models
{
    public class Person
    {
        public Guid PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string City { get; set; }
        public string PostNumber { get; set; }
        public string Country { get; set; }

        public Guid OrganizationÍd { get; set; }
        public Organization Organization { get; set; }

        public List<Responders> Responders { get; set; }

        public AspNetUsers MyProperty { get; set; }

    }
}