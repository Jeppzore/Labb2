
// MAIN PROGRAM - HÄR KAN ALLT SKRIVAS "OVANFÖR" Classes (TOP-LEVEL-STATEMENT)
// T.ex. Instanisera nya objekt

using System.IO;

bool playerFound = false;
bool ratFound = false;
bool snakeFound = false;

Dice dice1 = new Dice();

Console.Write("Du attackerar med: ");
Console.WriteLine(dice1.ThrowDice(1, 6, 2)); // Test för att slå en tärning
Console.WriteLine();
Console.WriteLine();


string filePath = @"Levels\\Level1.txt";
LevelData.Load(filePath);
