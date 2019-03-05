using System.Collections.Generic;
using System.Linq;
using mercado.nu.Models.Entities;

namespace mercado.nu.Models.Evaluations
{
    public class TextQuestion : BaseQuestion
    {
        public List<Group> TextList { get; set; }
        public QuestionTypes QuestionType { get; set; }
        public int QuestionNumber { get; set; }
        public string TheQuestion { get; set; }

        internal void GetResults(List<Answer> answersForEvaluation)
        {
            var textAnswers = answersForEvaluation.Select(x => x.Value).ToList();

            var questions = answersForEvaluation.Select(x => new { x.Question.ActualQuestion, x.Question.QuestionNumber, x.Question.QuestionType }).Distinct().ToList();

            var textList = new List<Group>();

            int i = 0;

            foreach (var text in textAnswers)
            {
                textList.Add(new Group { Key = i.ToString(), TextAnswer = text });
            }
            
            TextList = textList;
            TheQuestion = questions[0].ActualQuestion;
            QuestionNumber = questions[0].QuestionNumber;
            QuestionType = questions[0].QuestionType;
        }
    }
}
