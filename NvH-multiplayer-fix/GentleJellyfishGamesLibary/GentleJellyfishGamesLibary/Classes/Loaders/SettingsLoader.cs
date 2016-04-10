using System.Collections.Generic;

//Class to load game settings from a settings.txt file.
public class SettingsLoader : FileLoader
{
    protected List<string[]> settingsList;

    public SettingsLoader(string path) : base(path)
    {
        settingsList = new List<string[]>();

        foreach (string s in text)
            settingsList.Add(s.Split(':'));
    }
    //Method to get a certain setting.
    public string GetSetting(string setting)
    {
        foreach (string[] s in settingsList)
            if (s[0] == setting)
                return s[1];
        Log.Write(LogType.ERROR, "Error, setting not found! " + setting);
        return "";
    }
}

