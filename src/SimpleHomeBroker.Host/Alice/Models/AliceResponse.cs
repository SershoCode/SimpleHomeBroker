using System.Collections.Generic;
using Newtonsoft.Json;

namespace SimpleHomeBroker.Host.Alice.Models
{
    public class AliceResponse
    {
        [JsonProperty("response")] public Response Response { get; set; }

        [JsonProperty("version")] public string Version { get; set; }

        public AliceResponse(string text, string tts = default, bool endSession = true, List<Button> buttons = default,
            string version = "1.0")
        {
            Response = new Response(text, tts, buttons, endSession);
            Version = version;
        }
    }

    public class Response
    {
        [JsonProperty("text")] public string Text { get; set; }

        [JsonProperty("tts")] public string Tts { get; set; }

        [JsonProperty("buttons")] public List<Button> Buttons { get; set; }

        [JsonProperty("end_session")] public bool EndSession { get; set; }

        public Response(string text, string tts, List<Button> buttons, bool endSession)
        {
            Text = text;
            Tts = tts;
            Buttons = buttons;
            EndSession = endSession;
        }
    }

    public class Button
    {
        [JsonProperty("title")] public string Title { get; set; }

        [JsonProperty("payload")] public ResponsePayload Payload { get; set; }

        [JsonProperty("url")] public string Url { get; set; }

        [JsonProperty("hide")] public bool Hide { get; set; }
    }

    public class ResponsePayload
    {
    }
}