using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class ResourceController : GameObject
{
    Timer passiveResourceTimer;
    protected int primaryResourceAmount;
    protected int secondaryResourceAmount;
    public ResourceController(int timer,int primaryResourceAmount, int secondaryResourceAmount) : base("resourceController")
    {
        passiveResourceTimer = new Timer(timer);
        this.primaryResourceAmount = primaryResourceAmount;
        this.secondaryResourceAmount = secondaryResourceAmount;
    }
    public override void Update(GameTime gameTime)
    {
        passiveResourceTimer.Update(gameTime);
        checkPassiveTimer();
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }

    protected void checkPassiveTimer()
    {
        if (passiveResourceTimer.Ended)
        {
            for (int i = 0; i < GameData.LevelObjects.Objects.Count; i++)
            {
                if (GameData.LevelObjects.Objects[i] is Player)
                {
                    Player player = GameData.LevelObjects.Objects[i] as Player;
                    player.MainResource += primaryResourceAmount;
                    player.SecondaryResource += secondaryResourceAmount;
                }
            }
            passiveResourceTimer.Reset();
        }
    }
}

