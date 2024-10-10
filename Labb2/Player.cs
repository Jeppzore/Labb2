//Varken spelare, rats eller snakes kan gå igenom väggar eller varandra.


// Ärver av LevelElement för att kunna hålla reda på om spelaren
// krockar med vägg/enemy t.ex.

// För att få en effekt av “utforskande” i spelet begränsar vi spelarens
// synfält till att bara visa objekt inom en radie av 5 tecken
// (men ni kan också prova med andra radier);
// Väggarna försvinner dock aldrig när man väl sett dem,
// men fienderna syns inte så fort de kommer utanför radien.

//Avståndet mellan två punkter i 2D kan enkelt beräknas
//med hjälp av pythagoras sats.


using System.Numerics;
using System.Security.Cryptography.X509Certificates;

class Player : LevelElement
{
    public Dice DefenceDice { get; set; }
    public int VisionRange { get; private set; }
    public int MaxHealth { get; set; }
    public int Health { get; set; }
    public int Level { get; set; }
    public int Experience { get; set; }
    public string Name { get; set; }


    public Player(Position position) : base(position, '@', ConsoleColor.Yellow, elementType.Player)
    {
        Health = 100;
        MaxHealth = Health;
        Name = "Player";
        Level = 1;
        Experience = 0;
        VisionRange = 5;
    }

    public bool IsWithinVisionRange(LevelElement element)
    {
        int distanceX = Math.Abs(this.Position.X - element.Position.X);
        int distanceY = Math.Abs(this.Position.Y - element.Position.Y);
        return distanceX <= VisionRange && distanceY <= VisionRange;
    }

    public void DealWithDamage(int damage, Enemy enemy)
    {
        if (damage >= 0)
        {
            Health -= damage;
        }
    }

    public void DealWithRetaliation(int damage, Enemy enemy, bool hasAttackedFirst)
    {
        if (damage >= 0)
        {
            Health -= damage;
        }

        if (enemy.Type == elementType.Rat && hasAttackedFirst == true)
        {
            Dice playerAttackDice = new Dice(1, 6, (2 * this.Level));
            int playerDamage = playerAttackDice.ThrowDice();

            Dice ratDefenceDice = new Dice(1, 3, 0);
            int ratDefence = ratDefenceDice.ThrowDice();

            enemy.Health -= (playerDamage - ratDefence);

            if (enemy.Health <= 0)
            {
                enemy.Health = 0;
            //    Console.SetCursorPosition(0, 24);
            //    this.Experience += 5;
            //    Console.ForegroundColor = ConsoleColor.Yellow;
            //    Console.WriteLine($"You received 5 experience points".PadRight(Console.BufferWidth));
            //    Console.SetCursorPosition(0, 25);
            //    Console.WriteLine($"Current experience: {this.Experience}".PadRight(Console.BufferWidth));
            //    Console.ResetColor();

                //LevelData.Elements.Remove(this);
                //enemy.Clear();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, 3);
            Console.WriteLine($"{this.Name} attacked {enemy.Name} with: {playerDamage} ({playerAttackDice}) damage. {enemy.Name} defence: {ratDefence} ({ratDefenceDice}). {enemy.Name} took {playerDamage - ratDefence} damage. ({enemy.Health} health left).".PadRight(Console.BufferWidth));
            Console.ResetColor();
        }

        hasAttackedFirst = false;
    }

    public void PlayerLevelCheck()
    {
        if (Level == 1 && Experience >= 20) 
        {
            Level++;
            SetHP();
            Console.SetCursorPosition(0, 26);
            Console.WriteLine($"Congratulations! You advanced to level: {this.Level}. You gain full health ({Health}). Attack modifier increased".PadRight(Console.BufferWidth));
            Console.ResetColor();
        }

        if (Level == 2 && Experience >= 60)
        {
            Level++;
            SetHP();
            Console.SetCursorPosition(0, 26);
            Console.WriteLine($"Congratulations! You advanced to level: {this.Level}. You gain full health ({Health}). Attack modifier increased".PadRight(Console.BufferWidth));
            Console.ResetColor();
        }

    }

    public void SetHP()
    {
        Health = MaxHealth * Level;
        MaxHealth = Health;
    }

     public void RestoreHealth(Player player)
    {
        player.Health = player.MaxHealth;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.SetCursorPosition(0, 26);
        Console.WriteLine($"Your health was fully restored to {player.MaxHealth} health.".PadRight(Console.BufferWidth));
        Console.ResetColor();
        Clear();
    }
}