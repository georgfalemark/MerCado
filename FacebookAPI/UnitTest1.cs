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


            //H�r snappar vi VEM det �r!




            var result = facebookService.GetInfoFromFaceBookUser("N�n j�kla personnyckel");//.Result;
        }
    }
}
