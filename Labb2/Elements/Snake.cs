
// Varken spelare, rats eller snakes kan gå igenom väggar eller varandra.
// Snake står still om spelaren är mer än 2 rutor bort,
// annars förflyttar den sig bort från spelaren.
class Snake : Enemy
{

    public Snake(int x, int y) : base(x, y, 's', ConsoleColor.Green)
    {
        Health = 20;
        Name = "Snake";
    }



    public override void Update()
    {
        
    }
}