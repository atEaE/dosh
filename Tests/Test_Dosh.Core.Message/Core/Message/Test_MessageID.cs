using Dosh.Core.Message;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

namespace Test_Dosh.Core.Message
{
    /// <summary>
    /// MessageID Unit Test
    /// </summary>
    [TestClass]
    public class Test_MessageID
    {
        /// <summary>
        /// Check the message in English.
        /// </summary>
        [TestMethod]
        public void CheckMessageInEnglish()
        {
            // setup
            var eng = new CultureInfo("en-US");
            CultureInfo.CurrentCulture = eng;
            CultureInfo.CurrentUICulture = eng;

            // expected
            var exp_runtime_00001 = "Launch the program.";
            var exp_runtime_00002 = "End the program.";

            // assert
            Assert.AreEqual(exp_runtime_00001, MessageManager.GetMessage(MessageID.RUNTIME_00001));
            Assert.AreEqual(exp_runtime_00002, MessageManager.GetMessage(MessageID.RUNTIME_00002));
        }

        /// <summary>
        /// Check the message in Japanese.
        /// </summary>
        [TestMethod]
        public void CheckMessageInJapanese()
        {
            // setup
            var jpn = new CultureInfo("ja-JP");
            CultureInfo.CurrentCulture = jpn;
            CultureInfo.CurrentUICulture = jpn;

            // expected
            var exp_runtime_00001 = "プログラムを起動します。";
            var exp_runtime_00002 = "プログラムを終了します。";

            // assert
            Assert.AreEqual(exp_runtime_00001, MessageManager.GetMessage(MessageID.RUNTIME_00001));
            Assert.AreEqual(exp_runtime_00002, MessageManager.GetMessage(MessageID.RUNTIME_00002));
        }
    }
}
