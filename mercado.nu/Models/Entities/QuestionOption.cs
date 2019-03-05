using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mercado.nu.Models.Entities
{
    public class QuestionOption
    {
        public Guid QuestionOptionId { get; set; }
        [Display(Name = "Valrubrik")]
        public string QuestionOptionHeading { get; set; }
        [Display(Name = "Svarsvärde")]
        public string Value { get; set; }

        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
    }

}
