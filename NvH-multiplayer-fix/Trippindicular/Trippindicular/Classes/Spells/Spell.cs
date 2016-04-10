using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

class Spell : SpriteGameObject
{
    public string name;
    public int sunlightCost, waterCost, range;
    public Timer timer;

    public Spell(string assetName = "", string id = "") : base(assetName, 0, id, 10)
    {
        
    }

    public override void Update(GameTime gameTime)
    {
        timer.Update(gameTime);
        Console.WriteLine(this.position.ToString());
        Console.WriteLine();

        for (int i = 0; i < GameData.Units.Objects.Count; i++)
            if (((Unit)GameData.Units.Objects[i]).Faction != Player.Faction.nature)
            {
                Vector2 distance = new Vector2(Math.Abs(this.GlobalPosition.X - ((Unit)GameData.Units.Objects[i]).Position.X), Math.Abs(this.GlobalPosition.Y - ((Unit)GameData.Units.Objects[i]).Position.Y));
                double absDistance = Math.Sqrt(Math.Pow(distance.X, 2) + Math.Pow(distance.Y, 2));
                if (absDistance < range)
                {
                    if (timer.TimeLeft >= 0)
                        SpellEffect(gameTime, GameData.Units.Objects[i]);
                    else
                    {
                        UndoSpellEffect(GameData.Units.Objects[i]);
                    }
                }
                    
            }
        if (timer.TimeLeft <= 0)
        {
            GameData.LevelObjects.Remove(this);
        }
        base.Update(gameTime);
    }

    public virtual void SpellEffect(GameTime gameTime, GameObject target)
    {

    }

    public virtual void UndoSpellEffect(GameObject target)
    {

    }


}

