using System;
using YoshiSharp.Core;

var engine = new YoshiEngine();

string yoshiSource = @"
    sparkle weight = 95.5;
    berry isHungry = true;

    ha-pu (isHungry) {
        Yoshi.Say(""Yoshi weighs "" + weight + ""kg and needs a snack!"");
    }
";

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