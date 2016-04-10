using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class DropDown : GUIGameObject
{
    protected bool hover, drop;
    protected Vector2 startPos;
    protected int targetY, speed;

    public bool Hover
    {
        get { return hover; }
        set { hover = value; }
    }

    public bool Drop
    {
        get { return drop; }
        set { drop = value; }
    }

    public int TargetY
    {
        get { return targetY; }
        set { targetY = value; }
    }

    public int Speed
    {
        get { return speed; }
        set { speed = value; }
    }


    public DropDown(string assetName, Vector2 startPosition, int sheetIndex = 0, string id = "", int layer = 0) : base(assetName, sheetIndex, id, layer)
    {
        hover = false;
        targetY = 0;
        speed = 300;

        this.Position = startPosition;
        this.startPos = startPosition;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        //Base
        base.HandleInput(inputHelper);

        //Handle input for dropdown
        if ((inputHelper.MouseInBox(this.BoundingBox) || drop))
        {
            if (this.Position.Y < targetY)
            {
                this.Velocity = new Vector2(0, speed);
            }
            else
            {
                this.Velocity = Vector2.Zero;
            }
        }
        else
        {
            if (this.Position.Y > startPos.Y)
            {
                this.Velocity = new Vector2(0, -speed);
            }
            else
            {
                this.Velocity = Vector2.Zero;
            }
        }
    }

    public override void Reset()
    {
        //Base
        base.Reset();

        //Reset dropdown state
        this.hover = false;
        this.Position = startPos;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (this.Position.Y > targetY)
        {
            this.Position = new Vector2(this.Position.X, targetY);
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
    }

    public void Start()
    {
        this.Position = startPos;
    }
}