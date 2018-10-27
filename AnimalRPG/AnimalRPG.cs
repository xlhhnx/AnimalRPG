using AnimalRPG.Input;
using AnimalRPG.Input.Controllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AnimalRPG
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class AnimalRPG : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        InputManager _inputManager;

        public AnimalRPG()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            _inputManager = new InputManager();
            var keyboardController = new KeyboardController(0);
            var mouseController = new MouseController(1);
            _inputManager.Controllers.Add( keyboardController.Id , keyboardController );
            _inputManager.Controllers.Add( mouseController.Id , mouseController );

            // DISABLED
            #region Input Debugging
            //KeyboardController.KeyDown += ( i , k ) =>
            //{
            //    string name = i == 0 ? "KEYBOARD" : "MOUSE";
            //    System.Console.WriteLine( $"{name}({i}) : {k} DOWN" );
            //};
            //KeyboardController.KeyPress += ( i , k ) =>
            //{
            //    string name = i == 0 ? "KEYBOARD" : "MOUSE";
            //    System.Console.WriteLine( $"{name}({i}) : {k} PRESS" );
            //};
            //KeyboardController.KeyRelease += ( i , k ) =>
            //{
            //    string name = i == 0 ? "KEYBOARD" : "MOUSE";
            //    System.Console.WriteLine( $"{name}({i}) : {k} RELEASE" );
            //};

            //MouseController.ButtonDown += ( i , b , v ) =>
            //{
            //    string name = i == 0 ? "KEYBOARD" : "MOUSE";
            //    System.Console.WriteLine( $"{name}({i}) : {b} DOWN {v.X},{v.Y}" );
            //};
            //MouseController.ButtonPress += ( i , b , v ) =>
            //{
            //    string name = i == 0 ? "KEYBOARD" : "MOUSE";
            //    System.Console.WriteLine( $"{name}({i}) : {b} PRESS {v.X},{v.Y}" );
            //};
            //MouseController.ButtonRelease += ( i , b , v ) =>
            //{
            //    string name = i == 0 ? "KEYBOARD" : "MOUSE";
            //    System.Console.WriteLine( $"{name}({i}) : {b} RELEASE {v.X},{v.Y}" );
            //};
            //MouseController.MouseMove += ( i , v ) =>
            //{
            //    string name = i == 0 ? "KEYBOARD" : "MOUSE";
            //    System.Console.WriteLine( $"{name}({i}) : MOVE {v.X},{v.Y}" );
            //};
            //MouseController.MouseScroll += (i1, i2, v) =>
            //{
            //    string name = i1 == 0 ? "KEYBOARD" : "MOUSE";
            //    System.Console.WriteLine( $"{name}({i1}) : {i2} SCROLL {v.X},{v.Y}" );
            //};
            #endregion

            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }
        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        
        protected override void Update(GameTime gameTime)
        {
            _inputManager.Update();

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
