using Microsoft.Xna.Framework;

//Class for a simple timer. (Times are in seconds.)
public class Timer : Controller
{
    protected double totalTime;
    protected double timeLeft;
    protected bool paused;
    protected bool ended;

    public double TimeLeft
    {
        get { return timeLeft; }
    }
    public bool Ended { get { return ended; } }

    public Timer(float timeInSeconds)
    {
        paused = false;
        ended = false;
        totalTime = timeInSeconds;
        timeLeft = totalTime;
    }
    //Update the time left if the timer is not paused.
    public override void Update(GameTime gameTime)
    {
        if (!paused)
        {
            timeLeft -= gameTime.ElapsedGameTime.TotalSeconds;
            if (timeLeft <= 0)
                ended = true;
        }
    }
    public void Pause()
    {
        paused = true;
    }
    public override void Reset()
    {
        timeLeft = totalTime;
        ended = false;
        paused = false;
    }
}

