using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

class NatureBarracks:Building
{
    public NatureBarracks() : base("natureBarrack", "selectedTile")
    {
        Faction = Player.Faction.nature;
        name = "NATURE BARRACKS";
        level = 1;
        maxLevel = 3;
        this.resourceCosts = new Point(200, 50);
    }

    public override void LeftButtonAction()
    {

        if (GameData.player.GetFaction == Player.Faction.nature)
        {
            GameData.LevelObjects.Add(new BarracksMenu(this));
            GameData.Cursor.HasClickedTile = false;
        }
    }
}

