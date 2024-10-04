
// Varken spelare, rats eller snakes kan gå igenom väggar eller varandra.
// Snake står still om spelaren är mer än 2 rutor bort,
// annars förflyttar den sig bort från spelaren.
class Snake : Enemy
{

    public Snake(Position position) : base(position, 's', ConsoleColor.Green, elementType.Snake)
    {
        Health = 20;
        Name = "Snake";
    }



    public override void Update()
    {
        
    }
}