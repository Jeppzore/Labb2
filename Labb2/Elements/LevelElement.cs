


//Abstrakt basklass LevelElements som används för för att definiera basfunktionalitet
//som andra klasser sedan kan ärva. LevelElement ska ha properties för (X,Y)-position,
//en char som lagrar vilket tecken en klass ritas ut med (t.ex. kommer “Wall” använda #-tecknet),
//samt en ConsoleColor som lagrar vilken färg tecknet ska ritas med. Den ska dessutom ha en
//publik Draw-metod (utan parametrar), som vi kan anropa för att rita ut ett LevelElement med
//rätt färg och tecken på rätt plats.

enum elementType
{
    Player,
    Wall,
    Rat,
    Snake
}

abstract class LevelElement
{
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
    }

    public void Draw() // Metod för att rita ut objekten som kallar på Draw med deras respektive properties
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

}

