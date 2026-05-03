using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace YoshiSharp.Core;

public class YoshiEngine
{
    private readonly Dictionary<string, string> _dictionary = new()
    {
        { "mlem", "public" },
        { "melon", "int" },
        { "tongue", "string" },
        { "berry", "bool" },
        { "sparkle", "double" },
        { "gulp", "void" },
        { "Yoshi.Say", "Console.WriteLine" },
        { "Yoshi.Eat", "Console.ReadLine" },
        { "Yoshi.Paint", "ChangeYoshiColour" },
        { "hatch", "GetRandomEgg" },
        { "ha-pu", "if" },
        { "wah-hoo", "else" },
        { "stampede", "for" },
        { "flutter-jump", "while" }
    };

    public string Translate(string yoshiCode)
    {
        string translatedCode = yoshiCode;

        foreach (var entry in _dictionary)
        {
            // This pattern looks for the keyword ONLY if it is a whole word
            // and NOT followed or preceded by other letters/numbers
            string pattern = @"\b" + Regex.Escape(entry.Key) + @"\b(?=(?:[^""]*""[^""]*"")*[^""]*$)";

            translatedCode = Regex.Replace(translatedCode, pattern, entry.Value);
        }

        return translatedCode;
    }

    public async Task RunAsync(string yoshiCode)
    {
        string cSharpCode = Translate(yoshiCode);

        var options = ScriptOptions.Default
            .AddReferences(typeof(YoshiEngine).Assembly)
            .AddImports("System", "YoshiSharp.Core", "YoshiSharp.Core.YoshiEngine");
        await CSharpScript.RunAsync(cSharpCode, options);
    }

    public static void ChangeYoshiColour(string colour)
    {
        Console.ForegroundColor = colour.ToLower() switch
        {
            "red" => ConsoleColor.Red,
            "green" => ConsoleColor.Green,
            "blue" => ConsoleColor.Blue,
            "yellow" => ConsoleColor.Yellow,
            "pink" => ConsoleColor.Magenta,
            _ => ConsoleColor.White
        };
    }

    private static readonly Random _rng = new();

    public static int GetRandomEgg(int min, int max)
    {
        return _rng.Next(min, max);
    }
}