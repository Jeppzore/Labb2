
public struct Position
{
    public int X { get; set; }
    public int Y { get; set; }

    public Position(Position position) : this(position.X, position.Y) { }

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }
}