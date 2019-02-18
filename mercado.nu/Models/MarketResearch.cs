﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mercado.nu.Models
{
    public class MarketResearch
    {
        public Guid MarketResearchId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Purpose { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public int MyProperty { get; set; }
        public Gender Gender { get; set; }
        public string Area { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Guid OrganizationId { get; set; }
        public Person CreatorId { get; set; }

        public List<Responders> Responders { get; set; }





    }
}