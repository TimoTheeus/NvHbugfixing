using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//This class keeps a list of other gameobjects, this is an easy way to sort them (For drawing) 
//and to cycle through them (to draw and update the objects) and to find an object in the list.
public class GameObjectList : GameObject
{
    protected List<GameObject> gameObjects;

    public List<GameObject> Objects
    {
        get { return gameObjects; }
        set { gameObjects = value; }
    }

    public GameObjectList(int layer = 0, string id = "") : base(id, layer)
    {
        gameObjects = new List<GameObject>();
    }
    //Method to add an object to the list.
    public void Add(GameObject obj)
    {
        obj.Parent = this;
        for (int i = 0; i < gameObjects.Count; i++)
        {
            if (gameObjects[i].Layer > obj.Layer)
            {
                gameObjects.Insert(i, obj);
                return;
            }
        }
        gameObjects.Add(obj);
    }
    //Method to remove an object from the list.
    public void Remove(GameObject obj)
    {
        gameObjects.Remove(obj);
        obj.Parent = null;
        foreach (GameObject o in gameObjects)
        {
            if (o is GameObjectList)
            {
                GameObjectList objList = o as GameObjectList;
                objList.Remove(obj);
            }
        }
    }
    //Method to find a specific object (With the id) in the list.
    public GameObject Find(string id)
    {
        foreach (GameObject obj in gameObjects)
        {
            if (obj.ID == id)
                return obj;
            if (obj is GameObjectList)
            {
                GameObjectList objlist = obj as GameObjectList;
                GameObject subobj = objlist.Find(id);
                if (subobj != null)
                    return subobj;
            }
        }
        return null;
    }
   
    //Handle the input for all objects in the list.
    public override void HandleInput(InputHelper inputHelper)
    {
        for (int i = gameObjects.Count - 1; i >= 0; i--)
            gameObjects[i].HandleInput(inputHelper);
    }
    //Update all objects in the list.
    public override void Update(GameTime gameTime)
    {
        for (int i = gameObjects.Count - 1; i >= 0; i--)
            gameObjects[i].Update(gameTime);
    }
    //Draw all objects in the list according to their own type.
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!visible)
            return;
        List<GameObject>.Enumerator e = gameObjects.GetEnumerator();
        while (e.MoveNext())
            e.Current.Draw(gameTime, spriteBatch);
    }
    //Reset all objects in the list.
    public override void Reset()
    {
        base.Reset();
        foreach (GameObject obj in gameObjects)
            obj.Reset();
    }

    public override string getActionOutput()
    {
        string ls = "";
        foreach (GameObject obj in gameObjects)
        {
            string s = obj.getActionOutput();
            if (s != null)
            {
                ls += ";"+s;
            }
        }
        return ls;
    }

}

