using System.IO;
using System.Linq.Expressions;


internal class Program
{
    private static void Main(string[] args)
    {
        LevelData level = new LevelData();
        

        string filePath = @"Levels\\Level1.txt";
        level.Load(filePath);
        Console.CursorVisible = false;

        foreach (LevelElement element in level.Elements)
        {
            element.Draw();
        }

    /*static void MovePlayer()
    {
        Player player = new Player(5,5);

        var key = Console.ReadKey(true).Key;

        switch (key)
        {
            case ConsoleKey.W: // Upp
                if(player.Y > 0) player.Y--;
                break;

            case ConsoleKey.S: // Ner
                if(player.Y < 0) player.Y--;
                break;

            case ConsoleKey.A: // Vänster
                if (player.Y > 0) player.Y--;
                break;

            case ConsoleKey.D: // HÖger
                if (player.Y > 0) player.Y--;
                break;
            }
    }*/
    }


}

        //Console.SetCursorPosition(0, 2);




        /*Console.Write("Testslag: ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"{dice1.ThrowDice(1, diceSides.Next(1, 7), 2)}"); // Test för att slå en tärning. diceSides returnerar ett slumpat tal mellan 1 och 6
        Console.ResetColor();
        */



    
