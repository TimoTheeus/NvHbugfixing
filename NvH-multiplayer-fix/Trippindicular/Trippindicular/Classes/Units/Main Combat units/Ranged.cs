using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;

//Low HP but high range and good attack values

class Ranged : Unit
{
    public Ranged(Player.Faction faction, string assetName = "", string id = "") : base(assetName, id)
    {
        this.faction = faction;
        
        if (this.faction == Player.Faction.nature)
        {
            Damage = 25;
            maxHealth = 150;
            health = maxHealth;
            Range = 200;
            resourceCosts = new Point(100, 25);
        }
        else {
            Damage = 25;
            maxHealth = 150;
            health = maxHealth;
            Range = 200;
            resourceCosts = new Point(100, 25);
        }
    }
}

