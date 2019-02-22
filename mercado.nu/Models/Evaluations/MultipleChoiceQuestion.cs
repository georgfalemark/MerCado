using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mercado.nu.Models.Entities;

namespace mercado.nu.Models.Evaluations
{
    public class MultipleChoiceQuestion : BaseQuestion
    {
        internal List<Group> GetResults(List<Answer> answersForEvaluation)
        {
            var groups = answersForEvaluation.GroupBy(x => x.Value).ToList();

            var groupList = new List<Group>();

            foreach (var group in groups)
            {
                var numbersInGroup = group.Count();
                groupList.Add(new Group { Key = group.Key, Count = numbersInGroup });
            }

            var sortList = groupList.OrderBy(x => x.Key).ToList();
            return sortList;
        }
    }
}
