//Alla riktiga fiender (i labben rat & snake, men om man vill och har tid får man lägga till fler typer av fiender) ärver av denna klass.

//Enemy ska ha properties för namn (t.ex snake/rat), hälsa (HP), samt AttackDice och DefenceDice av typen Dice (mer om detta längre ner).
//Den ska även ha en abstrakt Update-metod, som alltså inte implementeras i denna klass, men som kräver att alla som ärver av klassen implementerar den.
//Vi vill alltså kunna anropa Update-metoden på alla fiender och sedan sköter de olika subklasserna hur de uppdateras (till exempel olika förflyttningsmönster).

abstract class Enemy : LevelElement
{
    public string Name { get; set; }
    public int Health { get; set; } // Property HP
    public Dice AttackDice { get; set; } // Property AttackDice
    public Dice DefenceDice { get; set; } // Property DefenceDice 

    protected Enemy(Position position, char icon, ConsoleColor consoleColor, elementType type) : base(position, icon, consoleColor, type)
    {

    }
    protected bool IsMoveAllowed(int newX, int newY, List<LevelElement> elements)
    {
        foreach (var element in elements)
        {
            if (element.Position.X == newX && element.Position.Y == newY)
            {
                return false; // kollision med ett objekt
            }
        }
        return true; // Ingen kollision, fltyten är giltig
    }

    protected void ClearOldPosition()
    {
        Console.SetCursorPosition(Position.X, Position.Y + 5);
        Console.Write(' ');
    }

    protected void DrawNewPosition()
    {
        Console.SetCursorPosition(Position.X, Position.Y + 5);
        Console.ForegroundColor = CharacterColor;
        Console.Write(Icon);
        Console.ResetColor();
    }

    public void EnemyTakeDamage(int damage, Player player)
    {
        Health -= damage;

        if (Health <= 0)
        {
            Health = 0; // Ser till att health aldrig blir mindre än 0

            if (this.Type == elementType.Rat)
            {
                Console.SetCursorPosition(0, 24);
                player.Experience += 5;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"You received 5 experience points".PadRight(Console.BufferWidth));
                Console.SetCursorPosition(0, 25);
                Console.WriteLine($"Current experience: {player.Experience}".PadRight(Console.BufferWidth));
                Console.ResetColor();
            }

            if (this.Type == elementType.Snake)
            {
                Console.SetCursorPosition(0, 24);
                player.Experience += 10;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"You received 10 experience points".PadRight(Console.BufferWidth));
                Console.SetCursorPosition(0, 25);
                Console.WriteLine($"Current experience: {player.Experience}".PadRight(Console.BufferWidth));
                Console.ResetColor();
            }

            LevelData.Elements.Remove(this);
            Clear();
            return;
        }

        // Om enemy är vid liv efter att ha tagit skada - gör skada tillbaka på spelaren
        if (this.Type == elementType.Rat)
        {
            Dice ratAttackDice = new Dice(1, 6, 1);
            int ratDamage = ratAttackDice.ThrowDice();
            player.PlayerTakeDamage(ratDamage);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(0, 3);
            Console.WriteLine($"{this.Name} attacked {player.Name} with: {ratDamage} ({ratAttackDice}) points of damage. {player.Name} has {player.Health} health left.".PadRight(Console.BufferWidth));
            Console.ResetColor();
        }

        if (this.Type == elementType.Snake)
        {
            Dice snakeAttackDice = new Dice(2, 6, 3);
            int snakeDamage = snakeAttackDice.ThrowDice();
            player.PlayerTakeDamage(snakeDamage);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(0, 3);
            Console.WriteLine($"{this.Name} attacked {player.Name} with: {snakeDamage} ({snakeAttackDice}) points of damage. {player.Name} has {player.Health} health left.".PadRight(Console.BufferWidth));
            Console.ResetColor();
        }
    }

    public abstract void Update(List<LevelElement> elements); // Abstract Update-metod som inte implementeras här men som krävs implementation av alla klasser som ärver av Enemy

}
