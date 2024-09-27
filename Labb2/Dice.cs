

// Dice-objekt ska ha en publik Throw() metod som returnerar ett heltal
// med den poäng man får när man slår med tärningarna enligt objektets
// konfiguration. Varje anrop motsvarar alltså ett nytt kast med tärningarna.




class Dice
{

    public int numberOfDice;
    public int sidesPerDice;
    public int modifier;

    // konstruktor som tar 3 parametrar.
    // Genom att skapa nya instans av denna kommer man kunna skapa olika
    // uppsättningar av tärningar t.ex “3d6+2”, d.v.s slag med 3 stycken
    // 6-sidiga tärningar, där man tar resultatet och sedan plussar på 2,
    // för att få en total poäng.
    public int ThrowDice(int numberOfDice, int sidesPerDice, int modifier)
    {
        int result = (numberOfDice * sidesPerDice) + modifier;
        return result;
    }


    // Gör även en override av Dice.ToString(), så att man när man skriver ut
    // ett Dice-objekt får en sträng som beskriver objektets konfiguration.
    // t.ex: “3d6+2”.


}

