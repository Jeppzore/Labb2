using System.IO;


internal class Program
{
    private static void Main(string[] args)
    {
        Console.SetCursorPosition(0, 2);

        Random diceSides = new Random();
        Dice dice1 = new Dice();
        LevelData leveldata = new LevelData();

        Console.Write("Testslag: ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"{dice1.ThrowDice(1, diceSides.Next(1, 7), 2)}"); // Test för att slå en tärning. diceSides returnerar ett slumpat tal mellan 1 och 6
        Console.ResetColor();

        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.White;
        string filePath = @"Levels\\Level1.txt";
        leveldata.Load(filePath);

        foreach (LevelElement element in leveldata.Elements)
        {
            element.Draw();
        }
    }
}