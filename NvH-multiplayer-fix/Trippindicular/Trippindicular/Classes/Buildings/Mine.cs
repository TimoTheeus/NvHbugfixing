using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


class Mine : Building
{
    ResourceController resController;
    public Mine() : base("mine","mine")
    {
        Faction = Player.Faction.humanity;
        name = "COALMINE";
        this.resourceCosts = new Point(100, 50);
    }

    public override void HandleInput(InputHelper ih)
    {
        if (GameData.Cursor.CurrentTile == this && ih.LeftButtonPressed() && GameData.Cursor.HasClickedTile)
        {
            GameData.Cursor.HasClickedTile = false;
        }
    }

    public override void Destroy()
    {
        base.Destroy();
        GameData.LevelObjects.Remove(resController);
    }
    public override void HasBeenBuiltAction()
    {
        base.HasBeenBuiltAction();
        resController = new ResourceController(3, 4, 0);
        GameData.LevelObjects.Add(resController);
    }
}

