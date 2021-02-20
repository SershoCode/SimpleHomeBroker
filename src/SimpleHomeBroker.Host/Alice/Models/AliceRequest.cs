using System.Collections.Generic;
using Newtonsoft.Json;

namespace SimpleHomeBroker.Host.Alice.Models
{
    public class AliceRequest
    {
        [JsonProperty("meta")] public Meta Meta { get; set; }

        [JsonProperty("request")] public Request Request { get; set; }

        [JsonProperty("session")] public Session Session { get; set; }

        [JsonProperty("version")] public string Version { get; set; }
    }

    public class Interfaces
    {
        [JsonProperty("screen")] public Screen Screen { get; set; }

        [JsonProperty("account_linking")] public AccountLinking AccountLinking { get; set; }
    }

    public class Meta
    {
        [JsonProperty("locale")] public string Locale { get; set; }

        [JsonProperty("timezone")] public string Timezone { get; set; }

        [JsonProperty("client_id")] public string ClientId { get; set; }

        [JsonProperty("interfaces")] public Interfaces Interfaces { get; set; }
    }

    public class Markup
    {
        [JsonProperty("dangerous_context")] public bool DangerousContext { get; set; }
    }

    public class Tokens
    {
        [JsonProperty("start")] public int Start { get; set; }

        [JsonProperty("end")] public int End { get; set; }
    }

    public class Entity
    {
        [JsonProperty("tokens")] public Tokens Tokens { get; set; }

        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("value")] public object Value { get; set; }
    }

    public class Nlu
    {
        [JsonProperty("tokens")] public List<string> Tokens { get; set; }

        [JsonProperty("entities")] public List<Entity> Entities { get; set; }
    }

    public class Request
    {
        [JsonProperty("command")] public string Command { get; set; }

        [JsonProperty("original_utterance")] public string OriginalUtterance { get; set; }

        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("markup")] public Markup Markup { get; set; }

        [JsonProperty("payload")] public Payload Payload { get; set; }

        [JsonProperty("nlu")] public Nlu Nlu { get; set; }
    }

    public class User
    {
        [JsonProperty("user_id")] public string UserId { get; set; }

        [JsonProperty("access_token")] public string AccessToken { get; set; }
    }

    public class Application
    {
        [JsonProperty("application_id")] public string ApplicationId { get; set; }
    }

    public class Session
    {
        [JsonProperty("message_id")] public int MessageId { get; set; }

        [JsonProperty("session_id")] public string SessionId { get; set; }

        [JsonProperty("skill_id")] public string SkillId { get; set; }

        [JsonProperty("user_id")] public string UserId { get; set; }

        [JsonProperty("user")] public User User { get; set; }

        [JsonProperty("application")] public Application Application { get; set; }

        [JsonProperty("new")] public bool New { get; set; }
    }

    public class Screen
    {
    }

    public class AccountLinking
    {
    }

    public class Payload
    {
    }
}