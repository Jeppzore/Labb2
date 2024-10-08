﻿

//Klassen “Enemy” ärver också av LevelElement, och lägger till funktionalitet som är specifik för fiender.
//Även Enemy är abstrakt, då vi inte vill att man ska kunna instansiera ospecifika “fiender”.
//Alla riktiga fiender (i labben rat & snake, men om man vill och har tid får man lägga till fler typer av fiender) ärver av denna klass.

//Enemy ska ha properties för namn (t.ex snake/rat), hälsa (HP), samt AttackDice och DefenceDice av typen Dice (mer om detta längre ner).
//Den ska även ha en abstrakt Update-metod, som alltså inte implementeras i denna klass, men som kräver att alla som ärver av klassen implementerar den.
//Vi vill alltså kunna anropa Update-metoden på alla fiender och sedan sköter de olika subklasserna hur de uppdateras (till exempel olika förflyttningsmönster).

using System;

abstract class Enemy : LevelElement
{
    public string Name { get; set; }
    public int Health { get; set; } // Property HP
    public Dice AttackDice { get; set; } // Property AttackDice
    public Dice DefenceDice { get; set; } // Property DefenceDice 
    
    protected Enemy(Position position, char icon, ConsoleColor consoleColor, elementType type) : base(position, icon, consoleColor, type)
    {

    }
    protected bool IsMoveAllowed(int newX, int newY, List<LevelElement> elements)
    {
        foreach (var element in elements)
        {
            if (element.Position.X == newX && element.Position.Y == newY)
            {              
                return false; // kollision med ett objekt
            }
        }
        return true; // Ingen kollision, fltyten är giltig
    }

    protected void ClearOldPosition()
    {
        Console.SetCursorPosition(Position.X, Position.Y + 4);
        Console.Write(' ');
    }

    protected void DrawNewPosition()
    {
        Console.SetCursorPosition(Position.X, Position.Y + 4);
        Console.ForegroundColor = CharacterColor;
        Console.Write(Icon);
        Console.ResetColor();
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            Health = 0; // Ser till att health aldrig blir mindre än 0

            if(this.Type == elementType.Rat)
            {
                Console.SetCursorPosition(0, 26);
                Console.WriteLine("You received 5 exp".PadRight(Console.BufferWidth));
            }

            if (this.Type == elementType.Snake)
            {
                Console.SetCursorPosition(0, 26);
                Console.WriteLine("You received 10 exp".PadRight(Console.BufferWidth));

            }

            LevelData.Elements.Remove(this);
            Clear();           
        }                   
    }


    public abstract void Update(List<LevelElement> elements); // Abstract Update-metod som inte implementeras här men som krävs implementation av alla klasser som ärver av Enemy

}
