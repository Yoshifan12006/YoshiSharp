using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace YoshiSharp.Core;

public class YoshiEngine
{
    private readonly List<KeyValuePair<string, string>> _keywords = new()
    {
        new("mlem", "public"),
        new("melon", "int"),
        new("tongue", "string"),
        new("berry", "bool"),
        new("sparkle", "double"),
        new("gulp", "void"),
        new("Yoshi.Say", "Console.WriteLine"),
        new("Yoshi.Eat", "Console.ReadLine"),
        new("Yoshi.Paint", "ChangeYoshiColour"),
        new("ha-pu", "if"),
        new("wah-hoo", "else"),
        new("stampede", "for"),
        new("flutter-jump", "while"),
        new("ground-pound", "try"),
        new("catch-egg", "catch"),
        new("star-power", "finally"),
        new("egg", "class"),
        new(@"hatch(?=\s*\()", "YoshiEngine.GetRandomEgg"),
        new(@"hatch\b", "new")
    };

    public string Translate(string yoshiCode)
    {
        if (string.IsNullOrEmpty(yoshiCode)) return yoshiCode;

        string translatedCode = yoshiCode;

        foreach (var entry in _keywords)
        {
            string pattern;

        if (entry.Key.StartsWith("hatch"))
        {
            pattern = entry.Key + @"(?=(?:[^""]*""[^""]*"")*[^""]*$)";
        }
        else
        {
            pattern = @"\b" + Regex.Escape(entry.Key) + @"\b(?=(?:[^""]*""[^""]*"")*[^""]*$)";
        }

        translatedCode = Regex.Replace(translatedCode, pattern, entry.Value, RegexOptions.Singleline);
    }

    return translatedCode;
    }

    public async Task RunAsync(string yoshiCode)
    {
        string cSharpCode = Translate(yoshiCode);

        var options = ScriptOptions.Default
            .AddReferences(typeof(Console).Assembly, typeof(YoshiEngine).Assembly)
            .AddImports("System", "YoshiSharp.Core", "YoshiSharp.Core.YoshiEngine");

        try
        {
            var scriptState = await CSharpScript.RunAsync(cSharpCode, options);
        }
        catch (CompilationErrorException ex)
        {
            Console.WriteLine($"The engine stalled: {ex.Message}");
        }
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