using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

class NatureBaseMenu : Menu
{
    protected Button button1, button2, button3,button4,button5;
    protected Tile tile;
    protected Spell spell;
    private string actionString;
    private bool actionSent;
    protected Player player;

    public NatureBaseMenu(Tile tile)
        : base(10, "barracksMenu")
    {
        actionString = null;
        actionSent = false;
        this.tile = tile;

        background = new SpriteGameObject("button", 0, "background", 4);
        background.Position = new Vector2(GameData.Cursor.CurrentTile.Position.X, GameData.Cursor.CurrentTile.Position.Y + (new Tile().Height * 3 / 2));
        background.Origin = background.Sprite.Center;
        this.Add(background);
        button1 = new Button("checkBox", "", "", 0, "", 4);
        button1.Position = background.Position + new Vector2(-120, 0);
        button2 = new Button("workernButton", "", "", 0, "", 4);
        button2.Position = button1.Position + new Vector2(button1.Width+25, 0);
        button3 = new Button("snowButton", "", "", 0, "", 4);
        button3.Position = button2.Position + new Vector2(button1.Width + 25, 0);
        addButton(button1);
        addButton(button2);
        button4 = new Button("meteorButton", "", "", 0, "", 4);
        button4.Position= button3.Position + new Vector2(button1.Width + 25, 0);
        if ((tile as Building).level >= 2)
            addButton(button3);
        if ((tile as Building).level >= 3)
            addButton(button4);
        for (int i = 0; i < GameData.LevelObjects.Objects.Count; i++)
        {
            if (GameData.LevelObjects.Objects[i] is Player)
            {
                player = GameData.LevelObjects.Objects[i] as Player;
                return;
            }
        }
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        Unit unit = null;
        if (button1 != null && button1.Pressed && (tile as Building).level < (tile as Building).maxLevel)
        {
            if (GameData.player.MainResource - 300 >= 0 && GameData.player.SecondaryResource - 200 >= 0)
            {
                GameData.Cursor.HasClickedTile = false;
                GameData.LevelObjects.Remove(this);
                (this.tile as Building).level += 1;
            }
            else {
                Notification n = new Notification("Not enough resources, upgrading costs:", " 300 Sunlight and 200 Water", "", 3);
                n.CreateNotification();
                GameData.Cursor.HasClickedTile = false;
                GameData.LevelObjects.Remove(this);
            }

        }
        else if (button2 != null && button2.Pressed)
        {
            unit = new NatureWorker();
            if (player.MainResource - unit.ResourceCosts.X >= 0 && player.SecondaryResource - unit.ResourceCosts.Y >= 0)
            {
                player.MainResource -= unit.ResourceCosts.X;
                player.SecondaryResource -= unit.ResourceCosts.Y;
                unit.Position = new Vector2(tile.Position.X + new Tile().Sprite.Width / 2 - unit.Sprite.Width / 2, tile.Position.Y +
                    new Tile().Sprite.Height / 2);
                GameData.Cursor.HasClickedTile = false;
                //GameData.LevelObjects.Remove(this);
            }
            else {
                Notification n = new Notification("Not enough resources, it costs:", unit.ResourceCosts.X.ToString() + " Sunlight and " + unit.ResourceCosts.Y.ToString() + " Water", "", 3);
                n.CreateNotification(); unit = null; }
        }
        else if (button3 != null && button3.Pressed)
        {
            if (GameData.player.MainResource - 500 >= 0 && GameData.player.SecondaryResource - 500 >= 0)
            {
                spell = new SnowStorm();
                GameData.Cursor.Spell = spell;
                GameData.Cursor.HasClickedTile = false;
                GameData.LevelObjects.Remove(this);
            }
            else {
                Notification n = new Notification("Not enough resources, it costs:", " 500 Sunlight and 500 Water", "", 3);
                n.CreateNotification();
            }
        }
        else if (button4 != null && button4.Pressed)
        {
            if (GameData.player.MainResource - 500 >= 0 && GameData.player.SecondaryResource - 500 >= 0)
            {
                spell = new MeteorStorm();
                GameData.Cursor.Spell = spell;
                GameData.Cursor.HasClickedTile = false;
                GameData.LevelObjects.Remove(this);
            }
            else {
                Notification n = new Notification("Not enough resources, it costs:", " 500 Sunlight and 500 Water", "", 3);
                n.CreateNotification();
            }
        }      
        if(unit!= null)
        {
            GameData.AddUnit(unit);
            this.actionString = "$addu:" + unit.ID + "$type:" + unit.GetType() + "$posi:" + unit.Position.X + "," + unit.Position.Y;//;
        }

    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (actionSent)
        {
            GameData.LevelObjects.Remove(this);
        }
    }
    public override string getActionOutput()
    {
        string s = this.actionString;
        if (s != null)
        {
            actionSent = true;
            this.actionString = null;
        }
        return s;
    }
}



