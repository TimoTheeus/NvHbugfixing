using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;

class Unit : SpriteGameObject
{
    protected float damage, maxHealth, health, speed, range,attackSpeed;
    protected Vector2 targetPosition;
    protected Point resourceCosts;
    public Unit targetUnit;
    public Building targetBuilding;
    bool selected;
    protected Timer attackTimer;
    protected Player.Faction faction;
    protected HealthBar healthBar;
    protected Timer checkIfInDiscoveredAreaTimer;
    protected bool inDiscoveredArea;
    protected Point mousePoint;
    protected string actionString;
    public string name;
    protected bool pacifist, frozen;
    protected const float meleeRange = 50;
    protected const float slowUnitSpeed = 150;
    protected bool repositioning;

    public bool IsRepositioning
    {
        get { return repositioning; }
    }
    public Unit TargetUnit
    {
        get { return targetUnit; }
        set { targetUnit = value; }
    }
    public Vector2 TargetPosition
    {
        get { return targetPosition; }
        set { targetPosition = value; }
    }
    public Point ResourceCosts
    {
        get { return resourceCosts; }
        set { resourceCosts = value; }
    }
    public bool InDiscoveredArea
    {
        get { return inDiscoveredArea; }
        set { inDiscoveredArea = value; }
    }

    public bool Frozen
    {
        get { return frozen; }
        set { frozen = value; }
    }

    public float AttackSpeed
    {
        get { return attackSpeed; }
        set { attackSpeed = value; }
    }
    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    public float Speed
    {
        get { return speed;  }
        set { speed = value; }
    }
    public float Range
    {

        get { return range; }
        set { range = value; }
    }
    public float Health
    {

        get { return health; }
        set { health = value; }
    }

    public Player.Faction Faction
    {
        get { return faction; }
        set { faction = value; }
    }
    public Unit(string assetName="",string id = "") : base(assetName,0, id,4)
    {
        actionString = null;
        this.Origin = this.sprite.Center;
        speed = slowUnitSpeed;
        range = meleeRange;
        maxHealth = 100;
        damage = 20;
        this.health = maxHealth;
        selected = false;
        attackSpeed = 1;
        attackTimer = new Timer(this.AttackSpeed);
        healthBar = new HealthBar(new Vector2(position.X, position.Y + sprite.Height / 2 + 10));
        checkIfInDiscoveredAreaTimer = new Timer((1 / 6));
        //RemoveMenu();
        repositioning = false;
    }

    public override void HandleInput(InputHelper ih)
    {
        mousePoint = new Point((int)(ih.MousePosition.X + GameWorld.Camera.Pos.X), (int)(ih.MousePosition.Y + GameWorld.Camera.Pos.Y));

        if (selected)
        {
            GameData.Cursor.HasClickedTile = false;
        }
        if (BoundingBox.Contains(mousePoint))
        {
            if (ih.LeftButtonPressed() && faction == GameData.player.GetFaction)
            {
                selected = true;
                GameData.Cursor.ClickedUnit = this;
            }
        }
        else if (!this.BoundingBox.Contains(mousePoint) )
        {
            if (ih.LeftButtonPressed())
            {
                selected = false;
                if(GameData.Cursor.ClickedUnit== this)
                GameData.Cursor.ClickedUnit = null;
                //action = null
                
            }
        }
        if (this.selected)
        {
            if (ih.RightButtonPressed())
            {
                //action != null
                RightClickAction();
            }
        }
    }

    public override void Update(GameTime gameTime)
    {
        actionString = null;
        checkIfInDiscoveredAreaTimer.Update(gameTime);
        if (checkIfInDiscoveredAreaTimer.Ended)
        {
            Point p = new Point((int)this.Position.X, (int)this.Position.Y);
            if (GameData.LevelGrid.GetTile(p) != null)
            {
                if (GameData.LevelGrid.GetTile(p).Discovered && !GameData.LevelGrid.GetTile(p).IsDark)
                    InDiscoveredArea = true;
                else InDiscoveredArea = false;
            }
        }
        if (!frozen && (Faction == Player.Faction.humanity || Faction == Player.Faction.nature))
        {
            attackTimer.Update(gameTime);
            if (targetUnit != null)
            {
                MoveToUnit();
            }


            else if (targetPosition != Vector2.Zero)
            {
                MoveToTile();
            }
            base.Update(gameTime);
        }
        healthBar.Update(new Vector2(position.X, position.Y - sprite.Height / 2 - 10));
        healthBar.ChangeHealth((float)((health / maxHealth) * 1.5));
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (InDiscoveredArea)
        {
            if (selected||faction==GameData.player.OppositeFaction)
            {
                healthBar.Draw(gameTime, spriteBatch);
            }
            base.Draw(gameTime, spriteBatch);
        }
    }
    protected void RemoveMenu()
    {
        Menu menu = GameData.LevelObjects.Find("barracksMenu") as Menu;
        if (menu != null)
            GameData.LevelObjects.Remove(menu);
    }

    protected void MoveToTile()
    {
        float differenceXPos = Math.Abs(targetPosition.X - this.GlobalPosition.X);
        float differenceYPos = Math.Abs(targetPosition.Y - this.GlobalPosition.Y);
        float xvelocity = 0;
        float yvelocity = 0;
        double angle = 0;
        float marginForError = 2;
        angle = Math.Atan((differenceXPos / differenceYPos));
        xvelocity = (float)(Math.Sin(angle) * this.Speed);
        yvelocity = (float)(Math.Cos(angle) * this.Speed);
        if (targetPosition.X >= this.GlobalPosition.X)
        {
            if (targetPosition.Y < this.GlobalPosition.Y)
            {
                this.Velocity = new Vector2(xvelocity, -yvelocity);
            }
            else if (targetPosition.Y > this.GlobalPosition.Y)
            {
                this.Velocity = new Vector2(xvelocity, yvelocity);
            }
        }
        else if (targetPosition.X <= this.GlobalPosition.X)
        {
            if (targetPosition.Y < this.GlobalPosition.Y)
            {
                this.Velocity = new Vector2(-xvelocity, -yvelocity);
            }
            else if (targetPosition.Y > this.GlobalPosition.Y)
            {
                this.Velocity = new Vector2(-xvelocity, yvelocity);
            }
        }
        if ((targetBuilding != null && (Math.Sqrt(Math.Pow(differenceXPos, 2) + Math.Pow(differenceYPos, 2)) <= range) || 
            (differenceXPos < marginForError && differenceXPos > -marginForError) &&(differenceYPos< marginForError && differenceYPos>-marginForError)))
        {
            if(targetBuilding != null)
            {
                ArrivedAtBuildingAction();
            }

            else
            {
                ArrivedAtTileAction();
            }
            
        }
       
    }
    protected void MoveToUnit()
    {
        float differenceXPos = Math.Abs(targetUnit.Position.X - this.GlobalPosition.X);
        float differenceYPos = Math.Abs(targetUnit.Position.Y - this.GlobalPosition.Y);
        float xvelocity = 0;
        float yvelocity = 0;
        double angle = 0;
        angle = Math.Atan((differenceXPos / differenceYPos));
        xvelocity = (float)(Math.Sin(angle) * this.Speed);
        yvelocity = (float)(Math.Cos(angle) * this.Speed);
        if (targetUnit.Position.X >= this.GlobalPosition.X)
        {
            if (targetUnit.Position.Y < this.GlobalPosition.Y)
            {
                this.Velocity = new Vector2(xvelocity, -yvelocity);
            }
            else if (targetUnit.Position.Y > this.GlobalPosition.Y)
            {
                this.Velocity = new Vector2(xvelocity, yvelocity);
            }
        }
        else if (targetUnit.Position.X <= this.GlobalPosition.X)
        {
            if (targetUnit.Position.Y < this.GlobalPosition.Y)
            {
                this.Velocity = new Vector2(-xvelocity, -yvelocity);
            }
            else if (targetUnit.Position.Y > this.GlobalPosition.Y)
            {
                this.Velocity = new Vector2(-xvelocity, yvelocity);
            }
        }
        if ((differenceXPos < Range && differenceXPos > -Range) && (differenceYPos < Range && differenceYPos > -Range))
        {
            this.Velocity = Vector2.Zero;
            if (attackTimer.Ended)
            {
                Attack();
            }
        }


    }
    public virtual void Attack()
    {
        if(targetUnit != null)
        {
            if (targetUnit.Health <= 0)
            {
                this.actionString = "$unit:" + targetUnit.id + "$damg:" + this.damage + "," + this.ID;
                targetUnit = null;
                return;
            }  
            if (this.Faction.Equals(GameData.player.GetFaction))
            {
                this.actionString = "$unit:" + targetUnit.id + "$damg:"+this.damage+","+this.ID;
                targetUnit.DealDamage(this.Damage, this);
            }
            
        }

        else 
        {
            if (targetBuilding.Health <= 0)
            {
                this.actionString = "$bdng:" + targetBuilding.ID + "$damg:" + this.damage + "," + this.ID;
                targetBuilding = null;
                return;
            } 
            targetBuilding.DealDamage(this.Damage, this);

            this.actionString = "$bdng:" + targetBuilding.ID + "$damg:" + this.damage + "," + this.ID;
        }
        
        attackTimer.Reset();
    }

    public void DealDamage(float amount, GameObject attacker)
    {        
        this.Health -= amount;
        if (Health <= 0)
        {
            if(attacker is Unit)
                ((GameWorld.GameStateManager.GetGameState("hud") as HUD).hud.Find("eventLog") as EventLog).Add(this.name, (attacker as Unit).name, false, false);
            if(attacker is Spell)
                ((GameWorld.GameStateManager.GetGameState("hud") as HUD).hud.Find("eventLog") as EventLog).Add(this.name, (attacker as Spell).name, false, true);
            Die();
        }
        /*
        if(targetUnit== null && pacifist != true)
        {
            if(attacker is Unit)
            {
                Unit target = attacker as Unit;
                targetUnit = target;
                actionString += "$targ:" + target.ID;
                attackTimer.Reset();
                ArrivedAtTileAction();
            }
        }*/
    }
    protected virtual void Die()
    {
        this.actionString = "$unit:" + this.ID + "$dead:true";
        this.selected = false;
        GameData.Cursor.ClickedUnit = null;
        GameData.Units.Remove(this);
    }
    public virtual void UpdateDiscoveredArea()
    {
        if(faction == GameData.player.GetFaction)
            foreach(Tile t in GameData.LevelGrid.Objects)
            {
                Vector2 distance = new Vector2(Math.Abs(this.GlobalPosition.X - t.Position.X), Math.Abs(this.GlobalPosition.Y - t.Position.Y));
                double absDistance = Math.Sqrt(Math.Pow(distance.X, 2) + Math.Pow(distance.Y, 2));
                if (absDistance < 300)
                {
                    t.Discovered = true;
                    t.IsDark = false;
                }

                
            }
    }
    protected virtual void ClickOnEmptyTileAction()
    {
        try
        {
            targetPosition = GameData.Cursor.CurrentTile.Position;
        }
        catch
        {
            targetPosition = GameData.selectedTile.Position;
        }
        targetBuilding = null;
    }
   
    protected void checkCurrentPosition()
    {
        for (int i = 0; i < GameData.Units.Objects.Count; i++)
        {
            if (GameData.Units.Objects[i] != null&&GameData.Units.Objects[i]!=this)
            {
                if ((GameData.Units.Objects[i].Position.X-this.Position.X>-5&& GameData.Units.Objects[i].Position.X - this.Position.X<5)&&
                    (GameData.Units.Objects[i].Position.Y - this.Position.Y > -5 && GameData.Units.Objects[i].Position.Y - this.Position.Y < 5)&&!((Unit)GameData.Units.Objects[i]).IsRepositioning)
                {
                    targetPosition = this.Position + new Vector2(20, 20);
                    repositioning = true;
                    this.actionString = "$unit:"+this.ID+"$move:" + this.targetPosition.X + "," + this.targetPosition.Y;
                    return;
                }
            }
        }
        repositioning = false;
    }
    protected virtual void ArrivedAtBuildingAction()
    {
        if (attackTimer.Ended)
        {
            Attack();
        }
        this.Velocity = Vector2.Zero;
    }
    protected virtual void ArrivedAtTileAction()
    {
        targetPosition = Vector2.Zero;
        this.Velocity = Vector2.Zero;
        checkCurrentPosition();
    }
    protected virtual void RightClickAction()
    {
        actionString = "$unit:"+this.ID;
        if (!pacifist)
        {
            for (int i = 0; i < GameData.Units.Objects.Count; i++)
            {
                if (GameData.Units.Objects[i] is Unit)
                {
                    Unit unit = GameData.Units.Objects[i] as Unit;
                    if (unit.BoundingBox.Contains(mousePoint) && unit.faction != this.faction)
                    {
                        actionString += "$move:" + "0,0" + "$targ:" + unit.ID ;
                        targetPosition = Vector2.Zero;
                        targetUnit = unit;
                        break;
                    }
                    else targetUnit = null;
                }
            }

            if (targetUnit == null)
            {
                if (GameData.Cursor.CurrentTile is Building&& (GameData.Cursor.CurrentTile as Building).Faction!=this.faction)
                {
                    targetUnit = null;
                    if (((Building)GameData.Cursor.CurrentTile).Faction != GameData.player.GetFaction)
                    {
                        targetBuilding = (Building)GameData.Cursor.CurrentTile;
                        actionString += "$build:" + targetBuilding.ID;
                    }
                    targetPosition = GameData.Cursor.CurrentTile.Position;
                    actionString += "$tgbd:" + targetBuilding.ID + "$move:" + targetPosition.X +","+ targetPosition.Y;
                }
                else
                {
                    ClickOnEmptyTileAction();
                    actionString += "$move:" + GameData.selectedTile.Position.X + "," + GameData.selectedTile.Position.Y;
                }
            }
        }
        else
        {
            ClickOnEmptyTileAction();
            actionString += "$move:" + GameData.selectedTile.Position.X + "," + GameData.selectedTile.Position.Y;
        }
    }

    public override string getActionOutput()
    {
        
        return actionString;
    }

    internal void SetTargetUnit(string targetID)
    {
        this.targetUnit = (Unit) GameData.Units.Find(targetID);
        MoveToUnit();
    }
}

