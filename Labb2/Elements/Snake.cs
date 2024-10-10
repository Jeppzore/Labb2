
class Snake : Enemy
{

    public Snake(Position position) : base(position, 's', ConsoleColor.Green, elementType.Snake)
    {
        Health = 20;
        Name = "snake";
    }

    public override bool Update(List<LevelElement> Elements, Player p)
    {
        ClearOldPosition();


        Player player = Elements.OfType<Player>().FirstOrDefault()!;
        Position newSnakePosition = new Position(this.Position);
        IsVisible = p.IsWithinVisionRange(this);
        if (IsVisible)
        {
            DrawNewPosition();
            ClearOldPosition();
        }

        if (player == null)
            return false; // Om spelaren inte finns, avsluta metoden

        // Beräkna avstånd till spelaren
        int distanceToPlayerX = Math.Abs(player.Position.X - Position.X);
        int distanceToPlayerY = Math.Abs(player.Position.Y - Position.Y);

        // Om spelaren är mer än 2 rutor bort, stoppa ormen från att röra sig
        if (distanceToPlayerX > 2 || distanceToPlayerY > 2)
        {
            return false;
        }

        // Rör omren bort från spelaren i X-led
        if (player.Position.X < Position.X)
        {
            newSnakePosition.X = Position.X + 1; 
        }
        else if (player.Position.X > Position.X)
        {
            newSnakePosition.X = Position.X - 1;
        }

        // Rör omren bort från spelaren i Y-led
        if (player.Position.Y < Position.Y)
        {
            newSnakePosition.Y = Position.Y + 1; 
        }
        else if (player.Position.Y > Position.Y) 
        {
            newSnakePosition.Y = Position.Y - 1;
        }

        if (IsMoveAllowed(newSnakePosition.X, newSnakePosition.Y, Elements))
        {
            Position = newSnakePosition;
        }

        //DrawNewPosition();
        return false;
    }
}