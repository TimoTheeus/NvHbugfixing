using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


class SunlightTree:Building
{
    ResourceController resController;
    public SunlightTree() : base("sunlightTree", "natureLightTree")
    {
        Faction = Player.Faction.nature;
        this.resourceCosts = new Point(60, 0);
    }


    public override void Destroy()
    {
        base.Destroy();
        GameData.LevelObjects.Remove(resController);
    }
    public override void HasBeenBuiltAction()
    {
        base.HasBeenBuiltAction();
        resController = new ResourceController(10, 10, 0); // 60/m
        GameData.LevelObjects.Add(resController);
    }
}

