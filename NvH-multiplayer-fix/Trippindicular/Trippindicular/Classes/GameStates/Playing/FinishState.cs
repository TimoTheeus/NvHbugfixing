using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


public class FinishState : GameObjectList
{
    IGameLoopObject hud;

    protected SpriteGameObject background;
    protected Button exitMenu;

    public FinishState()
    {
        hud = GameWorld.GameStateManager.GetGameState("hud");
        exitMenu = new Button("button", "buttonFont", "font", 0, "Return to menu");
        exitMenu.Position = new Vector2((GameSettings.GameWidth / 2), GameSettings.GameHeight - (exitMenu.Height * 3.0f));
        this.Add(exitMenu);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (exitMenu.Pressed)
        {
            for (int i = 0; i < GameData.LevelObjects.Objects.Count; i++)
            {
                GameData.LevelObjects.Objects[i].Reset();
                GameData.LevelObjects.Objects[i] = null;
            }
            GameWorld.GameStateManager.GetGameState("titleMenu").Reset();
            GameWorld.GameStateManager.SwitchTo("titleMenu");

        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Reset()
    {
        hud.Reset();
        base.Reset();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        hud.Draw(gameTime, spriteBatch);
        base.Draw(gameTime, spriteBatch);
    }
}
