
enum elementType
{
    Player,
    Wall,
    Rat,
    Snake,
    HealthPotion,
}

abstract class LevelElement
{
    public bool IsVisible { get; set; }
    public bool IsDiscovered { get; set; }
    public Position Position { get; set; }
    public elementType Type { get; set; }
    protected char Icon { get; set; }
    protected ConsoleColor CharacterColor { get; set; }

    protected LevelElement(Position position, char icon, ConsoleColor consoleColor, elementType type)
    {
        Position = position;
        Icon = icon;
        CharacterColor = consoleColor;
        Type = type;
        IsVisible = false;
    }

    public void Draw()
    {
        Console.SetCursorPosition(Position.X, Position.Y + 5);
        Console.ForegroundColor = CharacterColor;
        Console.WriteLine(Icon);
        Console.ResetColor();
    }

    public void Clear() // Metod för att ersätta objektets position med ett mellanrum
    {
        Console.SetCursorPosition(Position.X, Position.Y + 5);
        Console.WriteLine(' ');
    }

    //public void Discovered(elementType elementType, Player player) //Rörelsemönstret/ allt som fienden ska göra i varje drag
    //{
    //    IsDiscovered = player.IsWithinVisionRange(this);
    //    if (IsDiscovered)
    //    {
            
    //    }
    //}

}

