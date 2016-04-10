using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

//Class to help with any input from the player, like keypresses and mouseclicks.
public class InputHelper
{
    protected MouseState currMState;
    protected MouseState prevMState;
    protected KeyboardState currKBState;
    protected KeyboardState prevKBState;
    protected string inputString;
    protected int backCounter;
    protected int backVar = 20;

    //Get the string from the user input.
    public string InputString
    {
        get { return inputString; }
        set { inputString = value; }
    }

    //Get the position of the mouse on the screen (corrected to the scale of the screen).
    public Vector2 MousePosition { get { return new Vector2(currMState.X, currMState.Y) / GameSettings.Scale; } }

    public InputHelper()
    {
        inputString = "";
    }

    //Update the keyboard and mouse states.
    public void Update()
    {
        prevMState = currMState;
        prevKBState = currKBState;
        currMState = Mouse.GetState();
        currKBState = Keyboard.GetState();
    }
    //Bool to check if the left mouse button is down.
    public bool IsLeftMouseButtonDown()
    {
        return currMState.LeftButton == ButtonState.Pressed;
    }
    //Bool to check if the left mouse button is pressed.
    public bool LeftButtonPressed()
    {
        return currMState.LeftButton == ButtonState.Pressed && prevMState.LeftButton == ButtonState.Released;
    }
    //Bool to check if the left mouse button is pressed.
    public bool RightButtonPressed()
    {
        return currMState.RightButton == ButtonState.Pressed && prevMState.RightButton == ButtonState.Released;
    }
    //Bool to check if the left mouse button is pressed.
    public bool LeftButtonReleased()
    {
        return prevMState.LeftButton == ButtonState.Pressed && currMState.LeftButton == ButtonState.Released;
    }
    //Check if a key is pressed.
    public bool KeyPressed(Keys k)
    {
        return currKBState.IsKeyDown(k) && prevKBState.IsKeyUp(k);
    }
    //Check if either key given is pressed.
    public bool KeyPressed(Keys k1, Keys k2)
    {
        return KeyPressed(k1) || KeyPressed(k2);
    }
    //Check if both keys are pressed.
    public bool KeysBothPressed(Keys k1, Keys k2)
    {
        return (IsKeyDown(k1) && KeyPressed(k2)) || (IsKeyDown(k2) && KeyPressed(k1));
    }
    //Check if a key is down.
    public bool IsKeyDown(Keys k)
    {
        return currKBState.IsKeyDown(k);
    }
    //Check if either key given is down.
    public bool IsKeyDown(Keys k1, Keys k2)
    {
        return IsKeyDown(k1) || IsKeyDown(k2);
    }
    //Check if the mouse is inside a given Rectangle.
    public bool MouseInBox(Rectangle box)
    {
        return box.Contains(new Point((int)(MousePosition.X+GameWorld.Camera.Pos.X), (int)(MousePosition.Y + GameWorld.Camera.Pos.Y)));
    }
    //Return a list of keys that have been pressed.
    public List<Keys> GetPressedKeys()
    {
        List<Keys> keys = new List<Keys>();
        foreach (Keys k in currKBState.GetPressedKeys())
            if (prevKBState.IsKeyUp(k))
                keys.Add(k);
        return keys;
    }
    //Checking all keypresses, used for getting the players name.
    public void GetKeysPressedText()
    {
        //Remove multiple chars if the back keys is pressed continuously.
        if (IsKeyDown(Keys.Back))
        {
            backCounter++;
            if (backCounter == backVar && inputString.Length > 0)
            {
                inputString = inputString.Remove(inputString.Length - 1, 1);
                backCounter = 0;
                if (backVar > 2)
                    backVar /= 2;
            }
        }
        else
        {
            backVar = 20;
            backCounter = 0;
        }
        //For all keys that have been pressed, add the corresponding char.
        foreach (Keys k in GetPressedKeys())
            ChangeInputText(k);
    }
    //Method to get the char corresponding to a keypress.
    public void ChangeInputText(Keys k)
    {
        switch (k)
        {
            case Keys.Back:
                if (inputString.Length > 0)
                inputString = inputString.Remove(inputString.Length - 1, 1);
                break;
            case Keys.Space:
                inputString += " ";
                break;
            case Keys.OemPeriod:
                inputString += ".";
                break;
            default:
                break;
        }
        string temp;
        temp = k.ToString();
        Log.Write(LogType.INFO, "Input key: " + temp);
        //If the keyname is only one long, it is a letter, so use it.
        if (temp.Length == 1)
            inputString += temp;
        //If the keyname contains D or NumPad, it is a number, remove the D/Numpad part and add the number.
        else if (temp.Length == 2 && temp.Contains("D"))
        {
            temp = temp.Remove(0, 1);
            inputString += temp;
        }
        else if (temp.Length == 7 && temp.Contains("NumPad"))
        {
            temp = temp.Remove(0, 6);
            inputString += temp;
        }
        
    }
    //Resets the user input.
    public void ResetInput()
    {
        inputString = "";
    }
}





