// Rat förflyttar sig 1 steg i slumpmässig vald riktning
// (upp, ner, höger eller vänster) varje omgång.
// Varken spelare, rats eller snakes kan gå igenom väggar eller varandra.

class Rat : Enemy
{

    public Rat(Position position) : base(position, 'r', ConsoleColor.Red, elementType.Rat)
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