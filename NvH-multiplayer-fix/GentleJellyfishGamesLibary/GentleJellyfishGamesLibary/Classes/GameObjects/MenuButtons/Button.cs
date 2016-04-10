using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

//Make button
public class Button : GUIGameObject
{
    protected bool pressed;

    //Button Text
    protected string text;
    protected SpriteFont spriteFont, smallFont, bigFont;
    protected Color textColor;
    protected Vector2 textPosition;
    protected bool selected;
    protected bool selectedSet;

    public Vector2 TextPosition
    {
        get { return textPosition; }
        set { textPosition = value; }
    }

    public Color TextColor
    {
        get { return textColor; }
        set { textColor = value; }
    }
    
    public string Text
    {
        get { return text; }
        set { text = value; }
    }

    public bool Selected
    {
        get { return selected; }
        set { selected = value; selectedSet = true; }
    }

    public bool Pressed { get { return pressed; } }

    public Button(string assetName, string font, string smallFont, int sheetIndex = 0, string id = "", int layer = 0) : base(assetName, sheetIndex, id, layer)
    {
        //Button State
        pressed = false;

        //Button Origin
        this.Origin = new Vector2(sprite.Width / 2, sprite.Height / 2);

        //Button Text
        text = id;

        if (font != "")
            this.bigFont = GameWorld.AssetLoader.GetFont(font);

        if (smallFont != "")
            this.smallFont = GameWorld.AssetLoader.GetFont(smallFont);

        if (font != "")
            this.spriteFont = GameWorld.AssetLoader.GetFont(font);

        textColor = Color.Black;
        if(text!=""&&font!="")
        textPosition = new Vector2(-(spriteFont.MeasureString(text).X / 2), -(spriteFont.MeasureString(text).Y / 2));
    }

    public override void HandleInput(InputHelper ih)
    {
        //Determines button state
        if (ih.MouseInBox(BoundingBox))
        {
            selected = true;
            selectedSet = false;
        }
        else if (!selectedSet)
            selected = false;

        if (selected)
        {
            if ((ih.LeftButtonReleased() && ih.MouseInBox(BoundingBox)) || ih.KeyPressed(Keys.Enter))
            {
                pressed = true;
                textColor = Color.Purple;
            }

            else
            {
                pressed = false;
            }

            if ((ih.IsLeftMouseButtonDown() && ih.MouseInBox(BoundingBox)) || ih.IsKeyDown(Keys.Enter))
                sprite.SheetIndex = 1;
            else
            {
                sprite.SheetIndex = 2;
                textColor = Color.White;
                spriteFont = smallFont;
                if(text!="")
                textPosition = new Vector2(-(smallFont.MeasureString(text).X / 2), -(smallFont.MeasureString(text).Y / 2));
                sprite.Scale = 0.95f;
            }
        }
        else
        {
            sprite.SheetIndex = 0;
            textColor = Color.Black;
            sprite.Scale = 1.0f;
            spriteFont = bigFont;
            if (text != "")
                textPosition = new Vector2(-(bigFont.MeasureString(text).X / 2), -(bigFont.MeasureString(text).Y / 2));
            pressed = false;
        }
    }



    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //base
        base.Draw(gameTime, spriteBatch);

        //Button Text
        if(text!="")
        spriteBatch.DrawString(spriteFont, text, this.GlobalPosition + textPosition, textColor);
    }

    public override void Reset()
    {
        base.Reset();

        //Reset button state
        pressed = false;
    }
}
