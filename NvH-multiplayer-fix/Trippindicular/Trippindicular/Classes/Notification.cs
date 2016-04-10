using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Notification : TextGameObject
{
    protected const float visibleTime = 3f;
    protected string header;
    protected string line1;
    protected string line2;
    protected float time;
    Timer visibleTimer;
    public Notification(string header = "", string line1 = "", string line2 = "", float displayTime = visibleTime) : base("font", 1, "notification")
    {
        this.header = header;
        this.Color = Color.White;
        this.line1 = line1;
        this.line2 = line2;
        time = displayTime;
        visibleTimer = new Timer(visibleTime);
        this.Position = new Vector2(800, 180);
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        visibleTimer.Update(gameTime);
        if (visibleTimer.Ended)
        {
            GameData.LevelObjects.Remove(this);
        }
        this.Position = new Vector2(800, 180) + GameWorld.Camera.Pos;
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (this.text == null)
            this.text = "null";

        if (visible)
        {
            spriteBatch.DrawString(spriteFont, header, this.GlobalPosition, color);
            spriteBatch.DrawString(spriteFont, line1, this.GlobalPosition + new Vector2(0, 40), color);
            spriteBatch.DrawString(spriteFont, line2, this.GlobalPosition + new Vector2(0, 80), color);
        }
    }
    public void CreateNotification()
    {
        for (int i = 0; i < GameData.LevelObjects.Objects.Count; i++)
        {
            if (GameData.LevelObjects.Objects[i] is Notification)
            {
                Notification notif = GameData.LevelObjects.Objects[i] as Notification;
                GameData.LevelObjects.Remove(notif);
            }
        }
        Notification notification = new Notification(header, line1, line2, time);
        GameData.LevelObjects.Add(notification);
    }
}

