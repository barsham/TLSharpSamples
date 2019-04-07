using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
using TLSharp.Core;

namespace TLSharpSamples
{
    class Program
    {
        static async Task Main(string[] args)
        {

            //  Step 1
            var client = await LoginToTelegram();

            //  Step 2
            var user = await AuthorizeUser(client);

            //  Step 3
            await GetContacts(client);

            //  Step 4
            await GetChannels(client);

            //  Step 5
            await GetChannelsUsingOffset(client);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("All Done!");
            Console.ReadLine();

        }

        private static async Task GetChannels(TelegramClient client)
        {
            Console.WriteLine("4. Trying to get top 10 Channels ...");

            // get available contacts
            var result = await client.GetUserDialogsAsync() as TeleSharp.TL.Messages.TLDialogsSlice;

            //find recipient in contacts
            var list = result.Chats
                .Where(x => x.GetType() == typeof(TLChannel))
                .Cast<TLChannel>()
                .Take(10);

            foreach (var item in list)
            {
                Console.WriteLine($"                 {item.Title} ");
            }

            Console.WriteLine("4. Trying to get contacts  [Done]!");
            Console.WriteLine();

        }


        /// <summary>
        /// Not Valid Yet.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        private static async Task GetChannelsUsingOffset(TelegramClient client)
        {

            /// This is not working properly need fix.
            
            Console.WriteLine("5. Trying to get Channels using offset...");

            int offsetId = 0;

            var peer = new TeleSharp.TL.TLInputPeerEmpty();
            var iLoadCount = 4;

            while (true)
            {

                // get available contacts
                var result = await client.GetUserDialogsAsync(0, offsetId, peer, iLoadCount) as TeleSharp.TL.Messages.TLDialogsSlice;
                

                //find recipient in contacts
                var list = result.Chats
                    .Where(x => x.GetType() == typeof(TLChannel))
                    .Cast<TLChannel>();

                foreach (var item in list)
                {
                    Console.WriteLine($"Offset Id: {offsetId}                {item.Title} ");
                }

                if (list.Count() == 0)
                    break;

                offsetId += iLoadCount;

                System.Threading.Thread.Sleep(1000);

            }

            Console.WriteLine("5. Trying to get Channels using offset [Done]!");
            Console.WriteLine();

        }

        private static async Task<TLUser> AuthorizeUser(TelegramClient client)
        {

            TLUser user = null;

            Console.WriteLine("2. Trying to authotize the user ...");

            if (!client.IsUserAuthorized())
            {

                var hash = await client.SendCodeRequestAsync(ApplicationSettings.TelegramPhoneNumber);


                Console.Write("Please provide the code you received from Telegram: ");
                var code = Console.ReadLine();

                user = await client.MakeAuthAsync(ApplicationSettings.TelegramPhoneNumber, hash, code);

            }
            else
            {
                Console.WriteLine("User is already authorized!");
            }

            Console.WriteLine("2. Trying to authotize the user [Done]!");
            Console.WriteLine();

            return user;
        }

        private static async Task GetContacts(TelegramClient client)
        {
            Console.WriteLine("3. Trying to get top 10 contacts ...");

            // get available contacts
            var result = await client.GetContactsAsync();

            //find recipient in contacts
            var users = result.Users
                .Where(x => x.GetType() == typeof(TLUser))
                .Cast<TLUser>()
                .Take(10);

            //.FirstOrDefault(x => x.phone == "<recipient_phone>");

            //send message
            foreach (var item in users)
            {
                Console.WriteLine($"                 {item.FirstName} {item.LastName} {item.Phone}");
            }

            Console.WriteLine("3. Trying to get contacts  [Done]!");
            Console.WriteLine();
        }



        private static async Task<TelegramClient> LoginToTelegram()
        {

            Console.WriteLine("1. Trying to connect to Telegram...");

            var client = new TelegramClient(ApplicationSettings.TelegramApiId,
                ApplicationSettings.TelegramApiKey);

            await client.ConnectAsync();
            

            Console.WriteLine("1. Trying to connect to Telegram [Done]!");
            Console.WriteLine();

            return client;
        }
    }
}
