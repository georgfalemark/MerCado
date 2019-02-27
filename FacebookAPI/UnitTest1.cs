using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
