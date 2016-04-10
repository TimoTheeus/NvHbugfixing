using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System.IO;

//Class used for all Settings of the game (resolution, sound, fullscreen, vsync etc.).
static public class GameSettings
{
    static public float MusicVolume;
    static public float SoundVolume;
    static public float VoiceVolume;
    static public Point Resolution;
    static public Vector2 Scale;
    static public bool Fullscreen;
    static public bool VSync;

    //Several universal constants.
    public const float GameWidth = 1920f;
    public const float GameHeight = 1080f;
    public const int TileWidth = 64;
    public const int TileHeight = 64;
    public const int GameFieldOffset = 32;
    static public Point DefaultResolution { get { return new Point(1280, 720); } }

    //A few private vars.
    const string path = "Content/Settings/settings.txt";
    static SettingsLoader settings;

    //Property for the right scaling when drawing.
    static public Matrix ScaleMatrix
    {
        get
        {
            float screenScale = (float)GameWorld.graphics.GraphicsDevice.Viewport.Width / GameWidth;
            float screenScaleY= (float)GameWorld.graphics.GraphicsDevice.Viewport.Height / GameHeight;
            return Matrix.CreateScale(screenScale, screenScaleY, 1);
        }
    }

    //Method to initialize graphic settings (before starting the game).
    static public void Initialize()
    {
        settings = new SettingsLoader(path);
        try
        {
            Resolution = new Point(int.Parse(settings.GetSetting("width")), int.Parse(settings.GetSetting("height")));
        }
        catch
        {
            Log.Write(LogType.ERROR, "Error loading resolution settings, using default settings.");
            Resolution = DefaultResolution;
        }
        Fullscreen = bool.Parse(settings.GetSetting("fullscreen"));
        VSync = bool.Parse(settings.GetSetting("vsync"));
        MusicVolume = float.Parse(settings.GetSetting("musicvolume"));
        SoundVolume = float.Parse(settings.GetSetting("soundvolume"));
        VoiceVolume = float.Parse(settings.GetSetting("voicevolume"));

        ApplySettings();
    }

    //Applies the settings where needed (Mostly to the graphicsdevice).
    static public void ApplySettings()
    {
        GameWorld.graphics.SynchronizeWithVerticalRetrace = VSync;
        GameWorld.graphics.IsFullScreen = Fullscreen;
        GameWorld.graphics.PreferredBackBufferWidth = Resolution.X;
        GameWorld.graphics.PreferredBackBufferHeight = Resolution.Y;
        GameWorld.graphics.ApplyChanges();

        GameWorld.ScaleMatrix = ScaleMatrix;
        MediaPlayer.Volume = MusicVolume;
        Scale = new Vector2((Resolution.X / GameWidth), (Resolution.Y / GameHeight));

        SaveSettings();
    }

    //Saves settings to the settings file.
    static void SaveSettings()
    {
        List<string> lines = new List<string>();
        lines.Add("height:" + Resolution.Y.ToString());
        lines.Add("width:" + Resolution.X.ToString());
        lines.Add("fullscreen:" + Fullscreen.ToString());
        lines.Add("soundvolume:" + SoundVolume.ToString());
        lines.Add("musicvolume:" + MusicVolume.ToString());
        lines.Add("voicevolume:" + VoiceVolume.ToString());
        lines.Add("vsync:" + VSync.ToString());

        try
        {
            StreamWriter writer = new StreamWriter(path, false);
            for (int i = 0; i<lines.Count; i++)
            {
                writer.WriteLine(lines[i]);
            }
            writer.Close();
        }
        catch (Exception e)
        {
            Log.Write(LogType.ERROR, "Error occured when saving settings: " + e.Message);
        }
    }    

    //Changes the game to fullscreen (Temporarily, doesnt change the settings file).
    static public void SetFullscreen(bool b)
    {
        GameWorld.graphics.IsFullScreen = b;
        GameWorld.graphics.ApplyChanges();
        Fullscreen = b;
    }

    //Changes the music volume temporarily.
    static public void ChangeVolume()
    {
        MediaPlayer.Volume = MusicVolume;
    }

}

