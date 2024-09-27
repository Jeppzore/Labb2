

//input från kollega: LevelData skall inte ärva från något.
//I LevelData skall det finnas en lista som kommer innehålla Element objekt och via den kommer du åt positioner och annat för varje objekt

class LevelData
{

    private List<LevelElement> elements = new List<LevelElement>(); // Private field elements av typen List<LevelElement>

    public List<LevelElement> Elements { get { return elements; } } //public readonly property “Elements”.


    //läser in data från filen man anger vid anrop.
    //Load läser igenom textfilen tecken för tecken, och för varje tecken den hittar som är någon av #, r, eller s,
    //så skapar den en ny instans av den klass som motsvarar tecknet och lägger till en referens till instansen på “elements”-listan.

    //Tänk på att när instansen skapas så måste den även få en (X/Y) position; d.v.s Load behöver alltså hålla reda på
    //vilken rad och kolumn i filen som tecknet hittades på. Den behöver även spara undan startpositionen för spelaren när den stöter på @.

    //När filen är inläst bör det alltså finnas ett objekt i “elements” för varje tecken i filen (exkluderat space/radbyte),
    //och en enkel foreach-loop som anropar .Draw() för varje element i listan bör nu rita upp hela banan på skärmen.
    public void Load(string fileName)
    {
        try
        {
            StreamReader sr = new StreamReader(fileName);

            //Read the first line of text
            fileName = sr.ReadLine();
            //Continue to read until you reach end of file
            while (fileName != null)
            {
                //write the line to console window
                Console.WriteLine(fileName);
                //Read the next line
                fileName = sr.ReadLine();
            }
            //close the file
            sr.Close();
            Console.ReadLine();
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        finally
        {
            Console.WriteLine("Executing finally block.");
        }


        foreach (var element in Elements)
        {
            // För varje tecken #,r eller s skapa en ny instans av motsvarande klass och lägg till på elements-listan
        }

        
    }

}

