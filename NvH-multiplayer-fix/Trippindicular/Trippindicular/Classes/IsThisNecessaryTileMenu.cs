using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

public class TempTileMenu : GameObjectList
{
    private int radius;

    public TempTileMenu() : base(4,"tileMenu")
    {
        this.Position = new Vector2(GameWorld.Screen.X / 2, GameWorld.Screen.Y / 2);
        this.radius = GameWorld.Screen.Y / 4;
    }


    private void Update()
    {
        double step = 2 * Math.PI / Objects.Count;
        for (int i = 0; i < Objects.Count; i++)
        {
            double a = step * i - Math.PI / 2;
            Objects[i].Position = new Vector2(Position.X + (float)(radius * Math.Cos(a)), Position.Y + (float)(radius * Math.Sin(a)));
        }
    }
}
