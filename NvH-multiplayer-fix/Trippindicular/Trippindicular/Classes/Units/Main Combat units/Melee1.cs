using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

//strong vs melee2 and ranged
class Melee1 : Unit
{
    public Melee1(Player.Faction faction, string assetName = "", string id = "") : base(assetName, id)
    {
        this.faction = faction;
        if (this.faction == Player.Faction.nature)
        {
            Damage = 25;
            maxHealth = 175;
            health = maxHealth;
            resourceCosts = new Point(100, 25);
        }
        else {
            Damage = 30;
            maxHealth = 150;
            health = maxHealth;
            resourceCosts = new Point(100, 50);
        }
    }
}

