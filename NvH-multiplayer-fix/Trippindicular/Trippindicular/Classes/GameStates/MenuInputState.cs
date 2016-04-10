using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class MenuInputState : GameObjectList
{
    protected int selector;
    protected GameObjectList buttons;

    public MenuInputState()
    {
        selector = -1;
        buttons = new GameObjectList();
    }

    public override void HandleInput(InputHelper ih)
    {
        base.HandleInput(ih);
        if (buttons.Objects.Count > 0)
        {
            buttons.HandleInput(ih);
            for (int i = 0; i < buttons.Objects.Count; i++)
            {
                GameObject obj = buttons.Objects[i];
                if (obj is Button && i != selector)
                {
                    Button button = obj as Button;
                    if (button.Selected)
                        selector = -1;
                }
            }
            if (ih.KeyPressed(Keys.Up, Keys.W))
            {
                Button b;
                if (selector != -1)
                {
                    b = buttons.Objects[selector] as Button;
                    b.Selected = false;
                }
                selector--;
                if (selector < 0)
                    selector = buttons.Objects.Count - 1;
                b = buttons.Objects[selector] as Button;
                b.Selected = true;
            }
            else if (ih.KeyPressed(Keys.Down, Keys.S))
            {
                Button b;
                if (selector != -1)
                {
                    b = buttons.Objects[selector] as Button;
                    b.Selected = false;
                }
                selector++;
                if (selector >= buttons.Objects.Count)
                    selector = 0;
                b = buttons.Objects[selector] as Button;
                b.Selected = true;
            }
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        buttons.Update(gameTime);
    }

    public override void Reset()
    {
        base.Reset();
        selector = -1;
        buttons.Reset();
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        buttons.Draw(gameTime, spriteBatch);
    }
}

