

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
            string line = sr.ReadLine();
            //Continue to read until you reach end of file
            while (line != null)
            {
                //För varje gång line stöter på ett tecken nedan skall objektets x,y position sparas och läggas till i elements
                //Elements i program.cs skriver sedan ut kartan baserat på samtligas position
                //Console.SetcursorPosition(int, int) sätter positionen
                //forloop i som håller reda på y-position
                //forloop j som håller reda på x-position


                //int playerPosition = line.IndexOf('@');
                //Console.WriteLine(playerPosition);
                //Player player = new Player(x, y);

                if (line.)
                

                if (line.Contains('r'))
                {
                    //Rat rat = new Rat(x, y);
                    //elements.Add(rat);

                }

                if (line.Contains('s'))
                {
                    //Snake snake = new Snake(x, y);

                }

                //write the line to console window
                
                //Read the next line
                line = sr.ReadLine();
               
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


        //foreach (var element in Elements)
        //{
            // För varje tecken #,r eller s skapa en ny instans av motsvarande klass och lägg till på elements-listan
        }

        
    }

