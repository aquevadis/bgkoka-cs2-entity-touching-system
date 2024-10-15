using CounterStrikeSharp.API.Modules.Utils;

namespace EntitySubSystemBase;
public partial class EntitySubSystemBase {

    public enum PrintTo
    {
        Chat = 1,
        ChatAll,
        ConsoleError,
        ConsoleSucess,
        ConsoleInfo
    }

    public void SendMessageToInternalConsole(string msg = "",
        PrintTo print = PrintTo.Chat)
    {
        switch (print)
        {
            case PrintTo.ConsoleError:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[EntitySubSystem] {msg}");
                Console.ResetColor();
                return;
            case PrintTo.ConsoleSucess:
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[EntitySubSystem] {msg}");
                Console.ResetColor();
                return;
            case PrintTo.ConsoleInfo:
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"[EntitySubSystem] {msg}");
                Console.ResetColor();
                return;
        }
    }

    public string ReplaceColorTags(string input)
    {
        string[] colorPatterns =
        {
            "{DEFAULT}", "{WHITE}", "{GREEN}", "{LIGHTYELLOW}", "{LIGHTBLUE}", "{OLIVE}", "{LIME}",
            "{RED}", "{LIGHTPURPLE}", "{PURPLE}", "{GREY}", "{YELLOW}", "{GOLD}", "{SILVER}", "{BLUE}", "{DARKBLUE}",
            "{BLUEGREY}", "{MAGENTA}", "{LIGHTRED}", "{ORANGE}"
        };

        string[] colorReplacements =
        {
            $"{ChatColors.Default}", $"{ChatColors.White}", $"{ChatColors.Green}",
            $"{ChatColors.LightYellow}", $"{ChatColors.LightBlue}", $"{ChatColors.Olive}", $"{ChatColors.Lime}",
            $"{ChatColors.Red}", $"{ChatColors.LightPurple}", $"{ChatColors.Purple}", $"{ChatColors.Grey}",
            $"{ChatColors.Yellow}", $"{ChatColors.Gold}", $"{ChatColors.Silver}", $"{ChatColors.Blue}",
            $"{ChatColors.DarkBlue}", $"{ChatColors.BlueGrey}", $"{ChatColors.Magenta}", $"{ChatColors.LightRed}",
            $"{ChatColors.Orange}"
        };

        for (var i = 0; i < colorPatterns.Length; i++)
            input = input.Replace(colorPatterns[i], colorReplacements[i]);

        return input;
    }
    
}