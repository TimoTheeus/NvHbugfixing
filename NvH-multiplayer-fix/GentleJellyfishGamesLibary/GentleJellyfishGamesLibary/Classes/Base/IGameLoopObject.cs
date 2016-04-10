using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//All methods needed for the gameloop are in this interface.
public interface IGameLoopObject
{
    void HandleInput(InputHelper inputHelper);

    void Update(GameTime gameTime);

    void Draw(GameTime gameTime, SpriteBatch spriteBatch);

    void Reset();

}

