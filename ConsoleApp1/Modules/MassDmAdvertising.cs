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
    internal class MassDmAdvertising
    {
        static DiscordClient client = Program.clientX;
        public static void title()
        {
            Console.Clear();
            Program.writeTitle2(Program.clientX);
        }

        public static void main()
        {

        }
    }
}
