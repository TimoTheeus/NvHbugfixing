using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

//Struct that contains the information from highscores.
public struct HighscoreData
{
    public string[] Players;
    public string[] TimeString;
    public int[] Seconds;
    public int[] Minutes;
    public int[] EnemiesKilled;
    public int[] CoinsCollected;
    public int[] Ranks;

    public int Count;

    public HighscoreData(int count)
    {
        Count = count;
        Players = new string[count];
        TimeString = new string[count];
        Seconds = new int[count];
        Minutes = new int[count];
        EnemiesKilled = new int[count];
        CoinsCollected = new int[count];
        Ranks = new int[count];
    }
}

//Class to get highscores from the online database and to add new highscores to the database.
public class Highscores
{
    const string time = "Format=TOP10TIMES";
    const string enemies = "Format=TOP10KILLED";
    const string coins = "Format=TOP10COINS";
    const string urlReq = "http://diegokd143.143.axc.nl/requestscores.php";
    const string urlSend = "http://diegokd143.143.axc.nl/addscore.php";
    public const int entries = 10;
    public const int colNum = 5;

    //Method used to get the top list of the requested type of highscores.
    static public HighscoreData GetHighscores(string sortType = "time")
    {
        const string header = "START_";
        HighscoreData data;

        //First get the right POST type and get the data.
        string post = "";
        switch (sortType)
        {
            case "time": post = time; break;
            case "enemies": post = enemies; break;
            case "coins": post = coins; break;
            default: post = time; break;
        }
        string temp = MakeWebPost(urlReq, post).Trim();

        //Then check if the data is valid.
        if (temp.Length < header.Length || temp.Substring(0,header.Length)!= header)
        {
            Log.Write(LogType.ERROR, "Error: No valid header at the start of highscores! Returning empty highscores.");
            return new HighscoreData();
        }

        //Then actually split the data and put it in the HighscoreData.
        //Layout of data: 'START_Player_C_TimeSeconds_C_EnemiesKilled_C_CoinsCollected_C_Index_R_' etc. more rows after '_R_'.
        data = new HighscoreData(entries);
        temp = temp.Substring(header.Length);
        string[] rows = Regex.Split(temp, "_R_");
        for (int i = 0; i < entries; i++)
        {
            if (rows.Length > i && rows[i].Trim() != "")
            {
                string[] c = Regex.Split(rows[i], "_C_");
                if (c.Length == colNum)
                {
                    data.Players[i] = c[0];
                    int totalSeconds = int.Parse(c[1]);
                    int seconds = totalSeconds % 60;
                    int minutes = totalSeconds / 60;
                    string time = "";
                    if (minutes < 10)
                        time += "0";
                    time += minutes + ":";
                    if (seconds < 10)
                        time += "0";
                    time += seconds;
                    Log.Write(LogType.INFO, time); //TEMP TODO
                    data.TimeString[i] = time;
                    data.Seconds[i] = seconds;
                    data.Minutes[i] = minutes;
                    data.EnemiesKilled[i] = int.Parse(c[2]);
                    data.CoinsCollected[i] = int.Parse(c[3]);
                    data.Ranks[i] = int.Parse(c[4]);
                }
            }
        }
        return data;
    }

    //Method to get data from the requested url with given POST settings.
    static public string MakeWebPost(string url, string post)
    {
        const string reqMethod = "POST";
        const string contentType = "application/x-www-form-urlencoded";
        WebResponse response;
        string temp;

        //First initialize the request settings.
        WebRequest request = WebRequest.Create(url);
        request.Method = reqMethod;
        byte[] bytes = Encoding.UTF8.GetBytes(post);
        request.ContentType = contentType;
        request.ContentLength = bytes.Length;

        Stream stream;
        try
        {
            //Then try to request the data.
            stream = request.GetRequestStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();
            response = request.GetResponse();
            Log.Write(LogType.INFO, ((HttpWebResponse)response).StatusDescription);
        }
        catch (Exception e)
        {
            Log.Write(LogType.ERROR, "Error writing highscores to stream: " + e.Message);
            return "";
        }

        
        try
        {
            //Then try to read the response and put it in the temp string.
            stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            temp = reader.ReadToEnd();
            Log.Write(LogType.INFO, temp);
            reader.Close();
            stream.Close();
            response.Close();
        }
        catch (Exception e)
        {
            Log.Write(LogType.ERROR, "Error reading highscores from stream: " + e.Message);
            return "";
        }

        //Finally after closing all streams, return the read string.
        return temp;
    }

    //Method to add a score to the database, returns true when completed, false when failed.
    static public bool AddScore(string playerName, int timeSeconds, int enemiesKilled, int coinsCollected)
    {
        string temp = playerName + timeSeconds + enemiesKilled + coinsCollected + "xsw21qaz";
        string post = "PlayerName=" + playerName + "&TimeSeconds=" + timeSeconds + "&EnemiesKilled=" + enemiesKilled + "&CoinsCollected=" + coinsCollected + "&Hash=" + GetHash(temp);
        string response = MakeWebPost(urlSend, post);
        if (response.Contains("Succes"))
        {
            Log.Write(LogType.INFO, "Succesfully added an entry to the highscore database!");
            return true;
        }
        Log.Write(LogType.ERROR, "Error adding entry to highscores: " + response);
        return false;
    }

    //Method that makes the md5 security hash, used to verify highscore entries in the php script on the webserver.
    static string GetHash(string s)
    {
        MD5CryptoServiceProvider crypto = new MD5CryptoServiceProvider();
        byte[] bytes = Encoding.ASCII.GetBytes(s);
        bytes = crypto.ComputeHash(bytes);
        string hash = "";
        for (int i = 0; i < bytes.Length; i++)
        {
            hash += bytes[i].ToString("x2").ToLower();
        }
        return hash;
    }
}


