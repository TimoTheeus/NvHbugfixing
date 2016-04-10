using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

/// <summary>
/// This class draws the game with a heads-up-display over it. On this display the player will find any information he or she needs.
/// </summary>
public class HUD : IGameLoopObject
{
    protected IGameLoopObject playingState;

    //HUD
    public GameObjectList hud;
    public HUD()
    {
        playingState = GameWorld.GameStateManager.GetGameState("playing");

        //HUD
        hud = new GameObjectList(0, "HUD");
    }

    public void HandleInput(InputHelper inputHelper)
    {
        playingState.HandleInput(inputHelper);
        hud.HandleInput(inputHelper);
    }

    public void Update(GameTime gameTime)
    {
        playingState.Update(gameTime);
        hud.Update(gameTime);

    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        playingState.Draw(gameTime, spriteBatch);
        spriteBatch.End();
        spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, GameWorld.ScaleMatrix);
        hud.Draw(gameTime, spriteBatch);
    }

    public void Reset()
    {
        playingState.Reset();
        hud.Reset();
    }
}
