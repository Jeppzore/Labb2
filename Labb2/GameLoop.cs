using System.Runtime.CompilerServices;

class GameLoop
{
    private LevelData levelData;

    public GameLoop(LevelData levelData)
    {
        this.levelData = levelData;
    }

    public static void Start()
    {
        Console.SetWindowSize(120, 30);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Welcome to Jesper's Dungeon Crawler!\nPlease enter your name (max 8 characters): ");
        string playerName = Console.ReadLine()!;

        if (playerName.Length <= 0 || playerName.Length > 8)
        {
            Console.Clear();
            Start();
        }

        Console.ResetColor(); Console.Clear();

        LevelData level = new LevelData();
        string filePath = @"Levels\\Level1.txt";
        level.Load(filePath);
        Console.CursorVisible = false;

        DisplayControls();
        MoveElements(playerName, level);
    }

    private static void MoveElements(string playerName, LevelData levelData)
    {
        int numberOfTurns = 0;

        Player myPlayer = (Player)LevelData.Elements.FirstOrDefault(x => x.Type == elementType.Player)!;
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
            levelData.DrawElementsWithinRange(myPlayer, myPlayer.VisionRange);
            MovePlayer(myPlayer);
            numberOfTurns++;

            var enemies = LevelData.Elements.OfType<Enemy>().ToList();
            var deadEnemies = new List<Enemy>();
            foreach (var enemy in enemies)
            {
                var isDead = enemy.Update(LevelData.Elements, myPlayer);
                if (isDead)
                {
                    deadEnemies.Add(enemy);
                }
            }

            foreach (var enemy in deadEnemies)
            {
                LevelData.Elements.Remove(enemy);
                enemy.Clear();
            }
        }

        Console.Clear();
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("Game over! Restarting game...");
        Thread.Sleep(3000);
        Console.Clear();
        Start();
    }

    private static void MovePlayer(Player myPlayer)
    {
        var key = Console.ReadKey(true).Key;
        myPlayer.Clear();

        HealthPotion? healthPotion = (HealthPotion)LevelData.Elements.FirstOrDefault(x => x.Type == elementType.HealthPotion)!;

        switch (key)
        {
            case ConsoleKey.W: // Upp
                if (myPlayer.Position.Y > 0)
                {
                    LevelElement? element = LevelData.Elements.FirstOrDefault(elem => elem.Position.X == myPlayer.Position.X && elem.Position.Y == myPlayer.Position.Y - 1);
                    DoMovePlayer(myPlayer, healthPotion, element, new Position (myPlayer.Position.X, myPlayer.Position.Y - 1));
                }
                break;

            case ConsoleKey.S: // Ner
                if (myPlayer.Position.Y < 18 - 1)
                {
                    LevelElement? element = LevelData.Elements.FirstOrDefault(elem => elem.Position.X == myPlayer.Position.X && elem.Position.Y == myPlayer.Position.Y + 1);                 
                    DoMovePlayer(myPlayer, healthPotion, element, new Position(myPlayer.Position.X, myPlayer.Position.Y + 1));         
                }
                break;

            case ConsoleKey.A: // Vänster
                if (myPlayer.Position.X > 0)
                {
                    LevelElement? element = LevelData.Elements.FirstOrDefault(elem => elem.Position.X == myPlayer.Position.X - 1 && elem.Position.Y == myPlayer.Position.Y);
                    DoMovePlayer(myPlayer, healthPotion, element, new Position(myPlayer.Position.X - 1, myPlayer.Position.Y));
                }
                break;

            case ConsoleKey.D: // Höger
                if (myPlayer.Position.X < 53 - 1)
                {
                    LevelElement? element = LevelData.Elements.FirstOrDefault(elem => elem.Position.X == myPlayer.Position.X + 1 && elem.Position.Y == myPlayer.Position.Y);
                    DoMovePlayer(myPlayer, healthPotion, element, new Position(myPlayer.Position.X + 1, myPlayer.Position.Y));
                }
                break;

            case ConsoleKey.Escape: // ESC
                Console.Clear();
                Start();
                break;
        }
    }

    private static void DoMovePlayer(Player myPlayer, HealthPotion? healthPotion, LevelElement? element, Position position)
    {
        if (element is null)
        {
            myPlayer.Position = new Position(position.X, position.Y);
            return;
        }
        else if (element is Enemy enemy)
        {
            DoPlayerAttack(myPlayer, enemy);
        }
        else if (element is HealthPotion)
        {
            myPlayer.Position = new Position(position.X, position.Y);
            myPlayer.RestoreHealth(myPlayer);
            healthPotion!.Clear(); 
            LevelData.Elements.Remove(healthPotion);
            return;
        }
    }

    private static void DoPlayerAttack(Player myPlayer, Enemy enemy)
    {
        Dice playerAttackDice = new Dice(1, 6, (2 * myPlayer.Level));
        int playerDamage = playerAttackDice.ThrowDice();

        if(enemy.Type == elementType.Rat)
        {
            Dice ratDefenceDice = new Dice(1, 3, 0);
            int ratDefence = ratDefenceDice.ThrowDice();
            enemy.DealWithDamage(playerDamage - ratDefence, myPlayer);
        
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, 2);
            Console.WriteLine($"{myPlayer.Name} attacked {enemy.Name} with: {playerDamage} ({playerAttackDice}) damage. {enemy.Name} defence: {ratDefence} ({ratDefenceDice}) {enemy.Name} took {playerDamage - ratDefence} damage ({enemy.Health} health left).".PadRight(Console.BufferWidth));
        }
      
        if (enemy.Type == elementType.Snake)
        {
            Dice snakeDefenceDice = new Dice(1, 8, 1);
            int snakeDefence = snakeDefenceDice.ThrowDice();
            enemy.DealWithDamage(playerDamage - snakeDefence, myPlayer);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, 2);
            Console.WriteLine($"{myPlayer.Name} attacked {enemy.Name} with: {playerDamage} ({playerAttackDice}) damage. {enemy.Name} defence: {snakeDefence} ({snakeDefenceDice}) {enemy.Name} took {playerDamage - snakeDefence} damage ({enemy.Health} health left).".PadRight(Console.BufferWidth));
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
