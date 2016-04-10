using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


class WaterTree : Building
{
    ResourceController resController;
    public WaterTree() : base("natureWaterTree", "natureWaterTree")
    {
        Faction = Player.Faction.nature;
        this.resourceCosts = new Point(150, 0);
    }


    public override void Destroy()
    {
        base.Destroy();
        GameData.LevelObjects.Remove(resController);
    }
    public override void HasBeenBuiltAction()
    {
        base.HasBeenBuiltAction();
        resController = new ResourceController(3, 0, 5); //100pm
        GameData.LevelObjects.Add(resController);
    }
}

