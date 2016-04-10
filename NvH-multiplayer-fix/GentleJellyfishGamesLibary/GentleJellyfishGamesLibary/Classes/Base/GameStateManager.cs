using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Class used to keep track of the state the game is in and to switch between states. (Also makes sure the gameloop uses the right states).
public class GameStateManager
{
    Dictionary<string, IGameLoopObject> gameStates;
    IGameLoopObject currentGameState;

    public IGameLoopObject CurrentGameState { get { return currentGameState; } }

    public GameStateManager()
    {
        gameStates = new Dictionary<string, IGameLoopObject>();
        currentGameState = null;
    }
    //Add a new gamestate.
    public void AddGameState(string name, IGameLoopObject state)
    {
        gameStates[name] = state;
    }
    //Return a gamestate with the given name (Or write an error if that is not possible).
    public IGameLoopObject GetGameState(string name)
    {
        try { return gameStates[name]; }
        catch(Exception) { Log.Write(LogType.ERROR, "GameState not found! name: " + name); return null; }
    }
    //Switches to another gamestate with the given name.
    public void SwitchTo(string name)
    {
        if (gameStates.ContainsKey(name))
        {
            gameStates[name].Reset();
            currentGameState = gameStates[name];
        }
        else
            throw new KeyNotFoundException("Could not find game state: " + name);
    }
    //Handle input of the current gamestate.
    public void HandleInput(InputHelper inputHelper)
    {
        if (currentGameState != null)
            currentGameState.HandleInput(inputHelper);

    }
    //Update the current gamestate.
    public void Update(GameTime gameTime)
    {
        if (currentGameState != null)
            currentGameState.Update(gameTime);

    }
    //Draw the current gamestate.
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (currentGameState != null)
            currentGameState.Draw(gameTime, spriteBatch);
    }
    //Reset the current gamestate.
    public void Reset()
    {
        if (currentGameState != null)
            currentGameState.Reset();
    }
}

