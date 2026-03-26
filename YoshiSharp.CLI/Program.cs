using System;
using System.IO;
using YoshiSharp.Core;

if (args.Length == 0)
{
    Console.WriteLine("Usage: yoshi-run <filename.yoshi>");
    return;
}

string filePath = args[0];

if (!File.Exists(filePath))
{
    Console.WriteLine($"Error: Could not find file '{filePath}'");
    return;
}

var engine = new YoshiEngine();
string code = File.ReadAllText(filePath);

Console.WriteLine($"--- Yoshi is digesting {filePath} ---");

try
{
    await engine.RunAsync(code);
}
catch (Exception ex)
{
    Console.WriteLine($"Yoshi has a tummy ache: {ex.Message}");
}