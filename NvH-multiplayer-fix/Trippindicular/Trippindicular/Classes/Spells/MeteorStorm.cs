using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


class MeteorStorm : Spell
{

    public MeteorStorm() : base("meteorSpell", "meteorStorm")
    {
        name = "METEORSTORM";
        range = 200;
        this.timer = new Timer(5f);
    }

    public override void SpellEffect(GameTime gameTime, GameObject target)
    {
        ((Unit)target).DealDamage((float)(10 * gameTime.ElapsedGameTime.TotalSeconds), this);
    }
}

