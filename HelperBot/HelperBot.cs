//HelperBot - Copyright (c) Jonty800 and LeChosenOne <2013> (http://forums.au70.net)
//This plugin is open source and designed to be used with 800Craft and LegendCraft server softwares


using System;
using System.Collections.Generic;
using System.ComponentModel;
using GemsCraft.Configuration;
using GemsCraft.fSystem;
using GemsCraft.Plugins;
using Version = GemsCraft.Utils.Version;

namespace HelperBot
{
    public enum Gender
    {
        Male, Female
    }

    /// <summary>
    /// This is the plugin's initilization class
    /// The initilization of the bot should happen inside ServerStarted event in Events.cs
    /// </summary>
    public class Init : IPlugin
    {
        public bool Enabled { get; set; }
        internal static Init Instance { get; set; }

        public void Initialize()
        {
            Logger.Log(LogType.ConsoleOutput, "Starting HelperBot " + Version + ". Waiting for Init.");
            Server.Started += Events.ServerStarted;
            Instance = this;
        }

        public void Save()
        {
            Instance = this;
        }

        public IPlugin Load()
        {
            return Instance;
        }
        #region ColorParsing

        public static bool IsValidColorCode(char code)
        {
            return (code >= '0' && code <= '9') || (code >= 'a' && code <= 'f') || (code >= 'A' && code <= 'F');
        }

        public static readonly SortedList<char, string> ColorNames = new SortedList<char, string>{
            { '0', "black" },
            { '1', "navy" },
            { '2', "green" },
            { '3', "teal" },
            { '4', "maroon" },
            { '5', "purple" },
            { '6', "olive" },
            { '7', "silver" },
            { '8', "gray" },
            { '9', "blue" },
            { 'a', "lime" },
            { 'b', "aqua" },
            { 'c', "red" },
            { 'd', "magenta" },
            { 'e', "yellow" },
            { 'f', "white" }
        };

        //parses the colorcode
        public static string Parse(char code)
        {
            code = Char.ToLower(code);
            if (IsValidColorCode(code))
            {
                return "&" + code;
            }
            else
            {
                return null;
            }
        }

        //parses the colorname
        public static string Parse(string color)
        {
            if (color == null)
            {
                return null;
            }
            color = color.ToLower();
            switch (color.Length)
            {
                case 2:
                    if (color[0] == '&' && IsValidColorCode(color[1]))
                    {
                        return color;
                    }
                    break;

                case 1:
                    return Parse(color[0]);

                case 0:
                    return "";
            }
            if (ColorNames.ContainsValue(color))
            {
                return "&" + ColorNames.Keys[ColorNames.IndexOfValue(color)];
            }
            else
            {
                return null;
            }
        }

        //color code to name
        public static string GetName(char code)
        {
            code = Char.ToLower(code);
            if (IsValidColorCode(code))
            {
                return ColorNames[code];
            }
            string color = Parse(code);
            if (color == null)
            {
                return null;
            }
            return ColorNames[color[1]];
        }

        //name to color code
        public static string GetName(string color)
        {
            if (color == null)
            {
                return null;
            }
            else if (color.Length == 0)
            {
                return "";
            }
            else
            {
                string parsedColor = Parse(color);
                if (parsedColor == null)
                {
                    return null;
                }
                else
                {
                    return GetName(parsedColor[1]);
                }
            }
        }

        #endregion ColorParsing

        [Category("Info")]
        public string Name { get; set; } = "HelperBot";
        [Category("Info")]
        public string Version { get; set; } = "2.0";
        [Category("Info")]
        public string Author { get; set; } = "apotter96, forked from HelperBot by Jonty800 and LeChosenOne";
        [Category("Info")]
        public DateTime ReleaseDate { get; set; } = DateTime.Parse("03/13/2019");
        [Category("Info")]
        public string FileName { get; set; } = "HelperBot.dll";
        [Category("Info")]
        public Version SoftwareVersion { get; set; } = new Version("Alpha", 0, 3, -1, -1, true);

        // Helper Bot Specific Configs
        [Description("Bot will respond to players who are equiring about /fly or fly clients"),
         Category("Bot Announcements")]
        public bool AnnounceFly { get; set; } = true;

        [Description("Bot will respond when asked about the server name."),
         Category("Bot Announcements")]
        public bool AnnounceServer { get; set; } = true;

        [Description("Bot will display the player's hours when asked."),
         Category("Bot Announcements")]
        public bool AnnounceHours { get; set; } = true;

        [Description("Bot will display the requirements for a player to get promoted when asked."),
         Category("Bot Announcements")]
        public bool AnnounceRank { get; set; } = true;

        [Description("Bot will warn players who log in from a kick about the server rules."),
         Category("Bot Announcements")]
        public bool AnnounceWarnKick { get; set; } = true;

        [Description("Bot will warn players for swearing in PMs."),
         Category("Bot Announcements")]
        public bool AnnounceWarnSwear { get; set; } = true;

        [Description("Bot will suggest a ban if a player is meetting certain criteria."),
         Category("Bot Announcements")]
        public bool AnnounceSuggestBan { get; set; } = true;

        [Description("Bot will kick if another player impersonates them."),
         Category("Bot Announcements")]
        public bool AnnounceImpersonation { get; set; } = true;

        [Description("Bot will give the time of day when provoked."),
         Category("Bot Announcements")]
        public bool AnnounceTime { get; set; } = true;

        [Description("Bot will give instructions on how to make a private message when asked."),
         Category("Bot Announcements")]
        public bool AnnouncePrivateMessage { get; set; } = true;

        [Description(
             "Bot will assist in helping players who are stuck or who have fallen by explaining how to return to the spawn point."),
         Category("Bot Announcements")]
        public bool AnnounceFell { get; set; } = true;

        [Description("Bot will start a spleef timer when a player types !Spleef."),
         Category("Bot Announcements")]
        public bool AnnounceSpleefTimer { get; set; } = true;

        [Description("Bot will greet a player if it is their first time joining the server."),
         Category("Bot Announcements")]
        public bool AnnounceGreeting { get; set; } = true;

        [Description("Bot will explain what to do when demoted."),
         Category("Bot Announcements")]
        public bool AnnounceDemoted { get; set; } = true;

        [Description("Bot will display random jokes/facts when provoked"),
         Category("Bot Announcements")]
        public bool AnnounceJokes { get; set; } = true;

        [Description("Bot will warn players that spam caps on the server."),
         Category("Bot Announcements")]
        public bool AnnounceCaps { get; set; } = true;

        [Description("This will be the name the bot uses during server chat."),
         Category("Info")]
        public string BotName { get; set; } = "WackyBot";

        [Description("This will be the website url your bot will display when asked 'What is the server website?'"),
         Category("Info")]
        public string Website { get; set; } = ConfigKey.WebsiteURL.GetString();

        [Description("This will be the color of your bot when it talks in the server chat."),
         Category("Misc")]
        public string BotColor { get; set; } = "&c";

        [Description("Bot will forward all chat to the IRC channel."),
         Category("Bot Announcements")]
        public bool AnnounceIrc { get; set; }

        [Description("The message shown when a player inquires about building ranks."),
         Category("Tutorials")]
        public string HowToGetRankedBuilderText { get; set; } =
            "&fTo get ranked, keep building then use /review and a member of staff will check your build.";

        [Description("The message shown when a player inquires about staff ranks."),
         Category("Tutorials")]
        public string HowToGetRankedStaffText { get; set; } =
            "&fTo get the next staff rank, try to moderate the server the best way possible.";

        [Description(""),
         Category("Tutorials")]
        public string StuckMessage { get; set; } =
            "&fIf you are stuck, press R to respawn.";

        [Category("Personality")]
        public int Age { get; set; } = 21;

        [Category("Personality")]
        public string Hometown { get; set; } = "London, England";

        [Category("Personality")]
        public Gender Gender { get; set; } = Gender.Male;

        [Category("Personality")]
        public string Occupation { get; set; } = "Replacing your job";
    }
}