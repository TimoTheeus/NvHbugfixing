using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

public class GameObjectGrid : GameObject
{
    protected GameObject[,] grid;
    public int cellWidth, cellHeight, columns, rows;

    public GameObjectGrid(int columns, int rows, int cellWidth, int cellHeight, string id = "")
        : base(id)
    {
        grid = new GameObject[columns, rows];
        for (int x = 0; x < columns; x++)
            for (int y = 0; y < rows; y++)
                grid[x, y] = null;

        this.columns = columns;
        this.rows = rows;
        this.cellWidth = cellWidth;
        this.cellHeight = cellHeight;
    }

    public virtual void Add(GameObject obj, int x, int y)
    {
        grid[x, y] = obj;
        obj.Parent = this;

        obj.Position = new Vector2(x * cellWidth, y * cellHeight);
    }

    public GameObject[,] Objects
    {
        get
        {
            return grid;
        }
    }

    public int Columns
    {
        get { return grid.GetLength(0); }
    }

    public int Rows
    {
        get { return grid.GetLength(1); }
    }

    public int CellWidth
    {
        get { return cellWidth; }
        set { cellWidth = value; }
    }

    public int CellHeight
    {
        get { return cellHeight; }
        set { cellHeight = value; }
    }
    public override void Update(GameTime gameTime)
    {
        foreach (GameObject obj in grid)
            if (obj != null)
                obj.Update(gameTime);
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        foreach (GameObject obj in grid)
            if(obj!= null)
            obj.HandleInput(inputHelper);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach (SpriteGameObject obj in grid)
            if(obj!= null)
            obj.Draw(gameTime, spriteBatch);
    }

    public override string getActionOutput()
    {
        string ls = "";
        foreach (GameObject obj in grid)
        {
            string s = obj.getActionOutput();
            if (s != null)
            {
                ls += ";" + s;
            }
        }
        return ls;
    }
}




