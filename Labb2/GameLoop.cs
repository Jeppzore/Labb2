using System.Runtime.CompilerServices;

class GameLoop
{
    public static void Start()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Welcome to Jesper's Dungeon Crawler!\nPlease enter your name (max 12 characters): ");
        string playerName = Console.ReadLine();

        if (playerName.Length <= 0 || playerName.Length > 12)
        {
            Console.Clear();
            Start();
        }

        Console.ResetColor(); Console.Clear();


        LevelData level = new LevelData();
        string filePath = @"Levels\\Level1.txt";
        level.Load(filePath);
        Console.CursorVisible = false;

        foreach (LevelElement element in LevelData.Elements)
        {
            element.Draw();
        }

        DisplayControls();
        MoveElements(playerName);
    }

    private static void MoveElements(string playerName)
    {
        int numberOfTurns = 0;

        // Skapar ny Player med hittat position och Använder LINQ-metod FirstOrDefault
        // på en lista av LevelElement för att hitta första eller default-matchen av Player
        Player myPlayer = (Player)LevelData.Elements.FirstOrDefault(x => x.Type == elementType.Player);
        myPlayer.Name = playerName;
        myPlayer.SetHP();

        // Loopen som körs så länge spelaren lever
        while (myPlayer.Health > 0)
        {
            myPlayer.PlayerLevelCheck();

            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Name: {myPlayer.Name}     Health: {myPlayer.Health}/{myPlayer.MaxHealth}    Level: {myPlayer.Level}     Turns: {numberOfTurns}".PadRight(Console.BufferWidth));
            Console.ResetColor();

            myPlayer.Draw();
            MovePlayer(myPlayer);
            numberOfTurns++;

            // För varje gång MovePlayer har körts - kalla på Update() metoden för samtliga Enemy
            foreach (var element in LevelData.Elements.OfType<Enemy>())
            {
                element.Update(LevelData.Elements);
            }
        }

        // Om myPlayer.Health > 0 är spelet över
        Console.Clear();
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("Game over! Restarting game...");
        Thread.Sleep(3000);
        Console.Clear();
        //Startar om spelet 3 sekunder senare
        Start();

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

            case ConsoleKey.Escape: // ESC
                Console.Clear();
                Start();
                break;
        }
    }

    // Gör skada på den enemy spelaren aktivt går på. Skadan bestäms av playerdamage - enemydefence
    private static void DoPlayerAction(Player myPlayer, Enemy enemy)
    {
        Dice playerAttackDice = new Dice(1, 6, (2 * myPlayer.Level));
        int playerDamage = playerAttackDice.ThrowDice();

        if(enemy.Type == elementType.Rat)
        {
            Dice ratDefenceDice = new Dice(1, 3, 0);
            int ratDefence = ratDefenceDice.ThrowDice();
            enemy.EnemyDealWithDamage(playerDamage - ratDefence, myPlayer);
        
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, 2);
            Console.WriteLine($"{myPlayer.Name} attacked {enemy.Name} with: {playerDamage - ratDefence} ({playerAttackDice}) damage. {enemy.Name} defence: {ratDefence} ({ratDefenceDice}) {enemy.Name} has {enemy.Health} health left.".PadRight(Console.BufferWidth));
            Console.ResetColor();
        }
      
        if (enemy.Type == elementType.Snake)
        {
            Dice snakeDefenceDice = new Dice(1, 8, 1);
            int snakeDefence = snakeDefenceDice.ThrowDice();
            enemy.EnemyDealWithDamage(playerDamage - snakeDefence, myPlayer);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, 2);
            Console.WriteLine($"{myPlayer.Name} attacked {enemy.Name} with: {playerDamage - snakeDefence} ({playerAttackDice}) damage. {enemy.Name} defence: {snakeDefence} ({snakeDefenceDice}) {enemy.Name} has {enemy.Health} health left.".PadRight(Console.BufferWidth));
            Console.ResetColor();
        }
    }
    private static void DisplayControls()
    {
        // Display the user control
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.SetCursorPosition(70, 7);
        Console.WriteLine("Controls:");
        Console.SetCursorPosition(70, 8);
        Console.WriteLine("W: Up");
        Console.SetCursorPosition(70, 9);
        Console.WriteLine("A: Left");
        Console.SetCursorPosition(70, 10);
        Console.WriteLine("S: Down");
        Console.SetCursorPosition(70, 11);
        Console.WriteLine("D: Right");

        // Display control how to restart the game
        Console.SetCursorPosition(70, 13);
        Console.WriteLine("Esc: Restart game");
        Console.ResetColor();
    }
}
