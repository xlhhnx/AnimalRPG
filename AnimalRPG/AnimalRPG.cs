using AnimalRPG.Display;
using AnimalRPG.Graphics;
using AnimalRPG.Input;
using AnimalRPG.Input.Controllers;
using AnimalRPG.Systems.Maps;
using AnimalRPG.Systems.Maps.Pathfinding;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AnimalRPG.Display.Controls;

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
        TileMap _tileMap;
        Anchor _anchor;
        Anchor _target;
        SearchRegion<Tile> _searchRegion;
        Path<Tile> _path;
        Button _exitButton;

        public AnimalRPG()
        {
            graphics = new GraphicsDeviceManager( this );
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Cursor.Initialize( new Vector2( 32 * 10 ) );

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            Camera.Position = new Vector2( -120 , -70 );
            Camera.Dimensions = new Vector2( GraphicsDevice.Viewport.Width , GraphicsDevice.Viewport.Width );
            Primitive.Initialize( GraphicsDevice );

            Cursor.Ghost = Primitive.CreateRectangle( 32 , 32 , new Color( 0 , 255 , 0 , 50 ) );

            _inputManager = new InputManager();
            var keyboardController = new KeyboardController( 0 );
            var mouseController = new MouseController( 1 );
            _inputManager.Controllers.Add( keyboardController.Id , keyboardController );
            _inputManager.Controllers.Add( mouseController.Id , mouseController );

            MouseController.OnButtonPress += HandleClick;

            _exitButton = new Button(
                Primitive.CreateRectangle( new Vector2( 80 , 30 ) , Color.Yellow ) ,
                Primitive.CreateRectangle( new Vector2( 80 , 30 ) , Color.Orange ) ,
                Primitive.CreateRectangle( new Vector2( 80 , 30 ) , Color.Red )
                )
            {
                Position = new Vector2( -100 , -50 ) ,
                Dimensions = new Vector2( 80 , 30 )
            };
            _exitButton.OnPress += Exit;

            _tileMap = new TileMap( 25 , 25 );

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
            spriteBatch = new SpriteBatch( GraphicsDevice );

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update( GameTime gameTime )
        {
            _inputManager.Update();

            base.Update( gameTime );
        }

        protected override void Draw( GameTime gameTime )
        {
            GraphicsDevice.Clear( Color.Black );

            spriteBatch.Begin( blendState: BlendState.NonPremultiplied );
            _tileMap.Draw( spriteBatch );

            if ( !ReferenceEquals( _searchRegion , null ) )
                _searchRegion.Draw( spriteBatch );

            if ( !ReferenceEquals( _path , null ) )
                _path.Draw( spriteBatch );
            
            if ( !ReferenceEquals( _anchor , null ) )
                _anchor.Draw( spriteBatch );
            
            if ( !ReferenceEquals( _target , null ) )
                _target.Draw( spriteBatch );

            _exitButton.Draw( spriteBatch );
            Cursor.Draw( spriteBatch );
            spriteBatch.End();

            base.Draw( gameTime );
        }

        private void HandleClick( int index , MouseButtons button , Vector2 position )
        {
            var pos = Camera.ConvertToWorldCoordinates( position );
            if ( !_tileMap.ContainsPosition( pos ) )
                return;

            if ( button == MouseButtons.Left )
            {
                var tmpPoint = (pos / 32).ToPoint();
                var correctedPosition = tmpPoint.ToVector2() * 32;

                if ( !ReferenceEquals( _anchor , null ) )
                {
                    var rectangle = new Rectangle( _anchor.Position.ToPoint() , new Point( 32 ) );
                    if ( rectangle.Contains( pos.ToPoint() ) )
                        _anchor = null;
                    else
                        _anchor = new Anchor( correctedPosition.X , correctedPosition.Y , Color.Red );
                }
                else
                {
                    _anchor = new Anchor( correctedPosition.X , correctedPosition.Y , Color.Red );
                }

                if ( !ReferenceEquals( _anchor , null ) )
                {
                    _searchRegion = _tileMap.OpenUniformCost( _anchor.Position / 32 , 35 );
                }
                else
                {
                    _searchRegion = null;
                }
            }

            if ( button == MouseButtons.Right )
            {
                var tmpPoint = (pos / 32).ToPoint();
                var correctedPosition = tmpPoint.ToVector2() * 32;

                if ( !ReferenceEquals( _target , null ) )
                {
                    var rectangle = new Rectangle( _target.Position.ToPoint() , new Point( 32 ) );
                    if ( rectangle.Contains( pos.ToPoint() ) )
                        _target = null;
                    else
                        _target = new Anchor( correctedPosition.X , correctedPosition.Y , Color.Blue );
                }
                else
                {
                    _target = new Anchor( correctedPosition.X , correctedPosition.Y , Color.Blue );
                }
            }

            if ( !ReferenceEquals( _anchor , null ) && !ReferenceEquals( _target , null ) && _searchRegion.ContainsItem( _tileMap.TryGetTile( _target.Position / 32 ) ) )
            {
                _path = _tileMap.AStar( _anchor.Position / 32 , _target.Position / 32 );
            }
            else
            {
                _path = null;
            }
        }
    }
}
