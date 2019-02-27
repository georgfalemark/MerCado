using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mercado.nu.Models.Entities;

namespace mercado.nu.Models.Evaluations
{
    public class BinaryQuestion : BaseQuestion
    {
        public List<Group> CountListSorted { get; set; }
        public QuestionTypes QuestionType { get; set; }
        public int QuestionNumber { get; set; }
        public string TheQuestion { get; set; }

        internal void GetResults(List<Answer> answersForEvaluation)
        {
            var groups = answersForEvaluation.GroupBy(x => x.Value).ToList();

            var valuesForTable = answersForEvaluation.Select(x => x.Question.QuestionOptions.Select(y => y.Value)).ToList();

            string[] valueString = { };

            foreach (var value in valuesForTable)
            {
                var newlist = value.ToList();
                valueString = newlist[0].Split('-');
                break;
            }

            var questions = answersForEvaluation.Select(x => new { x.Question.ActualQuestion, x.Question.QuestionNumber, x.Question.QuestionType }).Distinct().ToList();

            var groupList = new List<Group>();


            foreach (var group in groups)
            {
                var numbersInGroup = group.Count();
                groupList.Add(new Group { Key = group.Key, Count = numbersInGroup });
            }

            var sortList = groupList.OrderBy(x => x.Key).ToList();

            CountListSorted = sortList;
            TheQuestion = questions[0].ActualQuestion;
            QuestionNumber = questions[0].QuestionNumber;
            QuestionType = questions[0].QuestionType;
        }
    }
}
