using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Tile : SpriteGameObject
{
    public Point gridPosition;
    protected List<Timer> buildTimers;
    protected List<SpriteGameObject> objectsToBuild;
    protected List<TextGameObject> text;
    TextGameObject descriptive;
    protected bool discovered;
    protected bool isBeingBuilt;
    protected bool isDark;
    private string actionString;

    public bool IsBeingBuilt
    {
        get { return isBeingBuilt; }
        set { isBeingBuilt = value; }
    }
    public bool Discovered
    {
        get { return discovered; }
        set { discovered = value; }
    }
    
    public bool IsDark
    {
        get { return isDark; }
        set { isDark = value; }
    }

    public Tile(string assetName="hexagonTile", string id="tile",int layer=1) : base(assetName, 0, id, layer)
    {
        this.Origin = this.sprite.Center;
        buildTimers = new List<Timer>();
        objectsToBuild = new List<SpriteGameObject>();
        text = new List<TextGameObject>();
        descriptive = new TextGameObject("smallFont", 4, "descriptive");
        isBeingBuilt = false;
    }

    public override void HandleInput(InputHelper ih)
    {
        if (GameData.Cursor.CurrentTile == this && ih.LeftButtonPressed() && GameData.Cursor.HasClickedTile)
        {
            LeftButtonAction();
        }
    }
    public override void Update(GameTime gameTime)
    {

        base.Update(gameTime);
        if (IsBeingBuilt)
        {
            for (int i = 0; i < buildTimers.Count; i++)
            {
                if (buildTimers[i] != null)
                {
                    buildTimers[i].Update(gameTime);
                    if (buildTimers[i].Ended)
                    {
                        if (objectsToBuild[i] is Unit)
                        {
                            objectsToBuild[i].Position = new Vector2(this.Position.X + this.Sprite.Width / 2 - objectsToBuild[i].Sprite.Width / 2, this.Position.Y + this.Sprite.Height / 2);
                            GameData.LevelObjects.Add(objectsToBuild[i]);
                            //this.actionString = "$mkun:" + b.ID + "$fnsh";
                        }
                        else
                        {
                            GameData.LevelGrid.replaceTile(this, objectsToBuild[i] as Building, true);
                            Building b = objectsToBuild[i] as Building;
                            GameData.Buildings.Add(b);
                            

                            
                            b.HasBeenBuiltAction();
                        }
                        buildTimers.Remove(buildTimers[i]);
                        objectsToBuild.Remove(objectsToBuild[i]);
                        if (text[i] != null)
                            text.Remove(text[i]);
                        GameData.LevelObjects.Remove(descriptive);
                    }
                }
            }
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (discovered)
        {
            if (isDark)
                Sprite.Color = Color.Gray;
            else
                Sprite.Color = Color.White;

            base.Draw(gameTime, spriteBatch);
            for (int i = 0; i < text.Count; i++)
            {
                if (text[i] != null && IsBeingBuilt)
                {
                    descriptive.Text = "constructing.." + (int)buildTimers[i].TimeLeft;
                }
                else { descriptive.Text = "waiting for worker.."; }
            }
        }
        else
        {
            Sprite.Color = Color.Gray;
            base.Draw(gameTime, spriteBatch);
        }
    }

    public virtual void LeftButtonAction()
    {
        GameData.LevelObjects.Add(new TileMenu(this));
    }

    public void AddTimer(Timer timer, SpriteGameObject objectToBuild)
    {
        buildTimers.Add(timer);
        objectsToBuild.Add(objectToBuild);
        if(objectToBuild is Building)
        {
            TextGameObject buildingText = new TextGameObject("smallFont", 4, "buildingText");
            text.Add(buildingText);
            descriptive.Position = Position + new Vector2(-50, -50);
            GameData.LevelObjects.Add(descriptive);
        }
    }

    public override string getActionOutput()
    {
        string s = this.actionString;
        if (this.actionString != null)
        {
            this.actionString = null;
        }
        return s;
    }

}

