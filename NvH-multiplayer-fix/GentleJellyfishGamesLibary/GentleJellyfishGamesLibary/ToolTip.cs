using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class ToolTip : GameObjectList
{
    protected TextGameObject name, hp, damage, range, speed, mainCost, secCost;
    public SpriteGameObject backGround;

    public ToolTip() : base(10, "toolTip")
    {
        backGround = new SpriteGameObject("toolTipBack");
        backGround.Position = Vector2.Zero;
        mainCost = new TextGameObject("smallFont");
        mainCost.Text = "I";
        mainCost.Position = Vector2.Zero;
        secCost = new TextGameObject("smallFont");
        secCost.Position = new Vector2(0, 20);
        name = new TextGameObject("smallFont");
        name.Position = new Vector2(0, 40);
        hp = new TextGameObject("smallFont");
        hp.Position = new Vector2(0, 60);
        damage = new TextGameObject("smallFont");
        damage.Position = new Vector2(0, 80);
        range = new TextGameObject("smallFont");
        range.Position = new Vector2(0, 100);
        speed = new TextGameObject("smallFont");
        speed.Position = new Vector2(0, 120);
        


    }

    public string MainCost
    {
        set { mainCost.Text = value; }
    }

    public string SecCost
    {
        set { secCost.Text = value; }
    }

    public string Name
    {
        set { name.Text = "Name: " + value; }
    }

    public string Hp
    {
        set { hp.Text = "HP: " + value; }
    }

    public string Damage
    {
        set { damage.Text = "Damage: " + value; }
    }

    public string Range
    {
        set { range.Text = "Range: " + value; }
    }

    public string Speed
    {
        set { speed.Text = "Speed: " + value; }
    }
}
