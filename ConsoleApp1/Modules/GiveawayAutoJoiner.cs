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

namespace ConsoleApp1.Modules
{
    internal class GiveawayAutoJoiner
    {
        static DiscordClient client = Program.clientX;
        static List<ulong> ids = new List<ulong>();
        static int giveawaysJoined = 0;

        public static string currentTime = DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss");

        public static void title()
        {
            Console.Clear();
            Program.writeTitle2(Program.clientX);
        }

        public static void main()
        {

            title();

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine($"[!] Module Chosen: Giveaway Auto Joiner");
            Thread.Sleep(1500);
            title();
            Directory.CreateDirectory(Environment.CurrentDirectory + @"\\Results\\DiscordGiveawayIDS");

            allServers();
        }
        public static void writeResults(string id)
        {
            string file = Environment.CurrentDirectory + @"\\Results\\DiscordGiveawayIDS\IDS " + currentTime + ".txt";
            File.AppendAllText(file, id + Environment.NewLine);
        }

        public static void allServers()
        {
            int num = 0;

            foreach (var guild in client.GetGuilds())
            {
                foreach(var channel in guild.GetChannels())
                {
                    if (channel.Name.Contains("nitro") || channel.Name.Contains("giveaway"))
                    {
                        try
                        {
                            foreach (var message in client.GetChannelMessages(channel.Id))
                            {
                                Console.Title = $"Discord AIO Tool | Server: {guild.Name} | Channel: {channel.Name} | MessageID: {message.Author.User.Id}";

                                //Console.WriteLine($"{message.Content}: {message.Embed}: Index: {num}", Console.ForegroundColor = ConsoleColor.White);
                                num += 1;
                                if (num == 30)
                                {
                                    num = 0;
                                    break;
                                }

                                if (message.Author.User.Id == 679379155590184966 || message.Author.User.Id == 294882584201003009 || message.Author.User.Id == 530082442967646230 || message.Author.User.Id == 717716451699589143)
                                {
                                    try
                                    {
                                        client.AddMessageReaction(channel.Id, message.Id, "🎉");
                                        Console.WriteLine($"[!] Sucessfully reacted to the message! Message ID: {message.Id}", Console.ForegroundColor = ConsoleColor.Yellow);

                                        ids.Add(message.Id);
                                        giveawaysJoined++;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"[-] Failed to react to message. {ex.Message}", Console.ForegroundColor = ConsoleColor.DarkRed);
                                    }
                                }
                            }
                        }
                        catch (Exception ex) { };
                    }
                }
            }
            Console.WriteLine($"[!] Finished to reacting to messages. Joined: {giveawaysJoined} giveaways. Do you want to save the message ids to a file? [1] YES [2] NO");
            Console.Write("> ");

            var key = Console.ReadKey().Key;

            if (key == ConsoleKey.D1)
            {
                foreach(ulong s in ids)
                {
                    writeResults(s.ToString());
                }
            }
            title();
            Console.WriteLine("[!] Going back to main menu in 2 seconds");
            Thread.Sleep(2000);
            Program.Main();
        }
    }
}
