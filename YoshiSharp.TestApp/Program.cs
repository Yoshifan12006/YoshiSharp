using System;
using YoshiSharp.Core;

var engine = new YoshiEngine();

string yoshiSource = @"
Yoshi.Paint(""Yellow"");
Yoshi.Say(""How many eggs should I try to hatch?"");

tongue input = Yoshi.Eat();
melon totalAttempts = int.Parse(input);
melon successCount = 0;

Yoshi.Paint(""Green"");
flutter-jump (totalAttempts > 0) {

    melon eggChance = hatch(1, 11); 
    
    ha-pu (eggChance > 5) {
        Yoshi.Say(""Hatched a green egg!"");
        successCount = successCount + 1;
    }
    wah-hoo {
        Yoshi.Say(""Oh no, the egg was empty..."");
    }
    
    totalAttempts = totalAttempts - 1;
}

Yoshi.Paint(""Pink"");
Yoshi.Say(""Total eggs hatched: "" + successCount);
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