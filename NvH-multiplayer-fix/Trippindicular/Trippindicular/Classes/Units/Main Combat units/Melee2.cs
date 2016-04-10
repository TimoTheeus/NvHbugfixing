using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;

//strong vs fast melee
class Melee2 : Unit
{
    private int speedBuffAmnt;
    private float unBuffedSpeed;
    private float buffedSpeed;
    public Melee2(Player.Faction faction, string assetName = "", string id = "") : base(assetName, id)
    {
        this.faction = faction;
        this.speed = 150;
        if (this.faction == Player.Faction.nature)
        {
            Damage = 30;
            this.unBuffedSpeed = speed;
            this.buffedSpeed = speed + 100;
            maxHealth = 225;
            health = maxHealth;
            this.resourceCosts = new Point(150, 150);
        }
        else {
            Damage = 40;
            maxHealth = 150;
            health = maxHealth;
            this.resourceCosts = new Point(150, 100);
        }
    }
    public override void Attack()
    {
        if (targetUnit != null)
        {
            if (targetUnit.Health <= 0)
            {
                targetUnit = null;
                return;
            }
            if (targetUnit is Unicorn)
            {
                targetUnit.DealDamage(this.Damage*2, this);
            }
            else
            { targetUnit.DealDamage(this.Damage, this); }
        }

        else
        {
            if (targetBuilding.Health <= 0)
            {
                targetBuilding = null;
                return;
            }
            targetBuilding.DealDamage(this.Damage, this);
        }

        attackTimer.Reset();
    }

    public override void UpdateDiscoveredArea()
    {
        bool speedBuff = false;
        if (faction == GameData.player.GetFaction){
            foreach (Tile t in GameData.LevelGrid.Objects)
            {
                Vector2 distance = new Vector2(Math.Abs(this.GlobalPosition.X - t.Position.X), Math.Abs(this.GlobalPosition.Y - t.Position.Y));
                double absDistance = Math.Sqrt(Math.Pow(distance.X, 2) + Math.Pow(distance.Y, 2));
                if (absDistance < 300)
                {
                    t.Discovered = true;
                    if (t is Forest)
                    {
                        speedBuff = true;
                    }
                    t.IsDark = false;
                    }
                }
        }
        if (speedBuff)
        {
            this.speed = buffedSpeed;
        }
        else
        {
            this.speed = unBuffedSpeed;
        }


         
        }
    }

