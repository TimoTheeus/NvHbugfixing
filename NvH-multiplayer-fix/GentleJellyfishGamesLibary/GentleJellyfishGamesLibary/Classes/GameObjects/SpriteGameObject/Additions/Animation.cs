using System;
using Microsoft.Xna.Framework;

//Class used for an animation, it is a spritesheet that changes it's sheetindex when playing, at a certain speed.
public class Animation : SpriteSheet
{
    protected float frameTime;
    protected bool isLooping;
    protected float time;

    //Readonly properties.
    public float FrameTime { get { return frameTime; } }
    public bool IsLooping { get { return isLooping; } }
    public int CountFrames { get { return NumberSprites; } }
    public bool AnimationEnded { get { return !isLooping && sheetIndex >= NumberSprites - 1; } }

    public Animation(string assetName, bool isLooping, float frameTime = 0.1f) : base(assetName)
    {
        this.frameTime = frameTime;
        this.isLooping = isLooping;
    }
    //Method to start the animation.
    public void Play()
    {
        sheetIndex = 0;
        time = 0.0f;
    }
    //Updates the time on a frame and checks if it should move to the next frame.
    public void Update(GameTime gameTime)
    {
        time += (float)gameTime.ElapsedGameTime.TotalSeconds;
        while (time > frameTime)
        {
            time -= frameTime;
            if (isLooping)
                sheetIndex = (sheetIndex + 1) % NumberSprites;
            else
                sheetIndex = Math.Min(sheetIndex + 1, NumberSprites - 1);
        }
    }


}

