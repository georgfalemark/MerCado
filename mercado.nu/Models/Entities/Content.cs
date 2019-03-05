using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mercado.nu.Models.Entities
{
    public class Content
    {
        public Guid ContentId { get; set; }
        public string Headline { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }
        public bool Publish { get; set; }
    }
}
