//Class for a special type of spritegameobject used for GUI.
public class GUIGameObject : SpriteGameObject
{
    public GUIGameObject(string assetName, int sheetIndex = 0, string id = "", int layer = 0) : base(assetName, sheetIndex, id, layer)
    {
        isGUI = true;
    }    
}

