using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mercado.nu.Models.ViewModels
{
    public class ShowQuestionComparisonVm
    {
        public List<List<int>> DataArrayForComparison { get; set; }
        public string[] HeadingsVertical { get; set; }
        public string[] HeadingsHorizontal { get; set; }
    }
}
