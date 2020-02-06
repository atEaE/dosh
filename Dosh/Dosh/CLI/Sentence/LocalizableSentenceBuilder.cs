using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Dosh.CLI.Localize
{
    /// <summary>
    /// Localize sentence builder.
    /// </summary>
    public class LocalizableSentenceBuilder : SentenceBuilder
    {
        /// <summary>
        /// Required sentence
        /// </summary>
        public override Func<string> RequiredWord => () => Properties.Resources.CLI_00005;

        public override Func<string> OptionGroupWord => () => Properties.Resources.CLI_00006;

        public override Func<string> ErrorsHeadingText => () => Properties.Resources.CLI_00003;

        public override Func<string> UsageHeadingText => () => Properties.Resources.CLI_00004;

        public override Func<bool, string> HelpCommandText => (bool _) => Properties.Resources.CLI_00001;

        public override Func<bool, string> VersionCommandText => (bool _) => Properties.Resources.CLI_00002;

        public override Func<Error, string> FormatError => (Error error) => Properties.Resources.CLI_00007;

        public override Func<IEnumerable<MutuallyExclusiveSetError>, string> FormatMutuallyExclusiveSetErrors => (IEnumerable<MutuallyExclusiveSetError> _) => Properties.Resources.CLI_00008;
    }
}
