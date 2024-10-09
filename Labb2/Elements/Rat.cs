// Rat förflyttar sig 1 steg i slumpmässig vald riktning
// (upp, ner, höger eller vänster) varje omgång.
// Varken spelare, rats eller snakes kan gå igenom väggar eller varandra.


using System.Xml.Linq;
using System.Xml;

class Rat : Enemy
{

    private static Random random = new Random();

    public Rat(Position position) : base(position, 'r', ConsoleColor.Red, elementType.Rat)
    {
        Health = 10;
        Name = "rat";
    }

    // attack = 1d6+3
    // defence = 1d6+1

    public override void Update(List<LevelElement> Elements) //Rörelsemönstret/ allt som fienden ska göra i varje drag
    {

        ClearOldPosition();

        int ratMove = random.Next(4);

        Position newRatPosition = new Position(this.Position);


        switch (ratMove)
        {
            case 0: // Vänster
                newRatPosition.X = newRatPosition.X - 1;
                break;

            case 1: // Höger
                newRatPosition.X = newRatPosition.X + 1;
                break;

            case 2: // Upp
                newRatPosition.Y = newRatPosition.Y - 1;
                break;

            case 3: // Ner
                newRatPosition.Y = newRatPosition.Y + 1;
                break;
        }

        LevelElement? playerEncounter = Elements.FirstOrDefault(player => player.Position.X == newRatPosition.X && player.Position.Y == newRatPosition.Y && player is Player);
        if (playerEncounter is Player player)
        {
            Dice ratAttackDice = new Dice(1, 6, 1);
            int ratDamage = ratAttackDice.ThrowDice();
            player.PlayerDealWithDamage(ratDamage);

            Console.SetCursorPosition(0, 30);
            Console.WriteLine($"{this} attacked {player} with {ratDamage} ({ratAttackDice}) points of damage".PadRight(Console.BufferWidth));
        }

            if (IsMoveAllowed(newRatPosition.X, newRatPosition.Y, Elements))
        {
            Position = newRatPosition;
        }

        DrawNewPosition();
    }

}




//this.Position = new Position(this.Position.X, this.Position.Y);
//this.Draw();

//this.Health--;
//Console.SetCursorPosition(0, 25);
//Console.WriteLine($"Rat Health: {this.Health}");

//if (this.Health <= 0)
//{
//    Console.WriteLine("The rats died");
//    this.Clear();
//}

//Rat rat;
//LevelElement moveRat = LevelData.Elements.FirstOrDefault(elem => elem.Position.X == rat.Position.X && elem.Position.Y == moveRat.Position.Y - 1);
//rat = new Rat(moveRat.Position); // Ger nya Rat objektet positionen av ratElement.Position