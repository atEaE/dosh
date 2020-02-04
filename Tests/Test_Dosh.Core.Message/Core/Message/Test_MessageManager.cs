using Dosh.Core.Message;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

namespace Test_Dosh.Core.Message
{
    /// <summary>
    /// MessageManager Unit Test
    /// </summary>
    [TestClass]
    public class Test_MessageManager
    {
        /// <summary>
        /// language switching test
        /// </summary>
        [TestMethod]
        public void MultipleLanguageCheck()
        {
            // setup
            var eng = new CultureInfo("en-US");
            CultureInfo.CurrentCulture = eng;
            CultureInfo.CurrentUICulture = eng;

            // english check
            var exp_eng = "Launch the program.";
            Assert.AreEqual(exp_eng, MessageManager.GetMessage(MessageID.RUNTIME_00001));


            // setup
            var jpn = new CultureInfo("ja-JP");
            CultureInfo.CurrentCulture = jpn;
            CultureInfo.CurrentUICulture = jpn;

            // japanese check
            var exp_jpn = "プログラムを起動します。";
            Assert.AreEqual(exp_jpn, MessageManager.GetMessage(MessageID.RUNTIME_00001));
        }
    }
}
