using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

class FactionMenuState : GameObjectList
{
    public bool humanity, nature, otherHumanity, otherNature;
    protected Button humanityButton, natureButton, backButton, startButton;
    protected SpriteGameObject background;
    protected Timer timer;

    public FactionMenuState()
    {
        this.Add(new MenuCursor());
        background = new SpriteGameObject("menuBackground");
        this.Add(background);

        humanityButton = new Button("humanityButton", "buttonFont", "font", 0, "Humanity", 0);
        humanityButton.Position = new Vector2(300, 150);
        this.Add(humanityButton);

        natureButton = new Button("natureButton", "buttonFont", "font", 0, "Nature", 0);
        natureButton.Position = new Vector2(700, 150);
        this.Add(natureButton);

        backButton = new Button("backButton", "buttonFont", "font", 0, "Back", 0);
        backButton.Position = new Vector2(GameWorld.Screen.X / 2 - humanityButton.Width / 2 - 100, GameWorld.Screen.Y / 2 + humanityButton.Height / 2 + 100);
        this.Add(backButton);

        startButton = new Button("startButton", "buttonFont", "font", 0, "Start", 0);
        startButton.Position = new Vector2(GameWorld.Screen.X / 2, GameWorld.Screen.Y / 2 + humanityButton.Height / 2 + 100);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);

        if(humanityButton.Pressed && !otherHumanity)
        {
            humanity = !humanity;
        }

        if (natureButton.Pressed && !otherNature)
        {
            nature = !nature;
        }

        if (backButton.Pressed)
        {
            GameWorld.GameStateManager.SwitchTo("titleMenu");
        }

        if (startButton.Pressed)
        {
            if (timer == null)
            {
                timer = new Timer(10f);
            }
        }
    }

    public override void Update(GameTime gameTime)
    {
        if (((humanity && otherNature) || (otherHumanity && nature)) && !Objects.Contains(startButton))
        {
            this.Add(startButton);
        }
        else if (Objects.Contains(startButton) && !((humanity && otherNature) || (otherHumanity && nature)))
        {
            startButton.Text = "Start";
            Remove(startButton);
            timer = null;
        }

        if (timer != null)
        {
            timer.Update(gameTime);
            startButton.Text = ((int)timer.TimeLeft).ToString();
            if (timer.TimeLeft <= 0)
            {
                if (humanity)
                {
                    GameData.player.GetFaction = Player.Faction.humanity;
                }
                else
                    GameData.player.GetFaction = Player.Faction.nature;
                
                GameWorld.GameStateManager.SwitchTo("hud");
            }
        }
    }
}
