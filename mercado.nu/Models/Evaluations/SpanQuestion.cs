using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mercado.nu.Models.Entities;

namespace mercado.nu.Models.Evaluations
{
    public class SpanQuestion : BaseQuestion
    {
        public List<Group> CountListSorted { get; set; }
        public double Average { get; set; }

        internal void GetResults(List<Answer> answersForEvaluation)
        {
            var average = answersForEvaluation.Select(x => int.Parse(x.Value)).Average();

            var groups = answersForEvaluation.GroupBy(x => x.Value).ToList();

            var groupList = new List<Group>();

            foreach (var group in groups)
            {
                var numbersInGroup = group.Count();
                groupList.Add(new Group { Key = group.Key, Count = numbersInGroup});
            }

            var sortlist = groupList.OrderBy(x => x.Key).ToList();

            CountListSorted = sortlist;
            Average = average;  
        }
    }
}
