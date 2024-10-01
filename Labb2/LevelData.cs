

//input från kollega: LevelData skall inte ärva från något.
//I LevelData skall det finnas en lista som kommer innehålla Element objekt och via den kommer du åt positioner och annat för varje objekt

class LevelData
{

    private List<LevelElement> _elements = new List<LevelElement>(); // Private field elements av typen List<LevelElement>

    public List<LevelElement> Elements { get { return _elements; } } //public readonly property “Elements”.


    //läser in data från filen man anger vid anrop.
    //Load läser igenom textfilen tecken för tecken, och för varje tecken den hittar som är någon av #, r, eller s,
    //så skapar den en ny instans av den klass som motsvarar tecknet och lägger till en referens till instansen på “elements”-listan.

    //Tänk på att när instansen skapas så måste den även få en (X/Y) position; d.v.s Load behöver alltså hålla reda på
    //vilken rad och kolumn i filen som tecknet hittades på. Den behöver även spara undan startpositionen för spelaren när den stöter på @.

    //När filen är inläst bör det alltså finnas ett objekt i “elements” för varje tecken i filen (exkluderat space/radbyte),
    //och en enkel foreach-loop som anropar .Draw() för varje element i listan bör nu rita upp hela banan på skärmen.
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
                    _elements.Add(new Wall(x, y));
                }

                if (line[x] == 'r') // Kontrollera om tecknet är 'r' (Rat)
                {
                    // Skapa ett nytt LevelElement med objektets motsvarade x och y värde
                    _elements.Add(new Rat(x, y));
                }

                if (line[x] == 's') // Kontrollera om tecknet är 's' (Snake)
                {
                    // Skapa ett nytt LevelElement med objektets motsvarade x och y värde
                    _elements.Add(new Snake(x, y));
                }

                if (line[x] == '@') // Kontrollera om tecknet är '@' (Player)
                {
                    // Skapa ett nytt LevelElement med objektets motsvarade x och y värde
                    _elements.Add(new Player(x, y));
                }
            }

            y++;

            //Läs nästa rad
            line = sr.ReadLine();

            //För varje gång line stöter på ett tecken nedan skall objektets x,y position sparas och läggas till i elements
            //Elements i program.cs skriver sedan ut kartan baserat på samtligas position
            //Console.SetcursorPosition(int, int) sätter positionen
            //forloop i som håller reda på y-position
            //forloop j som håller reda på x-position    

        }

    }
}        
        
    

