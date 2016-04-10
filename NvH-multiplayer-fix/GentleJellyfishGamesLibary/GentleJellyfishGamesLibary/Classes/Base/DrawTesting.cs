using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Class used to draw for testing purposes, this includes drawing hitboxes.
public class DrawTesting
{
    public static Texture2D pixel;

    //Bool used to shut down drawing testing items if needed temporarily.
    public static bool TurnedOff;

    public static void Initialize(GraphicsDevice graphics)
    {
        pixel = new Texture2D(graphics, 1, 1);
        pixel.SetData(new[] { Color.White });
        TurnedOff = true;
    }

    //This method draws an outline od a certain rectangle.
    public static void DrawHitbox(SpriteBatch spriteBatch, Rectangle r, Color col)
    {
        if (TurnedOff)
            return;

        int bw = 2;

        spriteBatch.Draw(pixel, new Rectangle(r.Left, r.Top, bw, r.Height), col);
        spriteBatch.Draw(pixel, new Rectangle(r.Right, r.Top, bw, r.Height), col);
        spriteBatch.Draw(pixel, new Rectangle(r.Left, r.Top, r.Width, bw), col);
        spriteBatch.Draw(pixel, new Rectangle(r.Left, r.Bottom, r.Width, bw), col);
    }
}
