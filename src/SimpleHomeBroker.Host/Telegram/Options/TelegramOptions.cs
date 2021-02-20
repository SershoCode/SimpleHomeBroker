using System;
using Telegram.Bot.Types.ReplyMarkups;

namespace SimpleHomeBroker.Host.Telegram.Options
{
    public class TelegramOptions
    {
        public string Token { get; set; }
        public string WebHookUrl { get; set; }
        public int Owner { get; set; }
        public int[] Family { get; set; }
        public string[][] Keyboard { get; set; }

        public KeyboardButton[][] KeyboardArray => Array.ConvertAll(Keyboard,
            array => Array.ConvertAll(array, item => new KeyboardButton(item)));
    }
}