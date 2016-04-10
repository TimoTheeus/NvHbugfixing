using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Class used to load single sprites from larger spritesheets, also used for basic sprite manipulation, like mirror, scale and rotation.
public class SpriteSheet
{
    protected Texture2D sprite;
    protected int sheetIndex;
    protected int sheetColumns;
    protected int sheetRows;
    protected bool mirror;
    protected float scale;
    protected float rotation;
    protected Color color;

    //Readonly properties.
    public int Width { get { return sprite.Width / sheetColumns; } }
    public int Height { get { return sprite.Height / sheetRows; } }
    public int NumberSprites { get { return sheetColumns * sheetRows; } }
    public Vector2 Center { get { return new Vector2(Width, Height) / 2; } }

    //Other properties.
    public float Scale
    {
        get { return scale; }
        set { scale = value; }
    }
    public float Rotation
    {
        get { return rotation; }
        set { rotation = value; }
    }
    public bool Mirror
    {
        get { return mirror; }
        set { mirror = value; }
    }
    public int SheetIndex
    {
        get { return sheetIndex; }
        set
        {
            if (value < sheetColumns * sheetRows && value >= 0)
                sheetIndex = value;
        }
    }
    public Color Color
    {
        get { return color; }
        set { color = value; }
    }
    //Load the texture and then set the right amount of rows and columns.    
    public SpriteSheet(string assetName, int sheetIndex = 0)
    {
        color = Color.White;
        sprite = GameWorld.AssetLoader.GetSprite(assetName);
        this.sheetIndex = sheetIndex;
        sheetColumns = 1;
        sheetRows = 1;
        scale = 1.0f;
        rotation = 0.0f;

        string[] assetSplit = GameWorld.AssetLoader.GetPath(assetName).Split('@');
        if (assetSplit.Length <= 1)
            return;

        string sheetNrData = assetSplit[assetSplit.Length - 1];
        string[] colrow = sheetNrData.Split('x');
        sheetColumns = int.Parse(colrow[0]);
        if (colrow.Length == 2)
            sheetRows = int.Parse(colrow[1]);
    }
    //Draw the sprite that it should draw according to the sheetindex.
    public void Draw(SpriteBatch spriteBatch, Vector2 position, Vector2 origin)
    {
        int columnIndex = sheetIndex % sheetColumns;
        int rowIndex = sheetIndex / sheetColumns % sheetRows;
        Rectangle spritePart = new Rectangle(columnIndex * Width, rowIndex * Height, Width, Height);
        SpriteEffects spriteEffects = SpriteEffects.None;
        if (mirror)
            spriteEffects = SpriteEffects.FlipHorizontally;

        spriteBatch.Draw(sprite, position, spritePart, color, rotation, origin, scale, spriteEffects, 0);
    }
    //Get the color of a certain pixel at the right position in the spritesheet. (Used for pixel perfect collisions.)
    public Color GetPixelColor(int x, int y)
    {
        int column_index = sheetIndex % sheetColumns;
        int row_index = sheetIndex / sheetColumns % sheetRows;
        Rectangle sourceRectangle = new Rectangle(column_index * Width + x, row_index * Height + y, 1, 1);
        Color[] retrievedColor = new Color[1];
        sprite.GetData<Color>(0, sourceRectangle, retrievedColor, 0, 1);
        return retrievedColor[0];
    }

    
}

