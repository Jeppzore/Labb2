


//Abstrakt basklass LevelElements som används för för att definiera basfunktionalitet
//som andra klasser sedan kan ärva. LevelElement ska ha properties för (X,Y)-position,
//en char som lagrar vilket tecken en klass ritas ut med (t.ex. kommer “Wall” använda #-tecknet),
//samt en ConsoleColor som lagrar vilken färg tecknet ska ritas med. Den ska dessutom ha en
//publik Draw-metod (utan parametrar), som vi kan anropa för att rita ut ett LevelElement med
//rätt färg och tecken på rätt plats.


abstract class LevelElement 
{
    public int X { get; set; }
    public int Y { get; set; }
    protected char Icon { get; set; }
    protected ConsoleColor CharacterColor { get; set; }

    protected LevelElement(int x, int y, char icon, ConsoleColor consoleColor)
    {
        X = x;
        Y = y;
        Icon = icon;
        CharacterColor = consoleColor;
    }

    private int Position // Property för position
    {
        get; set;
    }


    public void Draw() // Metod för att rita ut objekten som kallar på Draw med deras respektive properties
    {
        Console.SetCursorPosition(X,Y);
        Console.ForegroundColor = CharacterColor;
        Console.WriteLine(Icon);
        Console.ResetColor();

    }
    
}

