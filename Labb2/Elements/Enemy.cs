

//Klassen “Enemy” ärver också av LevelElement, och lägger till funktionalitet som är specifik för fiender.
//Även Enemy är abstrakt, då vi inte vill att man ska kunna instansiera ospecifika “fiender”.
//Alla riktiga fiender (i labben rat & snake, men om man vill och har tid får man lägga till fler typer av fiender) ärver av denna klass.

//Enemy ska ha properties för namn (t.ex snake/rat), hälsa (HP), samt AttackDice och DefenceDice av typen Dice (mer om detta längre ner).
//Den ska även ha en abstrakt Update-metod, som alltså inte implementeras i denna klass, men som kräver att alla som ärver av klassen implementerar den.
//Vi vill alltså kunna anropa Update-metoden på alla fiender och sedan sköter de olika subklasserna hur de uppdateras (till exempel olika förflyttningsmönster).
abstract class Enemy : LevelElement
{

    public string Snake { get; set; } // Property Snake
    public string Rat { get; set; } // Property Rat

    public int HP { get; set; } // Property HP
    public int AttackDice { get; set; } // Property AttackDice
    public int DefenceDice { get; set; } // Property DefenceDice

    public abstract void Update(); // Abstract Update-metod som inte implementeras här men som krävs implementation av alla klasser som ärver av Enemy



}
