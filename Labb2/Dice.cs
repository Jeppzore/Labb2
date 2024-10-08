
class Dice
{

    private int numberOfDice;
    private int sidesPerDice;
    private int modifier;

    Random random = new Random();
    public Dice(int numberOfDice, int sidesPerDice, int modifier)
    {
        this.numberOfDice = numberOfDice;
        this.sidesPerDice = sidesPerDice;
        this.modifier = modifier;
    }

    public int ThrowDice()
    {
        int diceRoll = 0;
        int totalRoll = 0;

        for (int i = 0; i < numberOfDice; i++)
        {
            diceRoll = random.Next(1, sidesPerDice + 1);
            totalRoll += diceRoll; // Lägg till resultatet till totalen
        }
        // Om 2 tärningar kastas blir resultatet värdet av två unika diceRolls + en modifier
        totalRoll += modifier;
        return totalRoll;
    }

    public override string ToString()
    {
        return $"{numberOfDice}d{sidesPerDice}+{modifier}";
    }

}

