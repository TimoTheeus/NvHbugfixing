using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class CursorToolTip : ToolTip
{
    public CursorToolTip()
    {
        Add(backGround);
        Add(name);
        Add(mainCost);
        Add(secCost);
        Add(hp);
        Add(damage);
        Add(range);
        Add(speed);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach (GameObject obj in Objects)
        {
            obj.Position += GameWorld.Camera.Pos + GameData.Cursor.Position;
            obj.Draw(gameTime, spriteBatch);
            obj.Position -= GameWorld.Camera.Pos + GameData.Cursor.Position;
        }
    }
}
