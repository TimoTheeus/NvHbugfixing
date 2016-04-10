using System;
using System.Collections.Generic;
using System.IO;

//This class loads txt files and adds every line to the List text. (Except for lines that start with #, they are comments.)
public class FileLoader
{
    protected List<string> text;

    public FileLoader(string path)
    {
        this.text = LoadFile(path);
    }

    //Method to load the file, you can also use this to load another file afterwards.
    static public List<string> LoadFile(string path)
    {
        List<string> text = new List<string>();
        try
        {
            StreamReader file = new StreamReader(path);
            string line = file.ReadLine();

            while (line != null)
            {
                //If the line starts with '#', don't add the line as it is a comment.
                if (line[0] == '#')
                {
                    line = file.ReadLine();
                    continue;
                }
                text.Add(line);
                line = file.ReadLine();
            }
            file.Close();
        }
        catch (Exception e)
        {
            Log.Write(LogType.ERROR, "Error occured while loading an important file: " + e.Message);
        }
        return text;
    }
        
}

