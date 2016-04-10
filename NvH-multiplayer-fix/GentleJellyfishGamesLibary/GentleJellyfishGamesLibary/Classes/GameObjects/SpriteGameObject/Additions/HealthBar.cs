using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

//Class that creates a health bar with changing colors, according to health level. (Used for enemy health).
public class HealthBar : GameObject
{
    protected Texture2D pixel;
    protected Rectangle drawArea;
    protected float health;
    protected Color color;
    public const int barWidth = 48;
    public const int barHeight = 10;
    protected float currWidth;

    public HealthBar(Vector2 centerPosition) : base("healthbar", 10)
    {
        position = centerPosition;
        health = 1;
        color = Color.Green;
        drawArea = new Rectangle((int)(position.X - 0.5 * barWidth), (int)(position.Y - 0.5 * barHeight), barWidth, barHeight);
        pixel = DrawTesting.pixel;
        currWidth = barWidth;
    }
    //Change the health to the percentage given (float between 0 and 1).
    public void ChangeHealth(float f)
    {
        if (f > 1 || f < 0)
        {
            Log.Write(LogType.WARNING, "Warning, tried to make a health bar out of bounds.");
        }
        currWidth = f * barWidth;

        if (f < 0.2)
            color = Color.Red;
        else if (f < 0.5)
            color = Color.Orange;
        else
            color = Color.Green;
    }
    //Update the position of the healthbar.
    public void Update(Vector2 pos)
    {
        position = pos;
        drawArea = new Rectangle((int)(position.X - 0.5 * currWidth), (int)position.Y - barHeight, (int)currWidth, barHeight);
    }
    
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (visible)
            spriteBatch.Draw(pixel, drawArea, color);
    }
}

