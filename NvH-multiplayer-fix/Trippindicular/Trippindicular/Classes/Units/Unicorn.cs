using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

//Has high move speed and ability to stun+damage
//melee1 and ranged by running over them.
//countered by melee2
class Unicorn : Unit
{
    protected bool slowed;
    protected Timer slowTimer;
    protected float stunTime;
    public Unicorn(Player.Faction faction,string assetName="", string id="") : base(assetName, id)
    {
        this.faction = faction;
        if (this.faction == Player.Faction.nature)
        {
            this.Speed = 250f;
            Damage = 40;
            maxHealth = 300;
            health = maxHealth;
            slowTimer = new Timer(2f);
            stunTime = 0.5f;
            this.resourceCosts = new Point(100, 50);
        }
        else
        {
            this.Speed = 250f;
            Damage = 50;
            maxHealth = 240;
            health = maxHealth;
            slowTimer = new Timer(2f);
            stunTime = 0.5f;
            this.resourceCosts = new Point(100, 50);
        }
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (this.Velocity != Vector2.Zero)
        {
            if (slowed)
            {
                slowTimer.Update(gameTime);
                if (slowTimer.TimeLeft <= 1)
                    this.Speed =210;
                if (slowTimer.Ended)
                    slowed = false;
            }
            else { Speed = 250f; }

            for (int i = 0; i < GameData.Units.Objects.Count; i++)
            {
                if (GameData.Units.Objects[i] != null&& GameData.Units.Objects[i]!=this)
                {
                    Unit u = GameData.Units.Objects[i] as Unit;
                    if ((this.CollidesWith(u) && this.Speed > 210&&!u.Frozen) && (u.Faction != Player.Faction.nature))
                    {
                        if (u is Melee2)
                        {
                            //decrease speed a lot, get damaged
                            slowed = true;
                            Speed = 110;
                            slowTimer.Reset();
                            this.DealDamage(u.Damage * 2, u);
                        }
                        else
                        {
                            //knockback+stun+dmg unit+decrease speed a little
                            slowed = true;
                            Speed = 180;
                            u.DealDamage(this.Damage*2, this);
                        }
                    }
                }
            }
        }
    }
}

