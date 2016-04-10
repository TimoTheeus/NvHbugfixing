using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class PolyTileBuilding : Building
{
    List<CoTile> coTiles;

    public PolyTileBuilding(string id, string assetName) : base(id, assetName)
    {
        coTiles = new List<CoTile>();
    }

    public void AddQuadCoTiles()
    {
        CoTile newTile = new CoTile();
        newTile.mainTile = this;
        newTile.Position = new Vector2(Position.X, Position.Y - GameData.LevelGrid.offsetY * 2);
        newTile.gridPosition = new Point(gridPosition.X, gridPosition.Y - 2);
       // GameData.LevelGrid.Objects[newTile.gridPosition.X, newTile.gridPosition.Y] = newTile;
        GameData.LevelGrid.replaceTile((Tile)GameData.LevelGrid.Objects[newTile.gridPosition.X, newTile.gridPosition.Y], newTile, false);
        coTiles.Add(newTile);

        if ((GameData.LevelGrid.startLeft && gridPosition.Y % 2 == 0) || (!GameData.LevelGrid.startLeft && gridPosition.Y % 2 == 1))
        {
            newTile = new CoTile();
            newTile.mainTile = this;
            newTile.Position = new Vector2(Position.X - GameData.LevelGrid.offsetX, Position.Y - GameData.LevelGrid.offsetY);
            newTile.gridPosition = new Point(gridPosition.X, gridPosition.Y - 1);
           // GameData.LevelGrid.Objects[newTile.gridPosition.X, newTile.gridPosition.Y] = newTile;
            GameData.LevelGrid.replaceTile((Tile)GameData.LevelGrid.Objects[newTile.gridPosition.X, newTile.gridPosition.Y], newTile, false);
            coTiles.Add(newTile);

            newTile = new CoTile();
            newTile.mainTile = this;
            newTile.Position = new Vector2(Position.X + GameData.LevelGrid.offsetX, Position.Y - GameData.LevelGrid.offsetY);
            newTile.gridPosition = new Point(gridPosition.X + 1, gridPosition.Y - 1);
           // GameData.LevelGrid.Objects[newTile.gridPosition.X, newTile.gridPosition.Y] = newTile;
            GameData.LevelGrid.replaceTile((Tile)GameData.LevelGrid.Objects[newTile.gridPosition.X, newTile.gridPosition.Y], newTile, false);
            coTiles.Add(newTile);
        }

        else
        {
            newTile = new CoTile();
            newTile.mainTile = this;
            newTile.Position = new Vector2(Position.X - GameData.LevelGrid.offsetX, Position.Y - GameData.LevelGrid.offsetY);
            newTile.gridPosition = new Point(gridPosition.X - 1, gridPosition.Y - 1);
           // GameData.LevelGrid.Objects[newTile.gridPosition.X, newTile.gridPosition.Y] = newTile;
            GameData.LevelGrid.replaceTile((Tile)GameData.LevelGrid.Objects[newTile.gridPosition.X, newTile.gridPosition.Y], newTile, false);
            coTiles.Add(newTile);

            newTile = new CoTile();
            newTile.mainTile = this;
            newTile.Position = new Vector2(Position.X + GameData.LevelGrid.offsetX, Position.Y - GameData.LevelGrid.offsetY);
            newTile.gridPosition = new Point(gridPosition.X, gridPosition.Y - 1);
           // GameData.LevelGrid.Objects[newTile.gridPosition.X, newTile.gridPosition.Y] = newTile;
            GameData.LevelGrid.replaceTile((Tile)GameData.LevelGrid.Objects[newTile.gridPosition.X, newTile.gridPosition.Y], newTile, false);
            coTiles.Add(newTile);
        }
    }

    public override void Destroy()
    {
        foreach(CoTile t in coTiles)
        {
            t.Destroy();
        }
        base.Destroy();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach (CoTile t in coTiles)
        {
        //    t.Draw(gameTime,spriteBatch);
        }
        base.Draw(gameTime, spriteBatch);
    }
}

