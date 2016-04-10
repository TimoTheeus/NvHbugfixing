using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

public class DropMenu : GUIGameObject
{
    protected bool hover;

    protected SpriteGameObject[] menuItems;
    protected TextGameObject[] options;
    protected GameObjectList items;

    //Text
    protected string text;
    protected Color textColor;
    protected Vector2 textPosition;
    protected SpriteFont spriteFont;

    public TextGameObject[] Options
    {
        get { return options; }
        set { options = value; }
    }

    public DropMenu(string assetName, string font, int numOptions, int sheetIndex = 0, string id = "", int layer = 0) : base(assetName)
    {
        menuItems = new SpriteGameObject[numOptions + 1];
        options = new TextGameObject[numOptions + 1];
        items = new GameObjectList();

        //Menu items
        menuItems[0] = new SpriteGameObject(assetName);

        for (int i = 1; i <= numOptions; i++)
        {
            menuItems[i] = new SpriteGameObject(assetName, 1);
            menuItems[i].Position = this.Position + new Vector2(0, (i * menuItems[i].Height));
            items.Add(menuItems[i]);
        }

        for (int i = 0; i <= numOptions; i++)
        {
            options[i] = new TextGameObject("font", 2);
            options[i].Text = "";
            options[i].Position = this.Position + new Vector2(0, (i * menuItems[i].Height));
            options[i].Visible = false;
            items.Add(options[i]);
        }

        //Button Text
        text = id;

        if (font != "")
            this.spriteFont = GameWorld.AssetLoader.GetFont(font);

        textColor = Color.Black;
        textPosition = new Vector2(5, -(spriteFont.MeasureString(text).Y) - 5);
    }

    public override void HandleInput(InputHelper ih)
    {
        base.HandleInput(ih);
        items.HandleInput(ih);

        if (ih.MouseInBox(this.BoundingBox) || (menuItems[1].Visible && ih.MouseInBox(new Rectangle((int)this.Position.X, (int)this.Position.Y, this.Width, ((options.Length + 1) * this.Height)))))
        {
            for (int i = 0; i < options.Length; i++)
            {
                menuItems[i].Visible = true;
            }

            for (int i = 1; i < options.Length; i++)
            {
                options[i].Visible = true;
            }

            for (int i = 1; i < options.Length; i++)
            {
                if (ih.MouseInBox(new Rectangle((int)this.Position.X, ((int)this.Position.Y + (i * sprite.Height)), sprite.Width, (sprite.Height))))
                {
                    options[i].Color = Color.White;
                }
                else
                {
                    options[i].Color = Color.Black;
                }
            }


            if (ih.LeftButtonPressed())
            {
                for (int i = 1; i < options.Length; i++)
                {
                    if (ih.MouseInBox(new Rectangle(((int)this.Position.X), ((int)this.PositionY + (sprite.Height * i)), sprite.Width, sprite.Height)))
                    {
                        options[0].Text = options[i].Text;
                    }
                }
            }
        }
        else
        {
            for (int i = 1; i < options.Length; i++)
            {
                menuItems[i].Visible = false;
            }

            for (int i = 1; i < options.Length; i++)
            {
                options[i].Visible = false;
            }
        }
    }

    public override void Reset()
    {
        this.Position = new Vector2(-1000, -1000);

        for (int i = 0; i < options.Length; i++)
        {
            menuItems[i].Visible = false;
        }

        items.Reset();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        items.Update(gameTime);

        options[0].Visible = true;

        for (int i = 0; i < options.Length; i++)
        {
            menuItems[i].Position = this.Position + new Vector2(0, (i * menuItems[i].Height));
        }

        for (int i = 0; i < options.Length; i++)
        {
            options[i].Position = this.Position + new Vector2(10, (i * menuItems[i].Height));
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        items.Draw(gameTime, spriteBatch);

        //Draw Text
        spriteBatch.DrawString(spriteFont, text, this.GlobalPosition + textPosition, textColor);
    }
}

