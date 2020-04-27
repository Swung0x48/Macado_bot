namespace Macado_bot.Misc
{
    public static class Language
    {
        private static string lang = "en_US";
        private static string Msg_ReqApiKey = "Please enter your Telegram bot API key:";
        private static string Msg_ConfNotFound = "Configuration file not found. A new one will be created.";
        private static string Msg_FirstBoot = "It seems that this is the first time you launch this.";
        private static string Msg_ReadConf = "Reading Configuration...";
        private static string Msg_BotInit = "Bot Initializing...";

        public static string Lang { get => lang; set => lang = value; }
        public static string MsgReqApiKey { get => Msg_ReqApiKey; set => lang = value; }
        public static string MsgConfNotFound { get => Msg_ConfNotFound; set => lang = value; }
        public static string MsgFirstBoot { get => Msg_FirstBoot; set => lang = value; }
        public static string MsgReadConf { get => Msg_ReadConf; set => lang = value; }
        public static string MsgBotInit { get => Msg_BotInit; set => lang = value; }
    }
}