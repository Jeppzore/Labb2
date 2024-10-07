
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


    public override void Update(List<LevelElement> elements)
    {

        ClearOldPosition();

        Player player = elements.OfType<Player>().FirstOrDefault();

        if (player == null)
            return; // Om spelaren inte finns, avsluta metoden

        // Beräkna avstånd till spelaren
        int distanceToPlayerX = Math.Abs(player.Position.X - Position.X);
        int distanceToPlayerY = Math.Abs(player.Position.Y - Position.Y);
        
        // Om spelaren är mer än 2 rutor bort, stoppa ormen från att fltyta sig
        if (distanceToPlayerX > 2 || distanceToPlayerY > 2)
        {
            //Rita ormen på sin nuvarande position om den inte rör sig
            DrawNewPosition();
            return;
        }

        // Annars flytta ormen bort från spelaren
        int newSnakePositionX = Position.X;
        int newSnakePositionY = Position.Y;

        // Rör omren bort från spelaren i X-led
        if(player.Position.X < Position.X)
        {
            newSnakePositionX = Position.X + 1; // Flytta ormen till höger
        }
        else if (player.Position.X > Position.X)
        {
            newSnakePositionX = Position.X - 1; // Flytta ormen till höger
        }

        // Rör omren bort från spelaren i Y-led
        if (player.Position.Y < Position.Y) // Spelaren är ovanför
        {
            newSnakePositionX = Position.Y + 1; // Flytta ormen ner
        }
        else if (player.Position.Y > Position.Y) // Spelaren är under
        {
            newSnakePositionX = Position.Y - 1; // Flytta ormen upp
        }

        if (IsMoveAllowed(newSnakePositionX, newSnakePositionY, elements))
        {
            int newSnakePositionX = Position.X;
            int newSnakePositionY = Position.Y;

        }

        DrawNewPosition();
    }
}