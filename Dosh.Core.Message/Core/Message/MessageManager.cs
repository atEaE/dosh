using Dosh.Properties;
using System.Globalization;
using System.Linq;

namespace Dosh.Core.Message
{
    /// <summary>
    /// message management class
    /// </summary>
    public static class MessageManager
    {
        /// <summary>
        /// Get the target message from Resource.resx.
        /// </summary>
        /// <param name="id">message id.</param>
        /// <param name="args">formart argments.</param>
        /// <returns>message</returns>
        public static string GetMessage(MessageID id, params string[] args)
        {
            var message = Resources.ResourceManager.GetString(id.ToString(), CultureInfo.CurrentCulture);
            if (string.IsNullOrEmpty(message))
            {
                return string.Empty;
            }

            if (args != null && args.Any())
            {
                return string.Format(message, args);
            } 
            else
            {
                return message;
            }
        }
    }
}
