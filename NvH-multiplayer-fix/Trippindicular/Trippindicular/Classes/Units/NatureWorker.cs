using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

class NatureWorker : Unit
{
    public NatureWorker() : base("natureWorker", "natureWorker")
    {
        this.Damage = 0;
        this.Range = 0;
        Faction = Player.Faction.nature;
        name = "NATURE BUILDER";
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
        Point p = new Point((int)this.Position.X + 30, (int)this.Position.Y + 30);
        GameData.LevelGrid.GetTile(p).IsBeingBuilt = true;
    }
    protected override void Die()
    {
        Point p = new Point((int)this.Position.X + 30, (int)this.Position.Y + 30);
        GameData.LevelGrid.GetTile(p).IsBeingBuilt = false;
        base.Die();
    }

}

