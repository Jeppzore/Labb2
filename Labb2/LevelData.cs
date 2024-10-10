
class LevelData
{

    private static List<LevelElement> _elements = new List<LevelElement>(); // Private field elements av typen List<LevelElement>
    public static List<LevelElement> Elements { get { return _elements; } } //public readonly property “Elements”.

    public Player Player { get; set; }

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
                if (line[x] == '#') // Wall
                {
                    _elements.Add(new Wall(new Position(x, y)));
                }

                if (line[x] == 'r') // Rat
                {          
                    _elements.Add(new Rat(new Position(x, y)));
                }

                if (line[x] == 's') // Snake
                {
                    _elements.Add(new Snake(new Position(x, y)));
                }

                if (line[x] == '@') // Player
                {
                    _elements.Add(new Player(new Position(x, y)));
                }

                if (line[x] == '%') // HealthPotion
                {
                    _elements.Add(new HealthPotion(new Position(x, y)));
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

    public void DrawElementsWithinRange(Player player, int visionRange)
    {
        foreach (var element in Elements)
        {
            double distance = Math.Sqrt(Math.Pow(player.Position.X - element.Position.X, 2) + Math.Pow(player.Position.Y - element.Position.Y, 2));

            if (distance <= visionRange)
            {
                element.Draw();
            }
        }
    }

}        
        
    

