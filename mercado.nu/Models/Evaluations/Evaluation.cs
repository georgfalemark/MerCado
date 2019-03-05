using mercado.nu.Data;
using mercado.nu.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mercado.nu.Models.Evaluations
{
    public class Evaluation
    {
        internal List<BaseQuestion> GetEvaluation(List<Answer> answers, List<List<string>> listValueTypes)
        {
            var baseListWithAnswers = new List<BaseQuestion>();

            var listofQuestionIds = answers.Select(x => x.QuestionId).Distinct().ToList();

            foreach (var questionId in listofQuestionIds)
            {
                var questionType = answers.Where(x => x.QuestionId == questionId).Select(x => x.Question.QuestionType).ToList();

                switch (questionType[0])
                {
                    case QuestionTypes.Graderingsfråga:
                        {
                            SpanQuestion evaluateAnswers = new SpanQuestion();
                            var answersForEvaluation = answers.Where(x => x.QuestionId == questionId).ToList();
                            evaluateAnswers.GetResults(answersForEvaluation);
                            baseListWithAnswers.Add(evaluateAnswers);
                            break;
                        }
                    case QuestionTypes.Flervalsfråga:
                        {
                            MultipleChoiceQuestion evaluateAnswers = new MultipleChoiceQuestion();
                            List<string> valueTypes = new List<string>();

                            foreach (var item in listValueTypes)
                            {
                                if (item.Contains(questionId.ToString()))
                                {
                                    valueTypes = item;
                                    break;
                                }
                                
                            }

                            var answersForEvaluation = answers.Where(x => x.QuestionId == questionId).ToList();
                            evaluateAnswers.GetResults(answersForEvaluation, valueTypes);
                            baseListWithAnswers.Add(evaluateAnswers);
                            break;
                        }
                    case QuestionTypes.Binärfråga:
                        {
                            BinaryQuestion evaluateAnswers = new BinaryQuestion();
                            var answersForEvaluation = answers.Where(x => x.QuestionId == questionId).ToList();
                            evaluateAnswers.GetResults(answersForEvaluation);
                            baseListWithAnswers.Add(evaluateAnswers);
                            break;
                        }
                    case QuestionTypes.Textfråga:
                        {
                            TextQuestion evaluateAnswers = new TextQuestion();
                            var answersForEvaluation = answers.Where(x => x.QuestionId == questionId).ToList();
                            evaluateAnswers.GetResults(answersForEvaluation);
                            baseListWithAnswers.Add(evaluateAnswers);
                            break;
                        }
                    default:
                        break;
                }
            }
            return baseListWithAnswers;
        }
    }
}
