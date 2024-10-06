﻿//En game loop är en loop som körs om och om igen medan spelet är igång,
//och i vårat fall kommer ett varv i loopen motsvaras av en omgång i spelet.
//För varje varv i loopen inväntar vi att användaren trycker in en knapp;
//sedan utför vi spelarens drag, följt av datorns drag
//(uppdatera alla fiender), innan vi loopar igen.
//Möjligtvis kan man ha en knapp (Esc) för att avsluta loopen/spelet.

//När spelaren/fiender flyttar på sig behöver vi beräkna deras nya position
//och leta igenom alla vår LevelElements för att se om det finns något annat
//objekt på den platsen man försöker flytta till.
//Om det finns en vägg eller annat objekt (fiende/spelaren) på platsen måste
//förflyttningen avbrytas och den tidigare positionen gälla.
//Notera dock att om spelaren flyttar sig till en plats där det står en fiende
//så attackerar han denna (mer om detta längre ner).
//Detsamma gäller om en fiende flyttar sig till platsen där spelaren står.
//Fiender kan dock inte attackera varandra i spelet.

class GameLoop
{
    public static void Start()
    {
        LevelData level = new LevelData();

        string filePath = @"Levels\\Level1.txt";
        level.Load(filePath);
        Console.CursorVisible = false;

        foreach (LevelElement element in LevelData.Elements)
        {
            element.Draw();
        }

        MoveElements();
    }

    static void MoveElements()
    {
        // Håller koll på antal turns och adderas med 1 för varje gång spelaren rör sig
        int numberOfTurns = 0;

        // Använder LINQ-metod FirstOrDefault på en lista av LevelElement för att hitta första eller default-matchen av elementType.Player
        LevelElement playerElement = LevelData.Elements.FirstOrDefault(x => x.Type == elementType.Player);

        // Skapar ny Player med hårdkodad position (om ingen matchning hittas i LevelElement Elements-listan) annars använd den Player som hittade med dennes position
        Player myPlayer;
        if (playerElement is null)
        {
            myPlayer = new Player(new Position(2, 4));
            myPlayer.Draw();
        }
        else
        {
            myPlayer = new Player(playerElement.Position); // Ger nya Player objektet positionen av playerElement.Position
        }

        while (myPlayer.Health > 0)
        {
            var key = Console.ReadKey(true).Key;
            myPlayer.Clear();
            numberOfTurns++;

            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Player name: {myPlayer.Name}     Health: {myPlayer.Health}     Turns: {numberOfTurns}");

            switch (key)
            {
                case ConsoleKey.W: // Upp
                    if (myPlayer.Position.Y > 0)
                    {
                        LevelElement element = LevelData.Elements.FirstOrDefault(elem => elem.Position.X == myPlayer.Position.X && elem.Position.Y == myPlayer.Position.Y - 1);

                        if (element is null)
                        {
                            myPlayer.Position = new Position(myPlayer.Position.X, myPlayer.Position.Y - 1);
                            break;
                        }

                        DoPlayerAction(element.Type);
                    }
                    break;

                case ConsoleKey.S: // Ner
                    if (myPlayer.Position.Y < 18 - 1)
                    {
                        LevelElement element = LevelData.Elements.FirstOrDefault(elem => elem.Position.X == myPlayer.Position.X && elem.Position.Y == myPlayer.Position.Y + 1);

                        if (element is null)
                        {
                            myPlayer.Position = new Position(myPlayer.Position.X, myPlayer.Position.Y + 1);
                            break;
                        }

                        DoPlayerAction(element.Type);
                    }
                    break;

                case ConsoleKey.A: // Vänster
                    if (myPlayer.Position.X > 0)
                    {
                        LevelElement element = LevelData.Elements.FirstOrDefault(elem => elem.Position.X == myPlayer.Position.X - 1 && elem.Position.Y == myPlayer.Position.Y);

                        if (element is null)
                        {
                            myPlayer.Position = new Position(myPlayer.Position.X - 1, myPlayer.Position.Y);
                            break;
                        }

                        DoPlayerAction(element.Type);
                    }
                    break;

                case ConsoleKey.D: // Höger
                    if (myPlayer.Position.X < 53 - 1)
                    {
                        LevelElement element = LevelData.Elements.FirstOrDefault(elem => elem.Position.X == myPlayer.Position.X + 1 && elem.Position.Y == myPlayer.Position.Y);

                        if (element is null)
                        {
                            myPlayer.Position = new Position(myPlayer.Position.X + 1, myPlayer.Position.Y);
                            break;
                        }

                        DoPlayerAction(element.Type);
                    }
                    break;
            }

            myPlayer.Draw();
        }
    }

    private static void DoPlayerAction(elementType type)
    {

        Random diceSides = new Random();
        Dice playerAttackDice = new Dice();
        LevelElement element = LevelData.Elements.FirstOrDefault(element => element.Type == element.Type);



        switch (type)
        {
            case elementType.Wall:
                break;

            case elementType.Rat:
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(0, 1);
                Console.WriteLine($"Player attacked {elementType.Rat} with {playerAttackDice.ThrowDice(1, diceSides.Next(1, 7), 2)} points of damage"); // Kallar på metoden Throwdice (1 tärning, med slumpat tal 1-6, + 2 modifier)
                Console.ResetColor();

                break;

            case elementType.Snake:
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(0, 1);
                Console.WriteLine($"Player attacked {elementType.Snake} with {playerAttackDice.ThrowDice(1, diceSides.Next(1, 7), 2)} points of damage"); // Kallar på metoden Throwdice (1 tärning, med slumpat tal 1-6, + 2 modifier)
                Console.ResetColor();
                break;
        }
    }
}