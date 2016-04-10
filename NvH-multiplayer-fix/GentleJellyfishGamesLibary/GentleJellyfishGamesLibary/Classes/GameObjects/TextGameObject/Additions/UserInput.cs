using Microsoft.Xna.Framework.Input;
using System;
//Class used to draw a string that the user typed himself.
public class UserInput : TextGameObject
{
    const int maxSize = 100;
    protected InputHelper ihTemp;
    public bool Locked = false;
    public bool AutoReset = false;

    public UserInput() :base("font")
    {
        text = "";
        ihTemp = new InputHelper();
    }

    //Get the keypresses and make a string from them in the inputhandler, then draw it on screen by putting it in this object.
    public override void HandleInput(InputHelper ih)
    {
        ihTemp = ih;
        if (!Locked)
        {
            ih.GetKeysPressedText();
            if (ih.InputString.Length <= maxSize)
            {
                
                Console.WriteLine(text);
                text = ih.InputString;
            }
            else
            {
                ih.InputString = ih.InputString.Substring(0, maxSize);
            }
            if (ih.KeyPressed(Keys.Enter))
            {
                Locked = true;
                ih.ResetInput();
            }
        }
        if((Locked || text.Length >= maxSize) && AutoReset)
        {
            Reset();
        }

        //Unlock used for testing.
        if (ih.KeyPressed(Keys.Tab))
            Reset();
    }
    public override void Reset()
    {
        Locked = false;
        text = "";
        ihTemp.ResetInput();
    }
}

