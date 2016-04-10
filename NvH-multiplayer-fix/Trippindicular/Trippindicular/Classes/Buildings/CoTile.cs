using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

class CoTile : Building
{
    public Building mainTile;

    public CoTile() : base("coTile", "hexagonTile")
    {
        healthBar.Visible = false;
    }

    public override void HandleInput(InputHelper ih)
    {
        if (GameData.Cursor.CurrentTile == this && ih.LeftButtonPressed() && GameData.Cursor.HasClickedTile)
        {
            mainTile.LeftButtonAction();
        }
    }

    public override void DealDamage(float amount, GameObject attacker)
    {
        mainTile.DealDamage(amount, attacker);
    }

    public override void LeftButtonAction()
    {
    }
}
