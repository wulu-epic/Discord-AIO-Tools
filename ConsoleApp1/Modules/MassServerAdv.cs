using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Leaf.xNet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Discord.Commands;
using Discord.Net;
using Discord.Gateway;
using Discord;
using System.Threading;
using System.Timers;

namespace ConsoleApp1.Modules
{
    internal class MassServerAdv
    {
        static DiscordClient client = Program.clientX;
        public static List<ulong> channelIDS = new List<ulong>();

        public static string message;
        public static int minutes = 10;

        public static void title()
        {
            Console.Clear();
            Program.writeTitle2(Program.clientX);
        }

        public static void main()
        {         
            title();

            Console.ForegroundColor = ConsoleColor.DarkBlue;

            Console.WriteLine("How long between resending all these messages? (in minutes)");
            Console.Write("> ");
            minutes = Convert.ToInt32(Console.ReadLine());
            title();

            Console.WriteLine("Enter the message you want to send.");
            Console.Write("> ");
            message = Console.ReadLine();

            title();

            Console.WriteLine("[!] Select the method to get channel IDS.");
            Console.WriteLine("[1] Type channel IDS manually");
            Console.WriteLine("[2] Select random channels automatically (this will select channels such as #general) ");
            Console.Write("> ");

            var key = Console.ReadKey().Key;

            if (key == ConsoleKey.D2)
            {
                title();
                Automatically();
            }
            if (key == ConsoleKey.D1)
            {
                title();
                Manually();
            }
        }

        public static void Automatically()
        {
            int num = 0;
            
            foreach (var guild in client.GetGuilds())
            {
                
                foreach (var channels in client.GetGuildChannels(guild.Id))
                {
                    if (channels.Name.Contains("general") || channels.Name.Contains("lounge") || channels.Name.Contains("chat") || channels.Name.Contains("lobby"))
                    {
                        if (channels.Name.Contains("staff") || channels.Name.Contains("vc") || channels.Name.Contains("admin") || channels.Name.Contains("mod") || channels.Name.Contains("voice") || channels.Name.Contains("owner")) 
                        { 

                        }
                        
                        else
                        {
                            num++;

                            channelIDS.Add(channels.Id);
                            Console.WriteLine($"[!] Added {channels.Name} {channels.Id} to list from the server {guild.Name}", Console.ForegroundColor = ConsoleColor.Yellow);
                            if (num > 300)
                            {
                                break;
                            }
                        }       
                    }
                }
            }
            Console.WriteLine($"[!] Successfully scraped {num} channels. Will begin the message sending process in 3 seconds. ", Console.ForegroundColor = ConsoleColor.Yellow);

            Thread.Sleep(3000);
            title();

            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMinutes(minutes);

            var timer = new System.Threading.Timer((e) =>
            {
                SendMessages();
            }, null, startTimeSpan, periodTimeSpan);

            Console.ReadLine();
        }

        public static void Manually()
        {
            bool x = true;
            while (x)
            {
                Console.WriteLine("Enter a channel ID (type exit to stop entering IDs)", Console.ForegroundColor = ConsoleColor.Green);
                Console.Write("> ");

                string ID = Console.ReadLine();
                ulong id = 0;
                try
                {
                     id = Convert.ToUInt64(ID);
                } 
                catch(Exception ex) 
                {
                    Console.WriteLine("[-] This is not the valid discord id format.", Console.ForegroundColor = ConsoleColor.Red);
                    title();
                }

                title();

                if (ID == "exit")
                {
                    break;
                }
                else
                {
                    channelIDS.Add(id);
                }
            }
            Console.WriteLine($"[!] Added {channelIDS.Count} IDS. Beginning the message sending process in 3 seconds.", Console.ForegroundColor = ConsoleColor.Yellow);
            Thread.Sleep(3000);
            title();

            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMinutes(minutes);

            var timer = new System.Threading.Timer((e) =>
            {
                SendMessages();
            }, null, startTimeSpan, periodTimeSpan);

            Console.ReadLine();
        }

        public static void SendMessages()
        {
            title();
            Console.WriteLine($"[!] Starting to send messages! {channelIDS.Count}");
            try
            {
                foreach (ulong m in channelIDS.ToList())
                {
                    try
                    {
                        
                        client.SendMessage(m, message);
                        Console.WriteLine($"Successfully sent the message {message} in {m}", Console.ForegroundColor = ConsoleColor.Yellow);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[-] Failed to send message {ex.Message}", Console.ForegroundColor = ConsoleColor.Red);
                        channelIDS.Remove(m);
                    }
                }
            } catch(Exception ex) { Console.WriteLine(ex.Message); }

            Console.WriteLine("[!] Finished sending messages.");
        }
    }
}
