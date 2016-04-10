using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Draws string object
public class TextGameObject : GameObject
{
    protected SpriteFont spriteFont;
    protected Color color;
    protected string text;

    public Color Color
    {
        get { return color; }
        set { color = value; }
    }

    public string Text
    {
        get { return text; }
        set { text = value; }
    }

    public Vector2 Size { get { return spriteFont.MeasureString(text); } }

    public TextGameObject(string assetname, int layer = 0, string id = "")
        : base(id, layer)
    {
        if (assetname != "")
            this.spriteFont = GameWorld.AssetLoader.GetFont(assetname);

        color = Color.Black;
    }

    public override void Reset()
    {
        //this.Position = new Vector2(-1000, -1000);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (visible && text != null)
            spriteBatch.DrawString(spriteFont, text, this.GlobalPosition, color);
    }
}

