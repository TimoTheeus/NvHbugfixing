using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//This class is the base for every object in the game that uses a sprite.
public class SpriteGameObject : GameObject
{
    protected SpriteSheet sprite;
    protected Vector2 origin;
    protected const int tileWidth = GameSettings.TileWidth;
    protected const int tileHeight = GameSettings.TileHeight;
    //Readonly properties.
    public int Height { get { return sprite.Height; } }
    public int Width { get { return sprite.Width; } }
    public SpriteSheet Sprite { get { return sprite; } }
    public override Vector2 CenterPos { get { return GlobalPosition + sprite.Center; } }

    public Vector2 Origin
    {
        get { return origin; }
        set { origin = value; }
    }
    public bool Mirror
    {
        get { return sprite.Mirror; }
        set { sprite.Mirror = value; }
    }
    public float Scale
    {
        get { return sprite.Scale;}
        set { sprite.Scale = value; }
    }
    
    //Returns the boundingbox of the sprite.
    public override Rectangle BoundingBox
    {
        get
        {
            int left = (int)(GlobalPosition.X - origin.X);
            int top = (int)(GlobalPosition.Y - origin.Y);
            return new Rectangle(left, top, Width, Height);
        }
    }

    //Constructor uses the assetname as the ID if no id was given.
    public SpriteGameObject(string assetName, int sheetIndex = 0, string id = "", int layer = 0) : base(id, layer)
    {
        if (assetName != "")
            sprite = new SpriteSheet(assetName, sheetIndex);
        else
            sprite = null;

        if (id == "" && assetName != "")
            this.id = assetName;
    }

    //Draws the sprite if the sprite is visible (and exists).
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!visible || sprite == null)
        {
            if (sprite == null)
                Log.Write(LogType.ERROR, "Error sprite from " + id + " is null!");
            return;
        }

        sprite.Draw(spriteBatch, position, origin);
        
    }

    //This method checks if this object collides with given object, this works by checking whether the pixels in the collision rectangle overlap or not.
    public bool CollidesWith(GameObject obj)
    {
        SpriteGameObject sprObj = obj as SpriteGameObject;
        if (sprObj == null || !sprObj.Visible)
            return false;
        
        Rectangle intersect = Collision.Intersection(BoundingBox, obj.BoundingBox);

        for (int x = 0; x < intersect.Width; x++)
            for (int y = 0; y < intersect.Height; y++)
            {
                int thisx = intersect.X - (int)(GlobalPosition.X - origin.X) + x;
                int thisy = intersect.Y - (int)(GlobalPosition.Y - origin.Y) + y;
                int objx = intersect.X - (int)(obj.GlobalPosition.X - sprObj.origin.X) + x;
                int objy = intersect.Y - (int)(obj.GlobalPosition.Y - sprObj.origin.Y) + y;
                if (sprite.GetPixelColor(thisx, thisy).A != 0
                    && sprObj.sprite.GetPixelColor(objx, objy).A != 0)
                    return true;
            }
        return false;
    }

    public override string getActionOutput()
    {
        return base.getActionOutput();
    }
}

