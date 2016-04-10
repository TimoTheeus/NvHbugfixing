using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class CheckBox : GUIGameObject
{
    protected bool check;

    protected string text;
    protected Color textColor;
    protected Vector2 textPosition;
    protected SpriteFont spriteFont;

    public bool Check
    {
        get { return check; }
        set { check = value; }
    }

    public CheckBox(string assetName, string font, int sheetIndex = 0, string id = "", int layer = 0) : base(assetName)
    {
        check = false;

        text = id;
        textColor = Color.Black;

        if (font != "")
            this.spriteFont = GameWorld.AssetLoader.GetFont(font);

        textPosition = new Vector2(5, -(spriteFont.MeasureString(text).Y) - 5);
    }

    public override void HandleInput(InputHelper ih)
    {
        //Determines button state
        if (ih.MouseInBox(this.BoundingBox))
        {
            if (ih.LeftButtonPressed())
                check = !check;
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (check == true)
        {
            sprite.SheetIndex = 1;
        }
        else if (check == false)
        {
            sprite.SheetIndex = 0;
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //base
        base.Draw(gameTime, spriteBatch);

        //Button Text
        spriteBatch.DrawString(spriteFont, text, this.GlobalPosition + textPosition, textColor);
    }

    public override void Reset()
    {
        base.Reset();

        //Reset button state
        this.Position = new Vector2(-1000, -1000);
    }
}