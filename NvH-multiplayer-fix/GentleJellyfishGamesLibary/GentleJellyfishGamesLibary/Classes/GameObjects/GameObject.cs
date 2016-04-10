using Microsoft.Xna.Framework;

//This is the class that is used for everything in the game that is visible for the player, 
//all variables and methods important for all visible objects in the game are in this class.
public abstract class GameObject: Object
{
    protected Vector2 velocity;
    protected bool solid;

    public virtual Vector2 CenterPos { get { return new Vector2(); } }

    public bool Solid
    {
        get { return solid;}
        set { solid = value; }
    }
    public Vector2 Velocity
    {
        get { return velocity; }
        set { velocity = value; }
    }
    public float PositionX
    {
        get { return position.X; }
        set { position.X = value; }
    }
    public float PositionY
    {
        get { return position.Y; }
        set { position.Y = value; }
    }
    public float VelocityX
    {
        get { return velocity.X; }
        set { velocity.X = value; }
    }
    public float VelocityY
    {
        get { return velocity.Y; }
        set { velocity.Y = value; }
    }

    public GameObject(string id = "", int layer = 0) : base(id, layer)
    {
        this.solid = false;
        this.visible = true;
    }

    public override void Update(GameTime gameTime)
    {
        //Add velocity corrected by gameTime to the position, this is more efficient than += and since it is called a lot, this way is used.
        Vector2 temp = new Vector2();
        Vector2.Multiply(ref velocity, (float)gameTime.ElapsedGameTime.TotalSeconds, out temp);
        Vector2.Add(ref temp, ref position, out position);
    }

    public virtual string getActionOutput()
    {
        return null;
    }
}

