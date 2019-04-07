using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLSharp.Core;

namespace TLSharpSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            
            LoginToTelegram();
            GetAllUserGroups();
        }

        private static void GetAllUserGroups()
        {
            throw new NotImplementedException();
        }

        private static void LoginToTelegram()
        {

            var client = new TelegramClient(ApplicationSettings.TelegramApiId,
                ApplicationSettings.TelegramApiKey);

            var connected = client.ConnectAsync();

        }
    }
}
