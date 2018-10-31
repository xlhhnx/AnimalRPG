using AnimalRPG.Systems.Maps.Pathfinding;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace AnimalRPG.Systems.Maps
{
    public class TileMap
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Tile[,] Tiles { get; set; }

        public TileMap(int width, int height)
        {
            Width = width;
            Height = height;
            Tiles = new Tile[ Width , Height ];

            GenerateMap();
        }

        public void Draw( SpriteBatch spriteBatch )
        {
            for ( var y = 0; y < Height; y++ )
            {
                for ( var x = 0; x < Width; x++ )
                {
                    Tiles[ x , y ].Draw( spriteBatch );
                }
            }
        }

        public void GenerateMap()
        {
            for ( int y = 0; y < Height; y++ )
            {
                for ( int x = 0; x < Width; x++ )
                {
                    Tiles[ x , y ] = new Tile( x , y );
                }
            }
        }

        public bool ContainsPosition( Vector2 position )
        {
            var boundingBox = new Rectangle( new Point() , (new Vector2( Width , Height ) * 32).ToPoint() );
            return boundingBox.Contains( position );
        }

        public List<Tile> GetAdjacent( Tile tile ) => GetAdjacent( tile.X , tile.Y );
        public List<Tile> GetAdjacent( int x , int y )
        {
            var tiles = new Tile[ 4 ];

            // Left
            tiles[ 0 ] = x > 0 ? Tiles[ x - 1 , y ] : null;
            // Right
            tiles[ 1 ] = x < Width - 1 ? Tiles[ x + 1 , y ] : null;
            // Up
            tiles[ 2 ] = y > 0 ? Tiles[ x , y - 1 ] : null;
            // Down
            tiles[ 3 ] = y < Height - 1 ? Tiles[ x , y + 1 ] : null;

            return tiles.Where(t => !ReferenceEquals(t,null)).ToList();
        }

        public List<Tile> GetDiagonal( Tile tile ) => GetDiagonal( tile.X , tile.Y );
        public List<Tile> GetDiagonal( int x , int y )
        {
            var tiles = new Tile[ 4 ];

            // Up & Left
            tiles[ 0 ] = x > 0 && y > 0? Tiles[ x - 1 , y - 1 ] : null;
            // Down & Right
            tiles[ 1 ] = x < Width - 1 && y < Height - 1 ? Tiles[ x + 1 , y + 1 ] : null;
            // Up & Right
            tiles[ 2 ] = x < Width && y > 0 ? Tiles[ x + 1, y - 1 ] : null;
            // Down & Left
            tiles[ 3 ] = x > 0 && y < Height - 1 ? Tiles[ x - 1 , y + 1 ] : null;

            return tiles.Where(t => !ReferenceEquals(t,null)).ToList();
        }

        public List<Tile> GetNeighbors( Tile tile )
        {
            var tiles = GetAdjacent( tile );
            tiles.AddRange( GetDiagonal( tile ) );
            return tiles;
        }

        public List<Tile> GetNeighbors( int x , int y )
        {
            var tiles = GetAdjacent( x , y );
            tiles.AddRange( GetDiagonal( x , y ) );
            return tiles;
        }

        public bool AreAdjacent( Tile a , Tile b ) => AreAdjacent( a.MapIndex , b.MapIndex );
        public bool AreAdjacent( Point a , Point b )
        {
            return (b - a).ToVector2().Length() == 1f;
        }

        public SearchRegion<Tile> OpenUniformCost( Vector2 position , int move ) => OpenUniformCost( (int)position.X , (int)position.Y , move );
        public SearchRegion<Tile> OpenUniformCost( int x , int y , int move )
        {
            if ( x >= 0
                && y >= 0
                && x < Width
                && y < Height )
            {
                var startTile = Tiles[ x , y ];
                return this.OpenUniformCost( startTile , move );
            }
            else
            {
                return new SearchRegion<Tile>( new TileEqualityComparer() );
            }
        }

        public Path<Tile> AStar( Vector2 start , Vector2 end ) => AStar( (int)start.X , (int)start.Y , (int)end.X , (int)end.Y );
        public Path<Tile> AStar( int xStart , int yStart , int xEnd , int yEnd )
        {
            if ( xStart >= 0
                && yStart >= 0
                && xStart < Width
                && yStart < Height 
                && xEnd >= 0
                && yEnd >= 0
                && xEnd < Width
                && yEnd < Height )
            {
                var startTile = Tiles[ xStart , yStart ];
                var endTile = Tiles[ xEnd , yEnd ];
                return this.AStar( startTile , endTile );
            }
            else
            {
                return new Path<Tile>( new TileEqualityComparer() );
            }
        }

        public Tile TryGetTile( Vector2 position ) => TryGetTile( (int)position.X , (int)position.Y );
        public Tile TryGetTile( int x , int y )
        {
            if ( x >= 0
                && y >= 0
                && x < Width
                && y < Height )
            {
                return Tiles[ x , y ];
            }
            else
            {
                return null;
            }
        }
    }
}
