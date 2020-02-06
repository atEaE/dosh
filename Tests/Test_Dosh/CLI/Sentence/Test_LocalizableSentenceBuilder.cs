using Dosh.CLI.Localize;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

namespace Test_Dosh.CLI.Sentence
{
    [TestClass]
    public class Test_LocalizableSentenceBuilder
    {
        [TestMethod]
        public void CheckVersionCommandText_English()
        {
            // setup
            var builder = new LocalizableSentenceBuilder();

            // english
            var eng = new CultureInfo("en-US");
            Dosh.Properties.Resources.Culture = eng;
            var exp_eng = "Display version information.";

            // assert
            Assert.AreEqual(exp_eng, builder.VersionCommandText(true));
        }

        [TestMethod]
        public void CheckVersionCommandText_Japanese()
        {
            // setup
            var builder = new LocalizableSentenceBuilder();

            // japanese
            var jpn = new CultureInfo("ja-JP");
            Dosh.Properties.Resources.Culture = jpn;
            var exp_jpn = "Version情報を表示します。";

            // assert
            Assert.AreEqual(exp_jpn, builder.VersionCommandText(true));
        }

        [TestMethod]
        public void CheckHelpCommandText_English()
        {
            // setup
            var builder = new LocalizableSentenceBuilder();

            // english
            var eng = new CultureInfo("en-US");
            Dosh.Properties.Resources.Culture = eng;
            var exp_eng = "Display this help screen.";

            // assert
            Assert.AreEqual(exp_eng, builder.HelpCommandText(true));
        }

        [TestMethod]
        public void CheckHelpCommandText_Japanese()
        {
            var cu = CultureInfo.CurrentCulture;
            // setup
            var builder = new LocalizableSentenceBuilder();

            // japanese
            var jpn = new CultureInfo("ja-JP");
            Dosh.Properties.Resources.Culture = jpn;
            var exp_jpn = "Help情報を表示します。";

            // assert
            Assert.AreEqual(exp_jpn, builder.HelpCommandText(true));
        }
    }
}
