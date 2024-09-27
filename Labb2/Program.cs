
// MAIN PROGRAM - HÄR KAN ALLT SKRIVAS "OVANFÖR" Classes (TOP-LEVEL-STATEMENT)
// T.ex. Instanisera nya objekt

using System.IO;

Dice dice1 = new Dice();

Console.WriteLine("Du attackerar med:");
Console.WriteLine(dice1.ThrowDice(1, 6, 2)); // Test för att slå en tärning

String line;

try
{
    //Pass the file path and file name to the StreamReader constructor
    var path = Path.Combine(Directory.GetCurrentDirectory(), "Levels\\Level1.txt");
    StreamReader sr = new StreamReader(path);

    //Read the first line of text
    line = sr.ReadLine();
    //Continue to read until you reach end of file
    while (line != null)
    {
        //write the line to console window
        Console.WriteLine(line);
        //Read the next line
        line = sr.ReadLine();
    }
    //close the file
    sr.Close();
    Console.ReadLine();
}
catch (Exception e)
{
    Console.WriteLine("Exception: " + e.Message);
}
finally
{
    Console.WriteLine("Executing finally block.");
}