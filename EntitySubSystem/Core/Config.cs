using CounterStrikeSharp.API.Core;
using System.Text.Json.Serialization;

namespace EntitySubSystemBase;

public class CoreConfig : BasePluginConfig
{
    [JsonPropertyName("debug_mode")] public bool DebugMode { get; set; } = true;
    [JsonPropertyName("chat_prefix")] public string ChatPrefix { get; set; } = "{GREEN}[EntitySubSystem]{DEFAULT}";


}