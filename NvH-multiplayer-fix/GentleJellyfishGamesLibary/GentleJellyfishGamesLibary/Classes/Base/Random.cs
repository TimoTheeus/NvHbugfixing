using System;

//Class to get random numbers.
public class R
{
    static protected Random r;

    public R()
    {
        r = new Random();
    }
    //Return true or false.
    static public bool Coin()
    {
        return r.Next(2) == 1;
    }
    //Return random number from 1 to n.
    static public int Dice(int n)
    {
        return r.Next(n) + 1;
    }
    //Return random int.
    static public int Next()
    {
        return r.Next();
    }
    //Return random double.
    static public double NextDouble()
    {
        return r.NextDouble();
    }
    //Return random float.
    static public float NextFloat()
    {
        return (float)r.NextDouble();
    }
}

