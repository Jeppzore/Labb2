

//Klassen “Wall” ärver av LevelElement, och behöver egentligen ingen
//egen kod förutom att hårdkoda färgen och tecknet för vägg (en grå hashtag).


class Wall : LevelElement // Ärver från LevelElements (abstract)
{

    public Wall(int x, int y) : base(x, y, '#', ConsoleColor.White)
    {

    }

    public char wall = '#';
    //Lägg till färg på wall?

}
