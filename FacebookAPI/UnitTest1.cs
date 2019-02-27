using mercado.nu.Models.Entities;
using mercado.nu.Models.Evaluations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace FacebookAPI
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Get_Info_From_Facebook_User()
        {
            FacebookService facebookService = new FacebookService();


            //Här snappar vi VEM det är!




            var result = facebookService.GetInfoFromFaceBookUser("Nån jäkla personnyckel");//.Result;
        }

        [TestMethod]
        public void Split_Multi_Value_Answers_To_List()
        {
            var multi = new MultipleChoiceQuestion();
            List<Answer> list = new List<Answer>();
            list.Add(new Answer { Value = "Godis,Glass,Kakor,Bullar" });
            list.Add(new Answer { Value = "Godis,Glass,Kakor,Bullar" });
            list.Add(new Answer { Value = "Godis,Glass,Kakor,Bullar" });
            list.Add(new Answer { Value = "Godis,Glass,Kakor,Bullar" });
            list.Add(new Answer { Value = "Godis,Glass,Kakor,Bullar" });
            list.Add(new Answer { Value = "Godis,Glass,Kakor,Bullar" });
            list.Add(new Answer { Value = "Godis,Glass,Kakor,Bullar" });

            var valueList = new List<string>();
            valueList.Add("Godis");
            valueList.Add("Glass");
            valueList.Add("Kakor");
            valueList.Add("Bullar");

            var result = multi.SplitWordsInAnswers(list);
            var expected = new List<string>();
            expected.Add("Godis");
            expected.Add("Glass");
            expected.Add("Kakor");
            expected.Add("Bullar");
            expected.Add("Godis");
            expected.Add("Glass");
            expected.Add("Kakor");
            expected.Add("Bullar");
            expected.Add("Godis");
            expected.Add("Glass");
            expected.Add("Kakor");
            expected.Add("Bullar");
            expected.Add("Godis");
            expected.Add("Glass");
            expected.Add("Kakor");
            expected.Add("Bullar");
            expected.Add("Godis");
            expected.Add("Glass");
            expected.Add("Kakor");
            expected.Add("Bullar");
            expected.Add("Godis");
            expected.Add("Glass");
            expected.Add("Kakor");
            expected.Add("Bullar");
            expected.Add("Godis");
            expected.Add("Glass");
            expected.Add("Kakor");
            expected.Add("Bullar");

            Assert.AreEqual(expected.Count, result.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], result[i]);
            }
        }

        [TestMethod]
        public void Split_Multi_Value_Answers_To_List_2()
        {
            var multi = new MultipleChoiceQuestion();
            List<Answer> list = new List<Answer>();
            list.Add(new Answer { Value = "Godis,Glass" });
            list.Add(new Answer { Value = "Godis,Bullar" });
            list.Add(new Answer { Value = "Bullar" });
            list.Add(new Answer { Value = "Glass,Kakor" });
            list.Add(new Answer { Value = "Godis,Glass,Kakor,Bullar" });
            list.Add(new Answer { Value = "" });
            list.Add(new Answer { Value = "Kakor" });

            var valueList = new List<string>();
            valueList.Add("Godis");
            valueList.Add("Glass");
            valueList.Add("Kakor");
            valueList.Add("Bullar");

            var result = multi.SplitWordsInAnswers(list);
            var expected = new List<string>();
            expected.Add("Godis");
            expected.Add("Glass");
            expected.Add("Godis");
            expected.Add("Bullar");
            expected.Add("Bullar");
            expected.Add("Glass");
            expected.Add("Kakor");
            expected.Add("Godis");
            expected.Add("Glass");
            expected.Add("Kakor");
            expected.Add("Bullar");
            expected.Add("Kakor");

            Assert.AreEqual(expected.Count, result.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], result[i]);
            }
        }

        [TestMethod]
        public void Split_Multi_Value_Answers_To_List_3()
        {
            var multi = new MultipleChoiceQuestion();
            List<Answer> list = new List<Answer>();
            list.Add(new Answer { Value = "" });
            list.Add(new Answer { Value = "" });
            list.Add(new Answer { Value = "" });
            list.Add(new Answer { Value = "" });
            list.Add(new Answer { Value = "" });
            list.Add(new Answer { Value = "" });
            list.Add(new Answer { Value = "" });

            var valueList = new List<string>();
            valueList.Add("Godis");
            valueList.Add("Glass");
            valueList.Add("Kakor");
            valueList.Add("Bullar");

            var result = multi.SplitWordsInAnswers(list);
            var expected = new List<string>();
            

            Assert.AreEqual(expected.Count, result.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], result[i]);
            }
        }

        [TestMethod]
        public void Split_Multi_Value_Answers_To_List_4()
        {
            var multi = new MultipleChoiceQuestion();
            List<Answer> list = new List<Answer>();
            list.Add(new Answer { Value = "Bullar"});
            list.Add(new Answer { });

            var valueList = new List<string>();
            valueList.Add("Godis");
            valueList.Add("Glass");
            valueList.Add("Kakor");
            valueList.Add("Bullar");

            var result = multi.SplitWordsInAnswers(list);
            var expected = new List<string>();
            expected.Add("Bullar");
            

            Assert.AreEqual(expected.Count, result.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], result[i]);
            }
        }
    }
}
