


public struct Position
{
    //Publika Fields
    public int X { get; set; }
    public int Y { get; set; }

    //Konstruktor-chaining med en parameter som tar in två till
    public Position(Position position) : this(position.X, position.Y) { }

    //Konstruktor med två parametrar
    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }
}