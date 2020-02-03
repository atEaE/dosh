using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;
using System.Resources;
using Dosh.Properties;

namespace Dosh.Core.Message
{
    /// <summary>
    /// message management class
    /// </summary>
    public static class MessageManager
    {

        public static string GetMessage()
        {
            return Resources.RUNTIME_00001;
        }
    }
}
