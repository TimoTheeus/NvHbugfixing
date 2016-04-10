using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

//Class that returns the assets with the given assetnames as objects that can be used by the game. (Like textures and sounds).
public class AssetLoader
{
    protected ContentManager content;
    protected PathFinder pathFinder;

    public AssetLoader(ContentManager content)
    {
        this.content = content;
        Console.WriteLine("Root directory: " + content.RootDirectory);

        pathFinder = new PathFinder("Content/Paths/assetPaths.txt");
        
    }
    //Returns sprite with the given assetname.
    public Texture2D GetSprite(string assetName)
    {
        if (assetName == "")
            return null;

        return content.Load<Texture2D>(pathFinder.GetTexturePath(assetName));
    }
    //Returns path.
    public string GetPath(string assetName)
    {
        return pathFinder.GetTexturePath(assetName);
    }
    //Plays a sound with given assetname.
    public void PlaySound(string assetName)
    {
        if (assetName == "")
            return;
        SoundEffect snd = content.Load<SoundEffect>(pathFinder.GetSoundPath(assetName));
        snd.Play(GameSettings.SoundVolume, 0, 0);
    }
    //Plays music with given assetname and repeats.
    public void PlayMusic(string assetName)
    {
        if (assetName == "")
            return;

        Song song = content.Load<Song>(pathFinder.GetSongPath(assetName));
        if (song == null) return;
        MediaPlayer.Stop();
        MediaPlayer.IsRepeating = true;
        MediaPlayer.Play(song);
    }
    //Returns a font with given assetname.
    public SpriteFont GetFont(string assetName)
    {
        if (assetName == "")
            return null;

        return content.Load<SpriteFont>(pathFinder.GetFontPath(assetName));
    }

    public void PlayPlaylist(string[] p)
    {
        if (p == null)
            return;
        foreach (string assetName in p)
        {
            Song song = content.Load<Song>(pathFinder.GetSongPath(assetName));
            if (song == null) return;
            MediaPlayer.Stop();
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(song);
        }
    }
}

