using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mercado.nu.Models.ViewModels
{
    public class ChoseQuestionsVm
    {
        public List<SelectListItem> QuestionList { get; set; }
    }
}
