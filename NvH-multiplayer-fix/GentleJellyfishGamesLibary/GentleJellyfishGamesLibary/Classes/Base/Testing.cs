using System;
using System.IO;
using System.Reflection;
using Microsoft.Xna.Framework;

//Class used for some help while testing the game (like buildpath, for logs see Log.cs).
static public class Testing
{
    public static int curFPSCount;
    public static int FPSCount;
    public static float timePast;
    public static string FPSCounter { get { return FPSCount.ToString(); } }

    //Property to get the Directory the build is in.
    public static string AssemblyDirectory
    {
        get
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
    }

    //Update used for updating the FPS counter.
    public static void UpdateFPS(GameTime gameTime)
    {
        curFPSCount++;
        timePast += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (timePast >= 1.0f)
        {
            timePast = 0;
            FPSCount = curFPSCount;
            curFPSCount = 0;
            Log.Write(LogType.INFO, "Current FPS: " + FPSCounter);
        }
    }
}

