using System;
using System.Collections.Generic;

//Class used to get the paths needed to load assets, translating an assetname to a path.
public class PathFinder : FileLoader
{
    protected List<string[]> splitText;

    public PathFinder(string path) : base(path)
    {
        splitText = new List<string[]>();

        foreach (string s in text)
        {
            splitText.Add(s.Split('-'));
        }
    }
    //Returns a path that corresponds to the given assetname.
    public string GetPath(string assetName)
    {
        foreach (string[] s in splitText)
        {
            if (s[0] == assetName)
            {
                return s[1];
                
            }
        }
        Log.Write(LogType.ERROR, "Error occured while loading asset, returning placeholder, asset was not found! assetName: " + assetName);
        return "";
    }
    //Returns the path corresponding to the texture with given assetname.
    public string GetTexturePath(string assetName)
    {
        string temp = GetPath(assetName);
        if (temp == "")
            return "Tilesets/selectedTile";
        return temp;
    }
    //Returns the path corresponding to the sound with given assetname.
    public string GetSoundPath(string assetName)
    {
        string temp = GetPath(assetName);
        if (temp == "")
            return "SFX/placeholder";
        return temp;
    }
    //Returns path to font with given assetname.
    public string GetFontPath(string assetName)
    {
        string temp = GetPath(assetName);
        if (temp == "")
            return "Fonts/font";
        return temp;
    }
    //Returns music path.
    public string GetSongPath(string assetName)
    {
        string temp = GetPath(assetName);
        if (temp == "")
            return "Music/menumusic";
        return temp;
    }
}

