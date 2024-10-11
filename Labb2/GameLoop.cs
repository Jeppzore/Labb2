class GameLoop
{
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
        Console.CursorVisible = false;

        LevelData level = new LevelData();
        string filePath = @"Levels\\Level1.txt";
        level.Load(filePath);

        DisplayControls();
        MoveElements(playerName, level);
    }

    private static void MoveElements(string playerName, LevelData levelData)
    {
        int numberOfTurns = 0;

        Player myPlayer = (Player)LevelData.Elements.FirstOrDefault(x => x.Type == elementType.Player)!;
        myPlayer.Name = playerName;
        myPlayer.SetHP();

        // Main Game loop
        while (myPlayer.Health > 0)
        {
            myPlayer.PlayerLevelCheck();
            CheckWinCondition(myPlayer);

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
            case ConsoleKey.W: // Up
                if (myPlayer.Position.Y > 0)
                {
                    DoMovePlayer(myPlayer, healthPotion, new Position(myPlayer.Position.X, myPlayer.Position.Y - 1));
                }
                break;

            case ConsoleKey.S: // Down
                if (myPlayer.Position.Y < 18 - 1)
                {
                    DoMovePlayer(myPlayer, healthPotion, new Position(myPlayer.Position.X, myPlayer.Position.Y + 1));
                }
                break;

            case ConsoleKey.A: // Left
                if (myPlayer.Position.X > 0)
                {
                    DoMovePlayer(myPlayer, healthPotion, new Position(myPlayer.Position.X - 1, myPlayer.Position.Y));
                }
                break;

            case ConsoleKey.D: // Right
                if (myPlayer.Position.X < 53 - 1)
                {
                    DoMovePlayer(myPlayer, healthPotion, new Position(myPlayer.Position.X + 1, myPlayer.Position.Y));
                }
                break;

            case ConsoleKey.Escape: // ESC
                Console.Clear();
                Start();
                break;
        }
    }

    private static void DoMovePlayer(Player myPlayer, HealthPotion? healthPotion, Position position)
    {
        LevelElement? element = LevelData.Elements.FirstOrDefault(elem => elem.Position.X == position.X && elem.Position.Y == position.Y);

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

        if (enemy.Type == elementType.Rat)
        {
            Dice ratDefenceDice = new Dice(1, 3, 0);
            DealDamageToEnemy(myPlayer, enemy, playerAttackDice, playerDamage, ratDefenceDice);
        }

        if (enemy.Type == elementType.Snake)
        {
            Dice snakeDefenceDice = new Dice(1, 8, 1);
            DealDamageToEnemy(myPlayer, enemy, playerAttackDice, playerDamage, snakeDefenceDice);
        }
    }

    private static void DealDamageToEnemy(Player myPlayer, Enemy enemy, Dice playerAttackDice, int playerDamage, Dice enemyDefenceDice)
    {
        int enemyDefence = enemyDefenceDice.ThrowDice();
        enemy.DealWithDamage(playerDamage - enemyDefence, myPlayer);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.SetCursorPosition(0, 2);
        Console.WriteLine($"{myPlayer.Name} attacked {enemy.Name} with: {playerDamage} ({playerAttackDice}) damage. {enemy.Name} defence: {enemyDefence} ({enemyDefenceDice}) {enemy.Name} took {playerDamage - enemyDefence} damage ({enemy.Health} health left).".PadRight(Console.BufferWidth));
    }

    public static void CheckWinCondition(Player myPlayer)
    {
        if (myPlayer.Experience >= 145)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("Y O U  W I N!");
            Thread.Sleep(2000);

            Console.WriteLine("Restarting game...");
            Thread.Sleep(3000);

            Console.ResetColor();
            Console.Clear();
            Start();
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

        Console.SetCursorPosition(70, 13);
        Console.WriteLine("Esc: Restart game");

        Console.ResetColor();
    }
}
