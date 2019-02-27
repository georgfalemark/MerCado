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
        public QuestionTypes QuestionType { get; set; }
        public int QuestionNumber { get; set; }
        public string TheQuestion { get; set; }


        internal void GetResults(List<Answer> answersForEvaluation)
        {
            var average = answersForEvaluation.Select(x => int.Parse(x.Value)).Average();

            var questions = answersForEvaluation.Select(x => new { x.Question.ActualQuestion, x.Question.QuestionNumber, x.Question.QuestionType }).Distinct().ToList();

            var valuesForTable = answersForEvaluation.Select(x => x.Question.QuestionOptions.Select(y => y.Value)).ToList();

            var itemList = new List<string>();

            foreach (var item in valuesForTable)
            {
                itemList = item.ToList();
            }

            var groups = answersForEvaluation.GroupBy(x => x.Value).ToList();

            var groupList = new List<Group>();

            foreach (var group in groups)
            {
                var numbersInGroup = group.Count();
                groupList.Add(new Group { Key = group.Key, Count = numbersInGroup});
            }

            var listKey = new List<string>();

            foreach (var item in groupList)
            {
                listKey.Add(item.Key);

            }

            foreach (var item in itemList)
            {
                bool present = listKey.Contains(item);
                if (!present)
                {
                    groupList.Add(new Group { Key = item, Count = 0 });
                }
            }

            var sortlist = groupList.OrderBy(x => x.Key).ToList();

            CountListSorted = sortlist;
            Average = average;
            TheQuestion = questions[0].ActualQuestion;
            QuestionNumber = questions[0].QuestionNumber;
            QuestionType = questions[0].QuestionType;
        }
    }
}
