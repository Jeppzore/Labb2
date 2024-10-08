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

using System.Xml.Linq;

class GameLoop
{
    public static void Start()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Welcome to Jesper's Dungeon Crawler!\nPlease enter your name: ");
        string playerName = Console.ReadLine();       

        Console.ResetColor();
        Console.Clear();

        LevelData level = new LevelData();

        string filePath = @"Levels\\Level1.txt";
        level.Load(filePath);
        Console.CursorVisible = false;

        foreach (LevelElement element in LevelData.Elements)
        {
            element.Draw();
        }

        MoveElements(playerName);
    }


    private static void MoveElements(string playerName)
    {
        // Håller koll på antal turns och adderas med 1 för varje gång spelaren rör sig
        int numberOfTurns = 0;

        // Skapar ny Player med hittat position och Använder LINQ-metod FirstOrDefault
        // på en lista av LevelElement för att hitta första eller default-matchen av Player
        Player myPlayer = (Player)LevelData.Elements.FirstOrDefault(x => x.Type == elementType.Player);
        myPlayer.Name = playerName;

        // Loopen som körs så länge spelaren lever
        while (myPlayer.Health > 0)
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Name: {myPlayer.Name}     Health: {myPlayer.Health}    Level: {myPlayer.Level}     Turns: {numberOfTurns}");
            Console.ResetColor();

            numberOfTurns++;
            myPlayer.Draw();
            MovePlayer(myPlayer);

            // För varje gång MovePlayer har körts - kalla på Update() metoden för samtliga Enemy
            foreach (var element in LevelData.Elements.OfType<Enemy>())
            {
                element.Update(LevelData.Elements);
            }

        }
    }

    private static void MovePlayer(Player myPlayer)
    {
        var key = Console.ReadKey(true).Key;
        myPlayer.Clear();
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
                    else if (element is Enemy enemy) // Kolla om elementet är en Enemy
                    {
                    DoPlayerAction(myPlayer, enemy); // Attackera enemy
                    }
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
                    else if (element is Enemy enemy) // Kolla om elementet är en Enemy
                    {
                        DoPlayerAction(myPlayer, enemy); // Attackera enemy
                    }
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
                    else if (element is Enemy enemy) // Kolla om elementet är en Enemy
                    {
                        DoPlayerAction(myPlayer, enemy); // Attackera enemy
                    }
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
                    else if (element is Enemy enemy) // Kolla om elementet är en Enemy
                    {
                        DoPlayerAction(myPlayer, enemy); // Attackera enemy
                    }                 
                }
                break;

        }
    }

    private static void DoPlayerAction(Player myPlayer, Enemy enemy)
    {
        Dice playerAttackDice = new Dice(1, 6, 2);
        int damage = playerAttackDice.ThrowDice();

        // Minska enemy health med resultatet från tärningskastet
        enemy.TakeDamage(damage);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.SetCursorPosition(0, 2);
        Console.WriteLine($"{myPlayer.Name} attacked {enemy.Name} with: {damage} ({playerAttackDice}) points of damage. {enemy.Name} has {enemy.Health} health left.".PadRight(Console.BufferWidth));
        Console.ResetColor();

    }

}
