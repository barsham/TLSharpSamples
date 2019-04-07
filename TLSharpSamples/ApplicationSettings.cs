
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace TLSharpSamples
{
    class ApplicationSettings
    {
        public static int TelegramApiId => GetSettings<int>("Telegram.ApiKey");
        public static string TelegramApiKey => GetSettings<string>("Telegram.ApiValue");

        private static T GetSettings<T>(string keyName) where T : IConvertible
        {

            var result = ConfigurationManager.AppSettings[keyName];

            return (T)Convert.ChangeType(result, typeof(T));

        }
    }
}
