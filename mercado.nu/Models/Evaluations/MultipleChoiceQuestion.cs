using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mercado.nu.Data;
using mercado.nu.Models.Entities;

namespace mercado.nu.Models.Evaluations
{
    public class MultipleChoiceQuestion : BaseQuestion
    {
        public List<Group> CountListSorted { get; set; }
        public QuestionTypes QuestionType { get; set; }
        public int QuestionNumber { get; set; }
        public string TheQuestion { get; set; }

        public void GetResults(List<Answer> answersForEvaluation, List<string> valueTypes)
        {

            var listOfAnswers = SplitWordsInAnswers(answersForEvaluation);

            var groupList = SortAndCountGroups(listOfAnswers, valueTypes);

            var questions = answersForEvaluation.Select(x => new { x.Question.ActualQuestion, x.Question.QuestionNumber, x.Question.QuestionType }).Distinct().ToList();

            var sortList = groupList.OrderBy(x => x.Key).ToList();

            CountListSorted = sortList;
            TheQuestion = questions[0].ActualQuestion;
            QuestionNumber = questions[0].QuestionNumber;
            QuestionType = questions[0].QuestionType;
        }

        public List<string> SplitWordsInAnswers(List<Answer> answersForEvaluation)
        {
            List<string> listOfAnswers = new List<string>();

            foreach (var answer in answersForEvaluation)
            {
                string stringValue = answer.Value;
                if (!string.IsNullOrEmpty(stringValue))
                {
                    var value = answer.Value.Split(',');

                    foreach (var newValue in value)
                    {
                        listOfAnswers.Add(newValue);
                    }
                }
                
            }

            return listOfAnswers;
        }

        public List<Group> SortAndCountGroups(List<string> listOfAnswers, List<string> valueTypes)
        {
            var groupList = new List<Group>();

            valueTypes.RemoveAt(valueTypes.Count - 1);

            foreach (var value in valueTypes)
            {
                var count = listOfAnswers.Where(x => x == value).Count();
                groupList.Add(new Group { Key = value, Count = count });
            }

            return groupList;
        }
    }
}
