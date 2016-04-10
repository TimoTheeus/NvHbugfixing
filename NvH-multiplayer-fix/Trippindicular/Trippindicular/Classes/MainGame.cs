using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Trippindicular.Classes;
using Microsoft.Xna.Framework.GamerServices;
using Lidgren.Network;

//This is the main class, the game gets started from here.
class MainGame : GameWorld
{

    static void Main()
    {
        MainGame game = new MainGame();
        game.Run();
    }



    public MainGame()
    {
        Log.Initialize();
        Content.RootDirectory = "Content";
        this.IsMouseVisible = false;
        

        Log.Write(LogType.INFO, "Build Directory: " + Testing.AssemblyDirectory);
        Console.WriteLine("Build Directory: " + Testing.AssemblyDirectory);
    }

    protected override void LoadContent()
    {
        base.LoadContent();

        gameStateManager.AddGameState("playing", new PlayingState());
        gameStateManager.AddGameState("hud", new HUD());
        gameStateManager.AddGameState("titleMenu", new TitleMenuState());
        gameStateManager.AddGameState("hostLobby", new HostLobbyState());
        gameStateManager.AddGameState("sessionsMenu", new SessionsMenuState());
        gameStateManager.AddGameState("settingsMenuTitle", new SettingsMenuOverlay(gameStateManager.GetGameState("titleMenu")));
        gameStateManager.AddGameState("settingsMenuPause", new SettingsMenuOverlay(gameStateManager.GetGameState("pauseMenu")));
        gameStateManager.AddGameState("finish", new FinishState());
        gameStateManager.AddGameState("factionMenuState", new FactionMenuState());
        gameStateManager.SwitchTo("titleMenu");
    }

    protected override void Update(GameTime gameTime)
    {
        //this.ProcessNetworkMessages();
        base.Update(gameTime);
    }


}

