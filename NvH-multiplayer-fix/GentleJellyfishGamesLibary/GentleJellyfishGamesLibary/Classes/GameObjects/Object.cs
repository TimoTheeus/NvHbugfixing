using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Basic Object, this is the building brick for everything in the game, all basic variables and methods are in this class.
public abstract class Object : IGameLoopObject
{
    protected Object parent;
    protected string id;
    protected bool visible;
    protected bool isGUI;
    protected bool isController;
    protected int layer;
    protected Vector2 position;

    //Readonly properties.
    public virtual Rectangle BoundingBox { get { return new Rectangle((int)GlobalPosition.X, (int)GlobalPosition.Y, 0, 0); } }
    public string ID { get { return id; }
        set { id = value; }
    }
    public virtual Vector2 GlobalPosition
    {
        get
        {
            if (parent != null)
                return parent.GlobalPosition + this.Position;
            else
                return this.Position;
        }
    }

    //Other properties.
    public Object Parent
    {
        get { return parent; }
        set { parent = value; }
    }
    public bool Visible
    {
        get { return visible; }
        set { visible = value; }
    }
    public Vector2 Position
    {
        get { return position; }
        set { position = value; }
    }
    public int Layer
    {
        get { return layer; }
        set { layer = value; }
    }

    //Initialization.
    public Object(string id = "", int layer = 0)
    {
        this.id = id;
        this.layer = layer;
        this.visible = false;
        this.isGUI = false;
    }

    //Base methods that can be overridden.
    public virtual void Update(GameTime gameTime)
    {
    }

    public virtual void HandleInput(InputHelper ih)
    {
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }

    public virtual void Reset()
    {
    }
}

