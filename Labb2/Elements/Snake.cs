
// Snake kan inte gå igenom väggar eller varandra.
// Snake står still om spelaren är mer än 2 rutor bort,
// annars förflyttar den sig bort från spelaren med ett steg i motsatt riktning
class Snake : Enemy
{

    public Snake(Position position) : base(position, 's', ConsoleColor.Green, elementType.Snake)
    {
        Health = 20;
        Name = "Snake";
    }

    public override void Update(List<LevelElement> Elements)
    {

        ClearOldPosition();

        Player player = Elements.OfType<Player>().FirstOrDefault();
        Position newSnakePosition = new Position(this.Position);

        if (player == null)
            return; // Om spelaren inte finns, avsluta metoden

        // Beräkna avstånd till spelaren
        int distanceToPlayerX = Math.Abs(player.Position.X - Position.X);
        int distanceToPlayerY = Math.Abs(player.Position.Y - Position.Y);

        // Om spelaren är mer än 2 rutor bort, stoppa ormen från att röra sig
        if (distanceToPlayerX > 2 || distanceToPlayerY > 2)
        {
            DrawNewPosition();
            return;
        }

        // Rör omren bort från spelaren i X-led
        if (player.Position.X < Position.X)
        {
            newSnakePosition.X = Position.X + 1; // Flytta ormen till höger
        }
        else if (player.Position.X > Position.X)
        {
            newSnakePosition.X = Position.X - 1; // Flytta ormen till höger
        }

        // Rör omren bort från spelaren i Y-led
        if (player.Position.Y < Position.Y) // Om spelaren är över
        {
            newSnakePosition.Y = Position.Y + 1; // Flytta ormen ner
        }
        else if (player.Position.Y > Position.Y) // Om spelaren är under
        {
            newSnakePosition.Y = Position.Y - 1; // Flytta ormen upp
        }

        // Kontrollera om rörelsen är tillåten
        if (IsMoveAllowed(newSnakePosition.X, newSnakePosition.Y, Elements))
        {
            Position = newSnakePosition; // Uppdatera ormens position
        }

        DrawNewPosition();
    }
}