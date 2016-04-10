using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

class Shop : SpriteGameObject
{
    GameObjectList stock;
    List<int> priceList;

    public Shop(string assetName, int sheetIndex, string id = "", int layer = 0) : base("shop", 1, id, layer)
    {
        Console.WriteLine("shop made at " + this.position);
        stock = new GameObjectList();
        priceList = new List<int>();
        DetermineStock();
    }

    void DetermineStock()
    {
        for (int i = 0; i < 3; i++)
        {
            int itemNr = R.Dice(1);//TODO: add the total item numbers
            AddItemToStock(itemNr, i);
        }
    }

    void AddItemToStock(int number, int i)
    {
        Items item;
        switch (number)
        {
            case 1:
            default:
                item = new KnockBackScroll();
                stock.Add(item);
                priceList.Add(10);
                break;
        }
        item.Position = new Vector2(200 + 200 * i, 200);
        GameData.LevelObjects.Add(item);
    }

    public GameObjectList Stock
    {
        get { return stock; }
    }

    public void BuyItem(GameObject item)
    {
        stock.Remove(item);
    }
}
