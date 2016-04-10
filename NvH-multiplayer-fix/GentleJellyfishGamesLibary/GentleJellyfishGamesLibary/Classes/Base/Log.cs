using System;
using System.IO;

//Types of log messages.
public enum LogType
{
    ERROR = 0,
    WARNING = 1,
    INFO = 2
}
//This is a class that writes messages to a html file, with chosen priorities (Error, Warning or Info) in the colors red, orange, black.
public class Log
{
    static public string saveFile;
    static public bool locked;

    //Initialize settings.
    static public void Initialize()
    {
        //You can find the log in Trippindicular/bin/x86/Debug/log_day_hour_minute.html.
        saveFile = "log_" + DateTime.Now.DayOfWeek + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + ".html";
        StreamWriter output = new StreamWriter(new FileStream(saveFile, FileMode.Create, FileAccess.Write));
        output.WriteLine("<span style=\"font - family: &quot; Kootenay & quot; ; color: #000000;\">");
        output.WriteLine("Log started at " + DateTime.Now.ToLongTimeString() + "</span><hr/> ");
        output.Close();
        Write(LogType.ERROR, "Testing if the error works!");
        Write(LogType.WARNING, "Testing if the warning works!");
        Write(LogType.INFO, "Testing if info works.");
        locked = true;
    }

    //Write a string of the given type to the log file.
    static public void Write(LogType type, string s)
    {
        if (locked)
            return;
        string text = "";
        switch(type)
        {
            case LogType.ERROR:
                text = "<span style=\"color: #ff0000;\">"; break; //Red color.
            case LogType.WARNING:
                text = "<span style=\"color: #ffa500;\">"; break; //Orange color.
            case LogType.INFO:
                text = "<span style=\"color: #000000;\">"; break; //Black color.
        }
        text += DateTime.Now.ToLongTimeString() + " : " + s + "</span><br/>";

        //Try to write the string to the file, if it doesn't work, write an error to the Console.
        try
        {
            StreamWriter output = new StreamWriter(new FileStream(saveFile, FileMode.Append, FileAccess.Write));
            output.WriteLine(text);
            output.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error occured while writing to the log file : " + e.Message);
        }
    }
}

