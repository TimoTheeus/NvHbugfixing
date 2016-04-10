using System.Collections.Generic;
using Microsoft.Xna.Framework;

//Class used for objects with animations, this is the basic class that loads and plays those animations. (On top of everything in spritegameobject.)
public class AnimatedGameObject : SpriteGameObject
{
    protected Dictionary<string, Animation> animations;
    protected Vector2 oldPosition;
    protected SpriteSheet hitboxSprite;

    //Readonly properties.
    public Animation Current { get { return sprite as Animation; } }
    public SpriteSheet HitboxSprite { get { return hitboxSprite; } }
    public Vector2 OldPosition { get { return oldPosition; } }

    public AnimatedGameObject(string id = "", int layer = 0) : base("", 1, id, layer)
    {
        animations = new Dictionary<string, Animation>();
        hitboxSprite = new SpriteSheet("hitboxVert");
    }
    //Load a new animation, with given settings.
    public void LoadAnimation(string assetname, string id, bool looping, float frametime = 0.1f)
    {
        Animation anim = new Animation(assetname, looping, frametime);
        animations[id] = anim;
    }
    //Play animation with the given id and mirror it if needed.
    public void PlayAnimation(string id)
    {
        if (sprite == animations[id])
            return;
        if (sprite != null)
            animations[id].Mirror = sprite.Mirror;
        animations[id].Play();
        sprite = animations[id];
        origin = new Vector2(sprite.Width / 2, sprite.Height);
    }

    //Update the animation and the position and checks if there is a solid collision after moving.
    public override void Update(GameTime gameTime)
    {
        if (sprite == null)
            return;
        Current.Update(gameTime);
        oldPosition = position;
        base.Update(gameTime);
    }
}

