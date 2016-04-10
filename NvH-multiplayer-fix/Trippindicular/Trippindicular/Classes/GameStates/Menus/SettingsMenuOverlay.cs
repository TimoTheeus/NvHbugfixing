using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

//Adjust all settings
class SettingsMenuOverlay : GameObjectList
{
    protected IGameLoopObject titleMenuState;

    //TEMP
    protected DropDown scroll;
    protected TextGameObject options;
    protected SliderButton musicSlider, soundEffectSlider, voiceOverSlider;
    protected CheckBox fullscreen, vsync;
    protected DropMenu resolution;
    protected Button apply;

    protected GameObjectList settings;

    public SettingsMenuOverlay(IGameLoopObject state)
    {
        titleMenuState = state;
        this.Add(new Cursor());
        //Create button list
        settings = new GameObjectList(0, "buttons");

        //Scroll
        scroll = new DropDown("scroll", new Vector2(600, -1000));
        scroll.Drop = true;
        scroll.Speed = 600;
        settings.Add(scroll);

        //Menu text
        options = new TextGameObject("buttonFont", 1);
        options.Text = "Options";
        options.Position = scroll.Position + new Vector2(150, 50);
        settings.Add(options);

        //Music Slider
        musicSlider = new SliderButton("sliderBack", "sliderFront", 1, "Music Volume:");
        musicSlider.Position = scroll.Position + new Vector2(150, 150);
        musicSlider.Value = GameSettings.MusicVolume;
        settings.Add(musicSlider);

        //SoundEffect Slider
        soundEffectSlider = new SliderButton("sliderBack", "sliderFront", 1, "Sound Volume:");
        soundEffectSlider.Position = scroll.Position + new Vector2(150, 250);
        soundEffectSlider.Value = GameSettings.SoundVolume;
        settings.Add(soundEffectSlider);

        //VoiceOver Slider
        voiceOverSlider = new SliderButton("sliderBack", "sliderFront", 1, "Voice Volume:");
        voiceOverSlider.Position = scroll.Position + new Vector2(150, 350);
        voiceOverSlider.Value = GameSettings.VoiceVolume;
        settings.Add(voiceOverSlider);

        //Fullscreen CheckBox
        fullscreen = new CheckBox("checkBox", "buttonFont", 0, "Toggle Fullscreen:");
        fullscreen.Position = scroll.Position + new Vector2(150, 450);
        fullscreen.Check = GameSettings.Fullscreen;
        settings.Add(fullscreen);

        //VSync CheckBox
        vsync = new CheckBox("checkBox", "buttonFont", 0, "Toggle VSync:");
        vsync.Position = scroll.Position + new Vector2(150, 570);
        vsync.Check = GameSettings.VSync;
        settings.Add(vsync);

        //Resolution DropDown
        resolution = new DropMenu("dropMenu", "buttonFont", 3, 0, "Resolution:");
        resolution.Position = scroll.Position + new Vector2(150, 690);

        switch (GameSettings.Resolution.X)
        {
            case 1920:
                resolution.Options[0].Text = "1900px X 1080px";
                break;

            case 1600:
                resolution.Options[0].Text = "1600px X 900px";
                break;

            case 1280:
                resolution.Options[0].Text = "1280px X 720px";
                break;

            default:
                break;
        }
        
        resolution.Options[1].Text = "1920px X 1080px";
        resolution.Options[2].Text = "1600px X 900px";
        resolution.Options[3].Text = "1280px X 720px";

        settings.Add(resolution);

        //Apply button
        apply = new Button("smallButton", "font", "smallFont", 0, "Apply");
        apply.Position = scroll.Position + new Vector2(850, 750);
        settings.Add(apply);

        this.Add(settings);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        titleMenuState.HandleInput(inputHelper);
        settings.HandleInput(inputHelper);

        if (apply.Pressed)
        {
            GameSettings.ApplySettings();
        }
    }

    public override void Update(GameTime gameTime)
    {
        titleMenuState.Update(gameTime);

        //Update positions
        options.Position = scroll.Position + new Vector2(150, 50);
        musicSlider.Position = scroll.Position + new Vector2(150, 150);
        soundEffectSlider.Position = scroll.Position + new Vector2(150, 250);
        voiceOverSlider.Position = scroll.Position + new Vector2(150, 350);
        fullscreen.Position = scroll.Position + new Vector2(150, 450);
        vsync.Position = scroll.Position + new Vector2(150, 570);
        resolution.Position = scroll.Position + new Vector2(150, 690);
        apply.Position = scroll.Position + new Vector2(850, 750);

        //Update volume settings
        GameSettings.MusicVolume = musicSlider.Value;
        GameSettings.SoundVolume = soundEffectSlider.Value;
        GameSettings.VoiceVolume = voiceOverSlider.Value;

        GameSettings.ChangeVolume();

        //Update checkboxes
        GameSettings.Fullscreen = fullscreen.Check;
        GameSettings.VSync = vsync.Check;

        //Update dropMenu
        switch (resolution.Options[0].Text)
        {
            case "1920px X 1080px" :
                GameSettings.Resolution = new Point(1920, 1080);
                break;

            case "1600px X 900px":
                GameSettings.Resolution = new Point(1600, 900);
                break;

            case "1280px X 720px":
                GameSettings.Resolution = new Point(1280, 720);
                break;

            case "640px X 360px":
                GameSettings.Resolution = new Point(640, 360);
                break;

            default:
                break;
        }

        settings.Update(gameTime);
    }

    public override void Reset()
    {
        settings.Reset();
        titleMenuState.Reset();

        musicSlider.Value = GameSettings.MusicVolume;
        soundEffectSlider.Value = GameSettings.SoundVolume;
        voiceOverSlider.Value = GameSettings.VoiceVolume;

        fullscreen.Check = GameSettings.Fullscreen;
        vsync.Check = GameSettings.VSync;

        apply.Position = new Vector2(-1000, -1000);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);

        titleMenuState.Draw(gameTime, spriteBatch);
        settings.Draw(gameTime, spriteBatch);
    }
}