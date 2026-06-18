using System;
using YoshiSharp.Core;

var engine = new YoshiEngine();

string yoshiSource = """
egg YoshiCharacter
{
    mlem tongue Name;
    mlem tongue Color;

    mlem gulp Introduce()
    {
        Yoshi.Paint(Color);
        Yoshi.Say("Mlem! I am " + Name + " and my color is " + Color + "!");
    }
}

YoshiCharacter greenYoshi = hatch YoshiCharacter();
greenYoshi.Name = "Yoshi";
greenYoshi.Color = "Green";
greenYoshi.Introduce();
Yoshi.Paint("White");
""";

Console.WriteLine("--- Launching Yoshi# Engine ---");

try
{
    await engine.RunAsync(yoshiSource);
}
catch (Exception ex)
{
    Console.WriteLine($"The engine stalled: {ex.Message}");
}

Console.WriteLine("--- End of Program ---");