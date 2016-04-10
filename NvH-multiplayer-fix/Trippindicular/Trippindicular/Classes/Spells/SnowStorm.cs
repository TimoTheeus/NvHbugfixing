using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

class SnowStorm : Spell
{
    public SnowStorm() : base("iceSpell", "snowStorm")
    {
        name = "SNOWSTORM";
        range = 300;
        timer = new Timer(5f);
    }

    public override void SpellEffect(GameTime gameTime, GameObject target)
    {
        ((Unit)target).Frozen = true;
    }

    public override void UndoSpellEffect(GameObject target)
    {
        ((Unit)target).Frozen = false;
    }
}
