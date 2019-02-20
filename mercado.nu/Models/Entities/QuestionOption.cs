using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mercado.nu.Models.Entities
{
    public class QuestionOption
    {
        public Guid QuestionOptionId { get; set; }
        public string QuestionOptionHeading { get; set; }
        public string Value { get; set; }

        public Guid QuestionTypesId { get; set; }
        public QuestionTypes QuestionType { get; set; }

        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
    }

}
