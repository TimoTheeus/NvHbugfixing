using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Net;
using System;

namespace Trippindicular.Classes
{
    class SessionsMenuState : GameObjectList
    {

        protected Button joinGame, options, exitGame, sessions,thisisjustfortextdisplay;
        protected SpriteGameObject background,nameBar;
        protected PlayingState playingState;
        protected UserInput input;


        public SessionsMenuState()
        {
            //Initialize playingState
            playingState = GameWorld.GameStateManager.GetGameState("playing") as PlayingState;
            this.Add(new MenuCursor());
            //Background
            background = new SpriteGameObject("menuBackground");
            this.Add(background);

            //Make buttons
            //New Game
            joinGame = new Button("button", "buttonFont", "font", 0, "Join Game", 0);
            joinGame.Position = new Vector2(300, 150);
            this.Add(joinGame);

            //Options
            options = new Button("button", "buttonFont", "font", 0, "Options", 0);
            options.Position = new Vector2(300, 280);
            this.Add(options);

            //Options
            exitGame = new Button("button", "buttonFont", "font", 0, "Exit", 0);
            exitGame.Position = new Vector2(300, 410);
            this.Add(exitGame);

            //nameBar
            //nameBar = new SpriteGameObject("nameBar");
            //nameBar.Position= new Vector2(100, 540);
            //this.Add(nameBar);
            //UseInput
            input = new UserInput();
            input.Position = new Vector2(150, 560);
            this.Add(input);
            //Text cuz textgameobject didnt work for some reason
            thisisjustfortextdisplay = new Button("sliderBack", "buttonFont", "font", 0, "Insert Opponent's IP down below", 1);
            thisisjustfortextdisplay.Position = new Vector2(300, 510);
            this.Add(thisisjustfortextdisplay);



        }


        public override void Reset()
        {
            base.Reset();
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            //base
            base.HandleInput(inputHelper);

            //Buttons
            if (joinGame.Pressed&&input!= null)
            {
                //Do something with input.Text
                string ip = null;
                if (input.Text.Length > 6)
                {
                    ip ="192.168.1.3";
                }
                playingState.Initialize(GameData.Host, ip);
                GameWorld.GameStateManager.SwitchTo("hud");
            }
            else if (options.Pressed)
            {
                GameWorld.GameStateManager.GetGameState("settingsMenuTitle").Reset();
                GameWorld.GameStateManager.SwitchTo("settingsMenuTitle");
            }
            else if (exitGame.Pressed)
            {
                GameWorld.GameStateManager.SwitchTo("titleMenu");
            }

        }
    }
}
