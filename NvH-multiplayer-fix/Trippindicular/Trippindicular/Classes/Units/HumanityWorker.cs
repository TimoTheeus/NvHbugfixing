using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


class HumanityWorker : Unit
{

    public HumanityWorker() : base("humanityWorker", "hworker")
    {
        this.Damage = 0;
        Range = 0;
        Faction = Player.Faction.humanity;
        name = "HUMAN BUILDER";
        pacifist = true;
    }

    protected override void RightClickAction()
    {
        Point p = new Point((int)this.Position.X + 30, (int)this.Position.Y + 30);
        GameData.LevelGrid.GetTile(p).IsBeingBuilt = false;
        base.RightClickAction();
        //return "unit:" + this.id + ":move:" + p.X + "," + p.Y;
    }
    protected override void ArrivedAtTileAction()
    {
        base.ArrivedAtTileAction();
        Point p = new Point((int)this.Position.X+30, (int)this.Position.Y+30);
        GameData.LevelGrid.GetTile(p).IsBeingBuilt = true;
    }
    protected override void Die()
    {
        Point p = new Point((int)this.Position.X + 30, (int)this.Position.Y + 30);
        GameData.LevelGrid.GetTile(p).IsBeingBuilt = false;
        base.Die();
    }

}

