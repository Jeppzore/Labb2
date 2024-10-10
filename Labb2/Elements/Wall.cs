
class Wall : LevelElement // Ärver från LevelElements (abstract)
{

    public Wall(Position position) : base(position, '#', ConsoleColor.White, elementType.Wall)
    {

    }

}
