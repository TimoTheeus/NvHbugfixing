using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class MenuCursor : SpriteGameObject
    {
    private string actionString;

    public MenuCursor(string assetName = "cursorDot") : base(assetName, 0, "cursor", 10)
    {
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        this.Position = inputHelper.MousePosition;
    }
}