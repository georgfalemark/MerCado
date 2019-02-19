using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mercado.nu.Models.Entities
{
    public class QuestionTypes
    {
        
        public Guid QuestionTypesId { get; set; }
        public string QuestionType { get; set; }

        public List<QuestionOption> QuestionOptions { get; set; }
    }

}
