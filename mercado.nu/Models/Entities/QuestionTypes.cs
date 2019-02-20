using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mercado.nu.Models.Entities
{
    public enum QuestionTypes
    {
        Graderingsfråga,
        Flervalsfråga,
        JaNejfråga,
        Textfråga,
    }

}
