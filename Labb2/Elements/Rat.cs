
class Rat : Enemy
{
    private static Random random = new Random();
    public bool HasAttackedFirst { get; set; }

    public Rat(Position position) : base(position, 'r', ConsoleColor.Red, elementType.Rat)
    {
        Health = 10;
        Name = "rat";
    }

    public override bool Update(List<LevelElement> Elements, Player player)
    {
        ClearOldPosition();
        var isDead = MoveEnemy(Elements);

        IsVisible = player.IsWithinVisionRange(this);
        if (IsVisible)
        {      
            DrawNewPosition();
        }

        return isDead;
    }

    public bool MoveEnemy(List<LevelElement> Elements)
    {
        var isDead = false;
        int ratMove = random.Next(4);

        Position newRatPosition = new Position(this.Position);

        switch (ratMove)
        {
            case 0: // Left
                newRatPosition.X = newRatPosition.X - 1;
                break;

            case 1: // Right
                newRatPosition.X = newRatPosition.X + 1;
                break;

            case 2: // Up
                newRatPosition.Y = newRatPosition.Y - 1;
                break;

            case 3: // Down
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

            HasAttackedFirst = true;
            isDead = player.DealWithRetaliation((ratDamage - playerDefence), this, HasAttackedFirst);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(0, 2);
            Console.WriteLine($"{this} attacked {player.Name} with {ratDamage} ({ratAttackDice}) damage. {player.Name} defence: {playerDefence} ({playerDefenceDice}) {player.Name} took {ratDamage - playerDefence} damage ({player.Health}) health left.".PadRight(Console.BufferWidth));
            Console.ResetColor();
        }

        if (IsMoveAllowed(newRatPosition.X, newRatPosition.Y, Elements))
        {
            Position = newRatPosition;
        }

        return isDead;
    }

}