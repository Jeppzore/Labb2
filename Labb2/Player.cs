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

class Player : LevelElement
{
    public Dice DefenceDice { get; set; }
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
    }

    public void PlayerDealWithDamage(int damage, Enemy enemy)
    {
        // Reducera Player.Health med den damage som skickas in från enemy om damage >= 0
        if (damage >= 0)
        {
            Health -= damage;
        }
    }

    public void PlayerLevelCheck()
    {
        if (Level == 1 && Experience >= 20) 
        {
            Level++;
            SetHP();
            Console.SetCursorPosition(0, 26);
            Console.WriteLine($"Congratulations! You advanced to level: {this.Level}. You gain full health ({Health})".PadRight(Console.BufferWidth));
            Console.ResetColor();
        }

        if (Level == 2 && Experience >= 60)
        {
            Level++;
            SetHP();
            Console.SetCursorPosition(0, 26);
            Console.WriteLine($"Congratulations! You advanced to level: {this.Level}. You gain full health ({Health})".PadRight(Console.BufferWidth));
            Console.ResetColor();
        }

    }

    public void SetHP()
    {
        Health = MaxHealth * Level;
        MaxHealth = Health;
    }

}