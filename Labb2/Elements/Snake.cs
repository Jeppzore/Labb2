
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
            return false;

        int distanceToPlayerX = Math.Abs(player.Position.X - Position.X);
        int distanceToPlayerY = Math.Abs(player.Position.Y - Position.Y);

        if (distanceToPlayerX > 2 || distanceToPlayerY > 2)
        {
            return false;
        }

        // Move Snake away from player X
        if (player.Position.X < Position.X)
        {
            newSnakePosition.X = Position.X + 1;
        }
        else if (player.Position.X > Position.X)
        {
            newSnakePosition.X = Position.X - 1;
        }

        // Move Snake away from player Y
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

        return false;
    }
}