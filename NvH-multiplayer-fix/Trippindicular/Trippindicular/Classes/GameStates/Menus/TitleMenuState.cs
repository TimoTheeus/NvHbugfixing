using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.GamerServices;

//Main menu, access to all sub-menus
class TitleMenuState : GameObjectList
{
    protected Button hostGame, joinGame, options, exitGame, sessions, singlePlayer;
    protected SpriteGameObject background;
    protected PlayingState playingState;
    PacketReader packetReader = new PacketReader();
    PacketWriter packetWriter = new PacketWriter();
    protected Button createGame;

    public TitleMenuState()
    {
        GameData.Host = true;

        this.Add(new MenuCursor());
        //Background
        background = new SpriteGameObject("menuBackground");
        this.Add(background);

        //Make buttons
                //New Game
                hostGame = new Button("button", "buttonFont", "font", 0, "Host game", 0);
                hostGame.Position = new Vector2(300, 150);
                this.Add(hostGame);
                //New Game
                joinGame = new Button("button", "buttonFont", "font", 0, "Join game", 0);
                joinGame.Position = new Vector2(700, 150);
                this.Add(joinGame);

                singlePlayer =  new Button("button", "buttonFont", "font", 0, "Single player", 0);
                singlePlayer.Position = new Vector2(700, 280);
                this.Add(singlePlayer);



                //Options
                options = new Button("button", "buttonFont", "font", 0, "Options", 0);
                options.Position = new Vector2(300, 280);
                this.Add(options);



                //Exit Game
                exitGame = new Button("button", "buttonFont", "font", 0, "Exit Game", 0);
                exitGame.Position = new Vector2(300, 540);
                this.Add(exitGame);

    }

    public override void Reset()
    {
        base.Reset();
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        //base
        base.HandleInput(inputHelper);

        if (inputHelper.KeyPressed(Keys.Home))
        {
           
        }
        //Buttons
        if (hostGame.Pressed)
        {
            GameData.SinglePlayer = true;
            GameData.Host = true;
            GameWorld.GameStateManager.SwitchTo("hostLobby");
        }
        else if (joinGame.Pressed)
        {
            GameData.SinglePlayer = true;
            GameData.Host = false;
            GameWorld.GameStateManager.SwitchTo("sessionsMenu");
        }
        else if (singlePlayer.Pressed)
        {
            GameData.Host = false;
            GameData.SinglePlayer = true;
            GameWorld.GameStateManager.SwitchTo("hostLobby");
        }
        else if (options.Pressed)
        {
            GameWorld.GameStateManager.GetGameState("settingsMenuTitle").Reset();
            GameWorld.GameStateManager.SwitchTo("settingsMenuTitle");
        }
        else if (exitGame.Pressed)
        {
            GameWorld.Exited = true;
        }
    }
}
