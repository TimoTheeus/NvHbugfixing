using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

//Make slider button, for values
public class SliderButton : GUIGameObject
{
    protected SpriteSheet back, front;
    protected bool dragging;
    protected int leftMargin, rightMargin;

    protected Vector2 frontPosition, backPosition;

    protected string text;
    protected Color textColor;
    protected Vector2 textPosition;
    protected SpriteFont spriteFont;

    protected InputHelper inputHelper;

    public float Value
    {
        get
        {
            return ((frontPosition.X - backPosition.X - leftMargin) / (back.Width - leftMargin - rightMargin - front.Width));
        }
        set
        {
            frontPosition = new Vector2((value * back.Width) - (value * front.Width) - (value * (2 * rightMargin)), frontPosition.Y);
        }
    }

    public SliderButton(string sliderback, string sliderfront, int layer = 0, string id = "") : base(sliderback)
    {
        back = new SpriteSheet("sliderBack");

        front = new SpriteSheet("sliderFront");
        frontPosition = Vector2.Zero;
        backPosition = this.Position;

        leftMargin = 0;
        rightMargin = 5;

        dragging = false;

        text = id;
        textColor = Color.Black;
        spriteFont = GameWorld.AssetLoader.GetFont("buttonFont");
        textPosition = new Vector2(5, -(spriteFont.MeasureString(text).Y) - 5);
    }
    
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (inputHelper.IsLeftMouseButtonDown())
        {
            //Determine whether 
            if (inputHelper.MouseInBox(BoundingBox))
            {
                float newXPos = MathHelper.Clamp(inputHelper.MousePosition.X - (front.Width / 2), this.Position.X + leftMargin, this.Position.X + back.Width - front.Width - rightMargin);
                frontPosition = new Vector2(newXPos - this.GlobalPosition.X, frontPosition.Y);
            }
        }
    }

    public override void Reset()
    {
        this.Position = new Vector2(-1000, -1000);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //base
        base.Draw(gameTime, spriteBatch);

        //Slider
        front.Draw(spriteBatch, this.GlobalPosition + frontPosition, Vector2.Zero);

        //SliderButton Text
        spriteBatch.DrawString(spriteFont, text, this.GlobalPosition + textPosition, textColor);
    }   
}