using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Lidgren.Network;


//Class with all basic loops and variables needed for any game.
public class GameWorld : Game
{
    static public GraphicsDeviceManager graphics;
    static public Matrix ScaleMatrix;
    protected SpriteBatch spriteBatch;
    protected InputHelper inputHelper;
    
    //Vars needed for saving data
    protected IAsyncResult result;

    protected static Point resolution;
    protected static AssetLoader assetLoader;
    protected static GameStateManager gameStateManager;
    protected static Random random;
    protected static Point screen;
    protected static Camera cam;
    protected static bool exited;

    //Readonly properties ({ get { return means get {return ...}(C# 6.0))
    public static Point Resolution
    {
        get
        { return resolution; }
    }
    public static AssetLoader AssetLoader { get { return assetLoader; } }
    public static Camera Camera { get { return cam; } }

    public static GameStateManager GameStateManager
    {
        get
        {
            return gameStateManager;
        }
    }

    //Property used to exit the game from anywhere
    public static bool Exited
    {
        set { exited = value; }
    }
    public static Point Screen
    {
        get { return screen; }
    }

    public static Random Random
    {
        get { return random; }
    }

    //Initialize the gameworld (also initializes settings).
    public GameWorld()
    {
        //this.Components.Add(new GamerServicesComponent(this));
        
        graphics = new GraphicsDeviceManager(this);
        GameSettings.Initialize();
        cam = new Camera();
        cam.ScaleMatrix = ScaleMatrix;
        gameStateManager = new GameStateManager();
        inputHelper = new InputHelper();
        random = new Random();
        screen = new Point(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);


    }
    //Method to load needed items
    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        assetLoader = new AssetLoader(Content);
        DrawTesting.Initialize(this.GraphicsDevice);
        
    }
    //Update the gamestatemanager and the testing class
    protected override void Update(GameTime gameTime)
    {
        gameStateManager.Update(gameTime);
        HandleInput();
    }
    //Handle input from the gamestatemanager
    protected void HandleInput()
    {
        inputHelper.Update();

        if (exited)
            this.Exit();
        gameStateManager.HandleInput(inputHelper);

        if (inputHelper.KeyPressed(Keys.F8))
            GameSettings.ApplySettings();
        if (inputHelper.KeyPressed(Keys.Delete))
        {


        }
        
    }
    //Clear the screen, then draw the gamestatemanager with the right scale
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Green);

        spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, cam.get_transformation(GraphicsDevice));
        gameStateManager.Draw(gameTime, spriteBatch);
        spriteBatch.End();
    }
}

