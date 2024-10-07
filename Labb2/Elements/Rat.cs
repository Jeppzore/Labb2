// Rat förflyttar sig 1 steg i slumpmässig vald riktning
// (upp, ner, höger eller vänster) varje omgång.
// Varken spelare, rats eller snakes kan gå igenom väggar eller varandra.

class Rat : Enemy
{

    public Rat(Position position) : base(position, 'r', ConsoleColor.Red, elementType.Rat)
    {
        Health = 10;
        Name = "rat";
    }

    // attack = 1d6+3
    // defence = 1d6+1

    public override void Update(List<LevelElement> elements) //Rörelsemönstret/ allt som fienden ska göra i varje drag
    {

        ClearOldPosition();

        Random random = new Random();
        int ratMove = random.Next(4);

        int newRatPositionX = Position.X;
        int newRatPositionY = Position.Y+4;

        switch (ratMove)
        {
            case 0: // Vänster
                newRatPositionX = newRatPositionX - 1;
                break;

            case 1: // Vänster
                newRatPositionX = newRatPositionX + 1;
                break;

            case 2: // Vänster
                newRatPositionY = newRatPositionY - 1;
                break;

            case 3: // Vänster
                newRatPositionY = newRatPositionY + 1;
                break;
        }

        if (IsMoveAllowed(newRatPositionX, newRatPositionY, elements))
        {
            Position.X = newRatPositionX;

        }

        DrawNewPosition();



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

    }

}