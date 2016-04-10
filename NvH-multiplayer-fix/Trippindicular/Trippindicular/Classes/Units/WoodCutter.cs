using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class WoodCutter : Unit
{
    public WoodCutter() : base("woodcutter", "WoodCutter")
    {
        Faction = Player.Faction.humanity;
        name = "WOODCUTTER";
        this.ResourceCosts = new Microsoft.Xna.Framework.Point(75, 0);
    }

    public override void Attack()
    {
        if (targetBuilding != null && GameData.player.GetFaction == Player.Faction.humanity)
        {
            GameData.player.SecondaryResource += 2;
        }
        base.Attack();
    }
}
