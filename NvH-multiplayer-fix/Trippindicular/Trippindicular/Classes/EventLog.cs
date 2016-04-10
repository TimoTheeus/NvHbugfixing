using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

class EventLog : GameObjectList
{
    public EventLog() : base(10, "eventLog")
    {
        Objects = new List<GameObject>(6);
        for(int i = 0; i < 6; i++)
            Objects.Add(new TextGameObject("smallFont"));
    }

    public void Add(string target, string attacker, bool building, bool spell)
    {
        TextGameObject prompt = new TextGameObject("smallFont");
        if(!building && !spell)
            switch(GameWorld.Random.Next(7))
            {
                case 0:
                    prompt.Text = attacker + " cashed in " + target + "'s chips";
                    break;
                case 1:
                    prompt.Text = target + " was no match for " + attacker + "'s fists of fury";
                    break;
                case 2:
                    prompt.Text = attacker + " iced " + target;
                    break;
                case 3:
                    prompt.Text = attacker + " slaughtered " + target + " in a crazed frenzy";
                    break;
                case 4:
                    prompt.Text = attacker + " decided the world wasn't big enough for " + target;
                    break;
                case 5:
                    prompt.Text = target + " didn't have what it took to take on " + attacker;
                    break;
                case 6:
                    prompt.Text = target + " got pummeled joyfully, jauntily and utterly onesidedly by " + attacker;
                    break;
            }
        else if(building && !spell)
        {
            switch(GameWorld.Random.Next(4))
            {
                case 0:
                    prompt.Text = target + " crumbled under " + attacker + "'s might";
                    break;
                case 1:
                    prompt.Text = attacker + " crushed " + target + " like a bulldozer";
                    break;
                case 2:
                    prompt.Text = target + " wasn't built with " + attacker + " in mind";
                    break;
                case 3:
                    prompt.Text = attacker + " found a structural weakness in " + target;
                    break;
            }
        }

        else if(spell && !building)
        {
            switch(GameWorld.Random.Next(4))
            {
                case 0:
                    prompt.Text = target + " couldn't get away from the " + attacker + " in time";
                    break;
                case 1:
                    prompt.Text = "the " + attacker + " didn't turn out too well for " + target;
                    break;
                case 2:
                    prompt.Text = target + " earned a nomination for the Darwin awards by dying to a " + attacker;
                    break;
                case 3:
                    prompt.Text = "I'm out of ideas, " + target + " died to a " + attacker + ", what do you want from me?";
                    break;

            }
        }

        for(int i = 4; i >= 0; i--)
        {
            if(Objects[i] != null)
            {
                Objects[i].Position = new Vector2(0, 20 * (i + 1));
                Objects[i + 1] = Objects[i];
            }
        }
        prompt.Position = new Vector2(0, 0);
        Objects[0] = prompt;
    }
}
