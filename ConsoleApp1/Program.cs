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


namespace ConsoleApp1
{
    internal class Program
    {
        public static string token = "";
        public static DiscordClient clientX = null;
        static dynamic jsonconfig = null;

        public static void writeTitle()
        {

            Console.WriteLine(@"
                                                   █     █░█    ██  ██▓     █    ██ 
                                                  ▓█░ █ ░█░██  ▓██▒▓██▒     ██  ▓██▒
                                                  ▒█░ █ ░█▓██  ▒██░▒██░    ▓██  ▒██░
                                                  ░█░ █ ░█▓▓█  ░██░▒██░    ▓▓█  ░██░
                                                  ░░██▒██▓▒▒█████▓ ░██████▒▒▒█████▓ 
                                                  ░ ▓░▒ ▒ ░▒▓▒ ▒ ▒ ░ ▒░▓  ░░▒▓▒ ▒ ▒ 
                                                    ▒ ░ ░ ░░▒░ ░ ░ ░ ░ ▒  ░░░▒░ ░ ░ 
                                                    ░   ░  ░░░ ░ ░   ░ ░    ░░░ ░ ░ 
                                                      ░      ░         ░  ░   ░     
                                         
            ", Console.ForegroundColor = ConsoleColor.DarkRed);
        }

        public static void writeTitle2(DiscordClient client)
        {

            Console.WriteLine($@"
                                                   █     █░█    ██  ██▓     █    ██ 
                                                  ▓█░ █ ░█░██  ▓██▒▓██▒     ██  ▓██▒
                                                  ▒█░ █ ░█▓██  ▒██░▒██░    ▓██  ▒██░
                                                  ░█░ █ ░█▓▓█  ░██░▒██░    ▓▓█  ░██░
                                                  ░░██▒██▓▒▒█████▓ ░██████▒▒▒█████▓ 
                                                  ░ ▓░▒ ▒ ░▒▓▒ ▒ ▒ ░ ▒░▓  ░░▒▓▒ ▒ ▒ 
                                                    ▒ ░ ░ ░░▒░ ░ ░ ░ ░ ▒  ░░░▒░ ░ ░ 
                                                    ░   ░  ░░░ ░ ░   ░ ░    ░░░ ░ ░ 
                                                      ░      ░         ░  ░   ░     
                                                            {client.ToString()}
                                         
            ", Console.ForegroundColor = ConsoleColor.DarkRed);
        }

        public static void Main()
        {
            Console.Clear();
            Console.Title = "Discord AIO Tools";
            writeTitle();
            

            if (!File.Exists("config.json"))
            {
                Console.Clear();
                writeTitle();

                Console.WriteLine("[#] Couldn't find config.json, so making a new config file. Please wait");
                config config = new config();
                {
                    config.token = "";
                }

                Thread.Sleep(1000);

                Console.Clear();
                writeTitle();

                dynamic json = JsonConvert.SerializeObject(config, Formatting.Indented);
                File.WriteAllText("config.json", json);
                Thread.Sleep(350);
                Console.WriteLine("[!] Finished writing. You can now use the program.");
                Thread.Sleep(350);
                Console.Clear();
                writeTitle();
            }
            else
            {
                try
                {
                    jsonconfig = JsonConvert.DeserializeObject(File.ReadAllText("config.json"));
                    token = jsonconfig["token"];
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }

            string url = "https://canary.discord.com/api/v9/users/@me";
            if (clientX == null)
            {
                Console.Title = "Trying to login...";
                if (token != "")
                {
                    try
                    {
                        DiscordClient client = new DiscordClient(token);
                        Console.WriteLine($"[!] Logged in as {client.ToString()}", Console.ForegroundColor = ConsoleColor.Yellow);

                        Console.Title = $"Successfully logged into {client.ToString()}";
                        Thread.Sleep(1500);
                        Console.Clear();

                        writeTitle2(client);
                        Console.Title = "Discord AIO Tools";
                        clientX = client;
                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        writeTitle();

                        Console.WriteLine("[~] Failed to login, do you want to try to edit your token? [1] YES [2] NO");
                        var key = Console.ReadKey();
                        if (key.Key == ConsoleKey.D1)
                        {
                            Console.Clear();
                            writeTitle();

                            Console.WriteLine($"[!] Enter your new token");
                            Console.Write("> ");

                            //TODO save it to the actual JSON file.
                            string Ntoken = Console.ReadLine();
                            token = Ntoken;

                            try
                            {
                                jsonconfig.token = token;

                                string output = JsonConvert.SerializeObject(jsonconfig, Formatting.Indented);
                                File.WriteAllText("config.json", output);
                            } catch (Exception esx) { Console.WriteLine(esx.Message); }


                            Console.Clear();
                            writeTitle();

                            Console.WriteLine($"[!] Successfully set the new token value, restarting...", Console.ForegroundColor = ConsoleColor.Green);
                            Thread.Sleep(1500);
                            Main();
                            return;
                        }
                        else
                        {
                            Console.Clear();
                            writeTitle();

                            Console.WriteLine("[~] Uhh okay. Exiting in 3 seconds.");
                            Thread.Sleep(3000);
                            Environment.Exit(0);
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"[!] Set a new token");
                    Console.Write("> ");
                    token = Console.ReadLine();

                    try
                    {
                        jsonconfig = JsonConvert.DeserializeObject(File.ReadAllText("config.json"));
                        jsonconfig.token = token;
                        string output = JsonConvert.SerializeObject(jsonconfig, Formatting.Indented);
                        File.WriteAllText("config.json", output);
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }

                    Console.Clear();
                    writeTitle();

                    //TODO SET THIS AS THE JSON VALUE!

                    Console.WriteLine($"[!] Successfully set the new token value, restarting...", Console.ForegroundColor = ConsoleColor.Green);
                    Thread.Sleep(1000);
                    Main();
                    return;
                }
            }

            Console.WriteLine($@"
                           [1] Mass Server Advertising
                           [2] Giveaway Auto Joiner
                           [3] Nitro Sniper
                           [4] Mass DM Advertising
                           [5] Spam DMS
             ", Console.ForegroundColor = ConsoleColor.DarkBlue);

            Console.Write("> ");
            var keyX = Console.ReadKey();
            Console.Clear();
            writeTitle2(clientX);
            switch (keyX.Key)
            {
                case ConsoleKey.D1:
                    Modules.MassServerAdv.main();
                    break;
                case ConsoleKey.D2:
                    Modules.GiveawayAutoJoiner.main();
                    break;
                case ConsoleKey.D3:
                    Modules.NitroSniper.main();
                    break;
                case ConsoleKey.D4:

                default:
                    Console.WriteLine("[-] That isnt a valid module. Try again.");
                    Thread.Sleep(700);
                    Main();
                    break;

            }

            Console.ReadLine();
        }
    }
}
