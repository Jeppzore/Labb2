using System.IO;


internal class Program
{
    private static void Main(string[] args)
    {
        LevelData level = new LevelData();

        string filePath = @"Levels\\Level1.txt";
        level.Load(filePath);

        foreach (LevelElement element in level.Elements)
        {
            element.Draw();
        }

    }

}

        //Console.SetCursorPosition(0, 2);




        /*Console.Write("Testslag: ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"{dice1.ThrowDice(1, diceSides.Next(1, 7), 2)}"); // Test för att slå en tärning. diceSides returnerar ett slumpat tal mellan 1 och 6
        Console.ResetColor();
        */



    
