


//Abstrakt basklass LevelElements som används för för att definiera basfunktionalitet
//som andra klasser sedan kan ärva. LevelElement ska ha properties för (X,Y)-position,
//en char som lagrar vilket tecken en klass ritas ut med (t.ex. kommer “Wall” använda #-tecknet),
//samt en ConsoleColor som lagrar vilken färg tecknet ska ritas med. Den ska dessutom ha en
//publik Draw-metod (utan parametrar), som vi kan anropa för att rita ut ett LevelElement med
//rätt färg och tecken på rätt plats.

abstract class LevelElement 
{

    char drawClass; //Placeholder skall lagra tecken en klass ritas med

    private int Position // Property för position
    {
        get; set;
    }


    public void Draw() // Metod för att rita ut 
    {

    }
    
}

