
//Varken spelare, rats eller snakes kan gå igenom väggar eller varandra.


// Ärver av LevelElement för att kunna hålla reda på om spelaren
// krockar med vägg/enemy t.ex.

// För att få en effekt av “utforskande” i spelet begränsar vi spelarens
// synfält till att bara visa objekt inom en radie av 5 tecken
// (men ni kan också prova med andra radier);
// Väggarna försvinner dock aldrig när man väl sett dem,
// men fienderna syns inte så fort de kommer utanför radien.

//Avståndet mellan två punkter i 2D kan enkelt beräknas
//med hjälp av pythagoras sats.

using Elasticsearch.Net;
using System.Xml.Linq;

class Player : LevelElement
{

    public int Health { get; set; }
    public string Name { get; set; }


    public Player(int x, int y) : base(x, y, '@', ConsoleColor.Yellow)
    {
        Health = 10;
        Name = "Player";
    }

    // attack = 2d6+2
    // defence = 2d6+0


}