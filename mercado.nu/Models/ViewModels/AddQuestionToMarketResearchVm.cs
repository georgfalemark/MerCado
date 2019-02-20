using mercado.nu.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mercado.nu.Models.ViewModels
{
    public class AddQuestionToMarketResearchVm
    {
        public Question Question { get; set; }
        public Guid CurrentMarketResearchId { get; set; }
        public List<SelectListItem> GradeChoices { get; set; }
        public List<SelectListItem> Chapters { get; set; }
        public List<QuestionTypes> QuestionTypes { get; set; }
        [Display(Name = "Välj maxvärde")]
        public int HighGrade { get; set; }
        public int TypeChoice { get; set; }
        [Display(Name = "Binära alternativ")]
        public List<SelectListItem> BinaryChoice { get; set; }
    }
}
