using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mercado.nu.Models.Entities;

namespace mercado.nu.Models.Evaluations
{
    public class SpanQuestion
    {
        internal object GetResults(List<Answer> answersForEvaluation)
        {
            var average = answersForEvaluation.Select(x => int.Parse(x.Value)).Average();

            var groups = answersForEvaluation.GroupBy(x => x.Value).ToList();

            var groupList = new List<Group>();

            foreach (var group in groups)
            {
                var numbersInGroup = group.Count();
                groupList.Add(new Group { Key = Convert.ToInt32(group.Key), Count = numbersInGroup});
            }

            groupList.Sort();
            return 1;
        }
    }
}
