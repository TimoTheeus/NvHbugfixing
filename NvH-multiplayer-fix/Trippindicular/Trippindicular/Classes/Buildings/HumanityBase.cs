using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

class HumanityBase : PolyTileBuilding
{

    public HumanityBase() : base("humanityBase", "humanityBase")
    {
        this.health = 2000;
        MaxHealth = health;
        Faction = Player.Faction.humanity;
        name = "HUMAN BASE";
        maxLevel = 2;
    }

    public override void Destroy()
    {
        base.Destroy();
        GameWorld.GameStateManager.SwitchTo("finish");
    }
    public override void LeftButtonAction()
    {
        GameData.LevelObjects.Add(new HumanityBaseMenu(this));
        GameData.Cursor.HasClickedTile = false;
    }
}

