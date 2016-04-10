using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Menu : GameObjectList
{
    protected SpriteGameObject background;

    public Menu(int layer = 0, string id = "") : base(layer, id) 
    {

    }

    public void addButton(Button b)
    {
        this.Add(b);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.LeftButtonPressed() && !inputHelper.MouseInBox(background.BoundingBox))
        {

            GameData.LevelObjects.Remove(this);

            GameData.Cursor.HasClickedTile = false;
        }
        base.HandleInput(inputHelper);
    }
}
