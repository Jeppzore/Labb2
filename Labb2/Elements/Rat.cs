

// Rat förflyttar sig 1 steg i slumpmässig vald riktning
// (upp, ner, höger eller vänster) varje omgång.
// Varken spelare, rats eller snakes kan gå igenom väggar eller varandra.

using System.Threading.Channels;

class Rat : Enemy
{

    public Rat(int x, int y) : base(x, y, 'r', ConsoleColor.Red)
    {
        Health = 10;
        Name = "rat";
    }

    
    // attack = 1d6+3
    // defence = 1d6+1

    public override void Update() //Rörelsemönstret/ allt som fienden ska göra i varje drag
    { 

    }

}