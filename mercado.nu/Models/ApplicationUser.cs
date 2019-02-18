using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mercado.nu.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public Guid PersonId { get; set; }
        public Person Person { get; set; }
    }
}
