
class Dice
{

    public int numberOfDice;
    public int sidesPerDice;
    public int modifier;

    public int ThrowDice(int numberOfDice, int sidesPerDice, int modifier)
    {
        int result = (numberOfDice * sidesPerDice) + modifier;
        return result;
    }

    public override string ToString()
    {
        return $"{numberOfDice}"
    }

    // Gör även en override av Dice.ToString(), så att man när man skriver ut
    // ett Dice-objekt får en sträng som beskriver objektets konfiguration.
    // t.ex: “3d6+2”.

}

