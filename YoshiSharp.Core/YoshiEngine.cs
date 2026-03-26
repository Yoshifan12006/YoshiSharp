using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace YoshiSharp.Core;

public class YoshiEngine
{
    private readonly Dictionary<string, string> _dictionary = new()
    {
        { "mlem","public" },
        { "melon", "int" },
        { "tongue", "string" },
        { "berry", "bool" },
        { "sparkle", "double" },
        { "gulp", "void" },
        { "Yoshi.Say", "Console.WriteLine" },
        { "Yoshi.Eat", "Console.ReadLine" },
        { "ha-pu", "if" },
        { "wah-hoo", "else" },
        { "stampede", "for" },
        { "flutter-jump", "while" }
    };

    public string Transpile(string yoshiCode)
    {
        return Regex.Replace(yoshiCode, @"[\w\.-]+", match =>
            _dictionary.ContainsKey(match.Value) ? _dictionary[match.Value] : match.Value);
    }

    public async Task RunAsync(string yoshiCode)
    {
        string cSharpCode = Transpile(yoshiCode);

        var options = ScriptOptions.Default.WithImports("System");
        await CSharpScript.RunAsync(cSharpCode, options);
    }
}