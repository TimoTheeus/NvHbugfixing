using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

class Building : Tile
{
    protected float health, maxHealth;
    protected HealthBar healthBar;
    protected Player.Faction faction;
    public string name;
    public int level;
    public int maxLevel;
    public int buildCost, levelUpCost2, levelUpCost3;
    protected Point resourceCosts;

    public Player.Faction Faction
    {
        get { return faction; }
        set { faction = value; }
    }
    public Point ResourceCosts
    {
        get { return resourceCosts; }
        set { resourceCosts = value; }
    }
    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    public float MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }
    public Building(string id = "", string assetName=""):base(assetName,id)
    {
        RemoveMenu();
        health = 500;
        maxHealth = 500;
        healthBar = new HealthBar(new Vector2(position.X, position.Y + sprite.Height / 2 + 10));
        level = 1;
        resourceCosts = new Point(100,100);
    }

    protected void RemoveMenu()
    {
        TileMenu menu = GameData.LevelObjects.Find("menu") as TileMenu;
        if (menu != null)
            GameData.LevelObjects.Remove(menu);
    }
    public virtual void DealDamage(float amount, GameObject attacker)
    {
        this.Health -= amount;
        if (this.Health <= 0)
        {
            if (!(this is Forest))
            {
                if (attacker is Unit)
                    ((GameWorld.GameStateManager.GetGameState("hud") as HUD).hud.Find("eventLog") as EventLog).Add(this.name, (attacker as Unit).name, true, false);
                if (attacker is Spell)
                    ((GameWorld.GameStateManager.GetGameState("hud") as HUD).hud.Find("eventLog") as EventLog).Add(this.name, (attacker as Spell).name, true, true);
            }
            Destroy();
        }
    }

    public virtual void Destroy()
    {
        GameData.LevelGrid.replaceTile(this, new Tile(), false);
        GameData.Buildings.Remove(this);
    }

    public virtual void HasBeenBuiltAction()
    {
  
    }
    public void UpdateDiscoveredArea()
    {
        foreach (Tile t in GameData.LevelGrid.Objects)
        {
            Vector2 distance = new Vector2(Math.Abs(this.GlobalPosition.X - t.Position.X), Math.Abs(this.GlobalPosition.Y - t.Position.Y));
            double absDistance = Math.Sqrt(Math.Pow(distance.X, 2) + Math.Pow(distance.Y, 2));
            if (absDistance < 300)
            {
                t.Discovered = true;
                t.IsDark = false;
            }
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        
        healthBar.Update(new Vector2(position.X, position.Y - sprite.Height / 2 - 10));
        healthBar.ChangeHealth((float)((health / maxHealth) * 1.5));
        if(!IsDark)
        healthBar.Draw(gameTime, spriteBatch);
        position -= new Vector2(0, sprite.Height / 2 - new Tile().Sprite.Height / 2);
        base.Draw(gameTime, spriteBatch);
        position += new Vector2(0, sprite.Height / 2 - new Tile().Sprite.Height / 2);
    }

}
