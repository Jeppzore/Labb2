
class LevelData
{

    private static List<LevelElement> _elements = new List<LevelElement>(); // Private field elements av typen List<LevelElement>

    public static List<LevelElement> Elements { get { return _elements; } } //public readonly property “Elements”.

    public void Load(string fileName)
    {
        _elements = new List<LevelElement>();

        StreamReader sr = new StreamReader(fileName);

        //Read the first line of text
        string line = sr.ReadLine();

        int y = 0; // Spårar radnummer (Y-koordinaten)

        // Läs filen rad för rad
        while (line != null)
        {

            for (int x = 0; x < line.Length; x++)
            {
                if (line[x] == '#') // Kontrollera om tecknet är '#' (Wall)
                {
                    // Skapa ett nytt LevelElement med objektets motsvarade x och y värde
                    _elements.Add(new Wall(new Position(x, y)));
                }

                if (line[x] == 'r') // Kontrollera om tecknet är 'r' (Rat)
                {
                    // Skapa ett nytt LevelElement med objektets motsvarade x och y värde
                    _elements.Add(new Rat(new Position(x, y)));
                }

                if (line[x] == 's') // Kontrollera om tecknet är 's' (Snake)
                {
                    // Skapa ett nytt LevelElement med objektets motsvarade x och y värde
                    _elements.Add(new Snake(new Position(x, y)));
                }

                if (line[x] == '@') // kontrollera om tecknet är '@' (player)
                {
                    // skapa ett nytt levelelement med objektets motsvarade x och y värde
                    _elements.Add(new Player(new Position(x, y)));
                }
            }

            y++;

            //Läs nästa rad
            line = sr.ReadLine();

        }
   
    }

    public LevelElement GetLevelElementAt(Position position)
    {

        foreach (LevelElement element in Elements)
        {
            if (element.Position.Equals(position))
            {
                return element;
            }
        }
        return null;
    }

}        
        
    

