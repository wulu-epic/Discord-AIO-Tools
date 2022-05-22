using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Discord;
using Discord.Gateway;
using System.Threading;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.Storage.Streams;

namespace ConsoleApp1.Modules
{

    public class NewToastNotification
    {
        public NewToastNotification(string input, int type)
        {
            string NotificationTextThing = input;
            string Toast = "";
            switch (type)
            {
                case 1:
                    {
                        //Basic Toast
                        Toast = "<toast><visual><binding template=\"ToastImageAndText01\"><text id = \"1\" >";
                        Toast += NotificationTextThing;
                        Toast += "</text></binding></visual></toast>";
                        break;
                    }
                default:
                    {
                        Toast = "<toast><visual><binding template=\"ToastImageAndText01\"><text id = \"1\" >";
                        Toast += "You have redeemed a nitro code. Go check your account!";
                        Toast += "</text></binding></visual></toast>";
                        break;
                    }
            }
            XmlDocument tileXml = new XmlDocument();
            tileXml.LoadXml(Toast);
            var toast = new ToastNotification(tileXml);
            ToastNotificationManager.CreateToastNotifier("Discord Nitro Sniper").Show(toast);
        }
    }
        internal class NitroSniper
        {
        static DiscordClient client = Program.clientX;

        public static string currentTime = DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss");

        public static List<string> results = new List<string>();
        public static List<string> badresults = new List<string>();

        public static void title()
        {
            Console.Clear();
            Program.writeTitle2(Program.clientX);
        }

        

        public static void main()
        {
            title();

            DiscordSocketClient clientx = new DiscordSocketClient();
            
            
            clientx.Login(client.Token);
            clientx.OnMessageReceived += Client_OnMessageReceived;

            


            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Thread th = new Thread(() =>
            {
                while (true)
                {
                    title(); 
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine("[!] Listening to messages. You will recieve a notification if you get a valid nitro code.");
                    foreach(string s in results)
                    {
                        Console.WriteLine(s, Console.ForegroundColor = ConsoleColor.Yellow);
                    }
                    foreach (string s in badresults)
                    {
                        Console.WriteLine(s, Console.ForegroundColor = ConsoleColor.Red);
                    }
                    Thread.Sleep(10000);
                }
            });
            th.Start();
            Console.ReadLine();

        }
        private static void Client_OnMessageReceived(DiscordSocketClient client, MessageEventArgs args)
        {
            const string giftPrefix = "discord.gift/";

            string message = args.Message.Content;
            string code = message.Substring(message.LastIndexOf('/') + 1);

            if (message.Contains(giftPrefix))
            {
                try
                {
                    client.RedeemGift(code);

                    Console.WriteLine($"[!!] Sucessfully redeemed code: {message}", Console.ForegroundColor = ConsoleColor.Yellow);
                    results.Add($"[!!] Sucessfully redeemed code: { message}");

                    NewToastNotification Window = new NewToastNotification("LETS GOOO", 72000);

                }
                catch (Exception) { }

                if(code.Length < 24 || code.Length > 24)
                {
                    Console.WriteLine($"{message} was a fake code.", Console.ForegroundColor = ConsoleColor.Red);
                    badresults.Add($"{message} was a fake code.");
                }
            }
        }

        
    }
}
