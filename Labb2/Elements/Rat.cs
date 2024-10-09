// Rat förflyttar sig 1 steg i slumpmässig vald riktning
// (upp, ner, höger eller vänster) varje omgång.
// Varken spelare, rats eller snakes kan gå igenom väggar eller varandra.

class Rat : Enemy
{
    private static Random random = new Random();

    public Rat(Position position) : base(position, 'r', ConsoleColor.Red, elementType.Rat)
    {
        Health = 10;
        Name = "rat";
    }

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

            Dice playerDefenceDice = new Dice(1, 6, 0);
            int playerDefence = playerDefenceDice.ThrowDice();

            player.PlayerDealWithDamage(ratDamage - playerDefence, this);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(0, 2);
            Console.WriteLine($"{this} attacked {player} with {ratDamage - playerDefence} ({ratAttackDice}) damage. {player.Name} defence: {playerDefence} ({playerDefenceDice})".PadRight(Console.BufferWidth));
            Console.ResetColor();

        }

        if (IsMoveAllowed(newRatPosition.X, newRatPosition.Y, Elements))
        {
            Position = newRatPosition;
        }

        DrawNewPosition();
    }

}