using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Priority_Queue;

namespace AnimalRPG.Systems.Maps.Pathfinding
{
    public static class Hueristic 
    {
        public static SearchRegion<Tile> OpenUniformCost( this TileMap map , Tile startTile , int move )
        {
            var searchRegion = new SearchRegion<Tile>(new TileEqualityComparer());
            var closed = new List<Tile>();
            var open = new SimplePriorityQueue<Tile>();

            searchRegion.Add( startTile , null );
            open.Enqueue( startTile , 0 );
            while ( open.Count > 0 )
            {
                var parentPriority = open.GetPriority( open.First );
                var parentTile = open.Dequeue();
                
                var adjacent = map.GetAdjacent( parentTile )
                                  .Where(a => !closed.Contains(a));
                foreach ( var a in adjacent )
                {
                    var priority = a.MoveCost + parentPriority;
                    if ( priority <= move )
                    {
                        searchRegion.Add( a , parentTile );
                        open.Enqueue( a , priority );
                    }
                }
                closed.Add( parentTile );
            }

            return searchRegion;
        }

        public static Path<Tile> AStar( this TileMap map , Tile startTile , params Tile[] waypoint )
        {
            var path = new Path<Tile>(new TileEqualityComparer());

            var tiles = new List<Tile>() { startTile };
            tiles.AddRange( waypoint );

            for ( var i = 0; i < tiles.Count - 1; i++ )
            {
                var localPath = AStar( map , tiles[ i ] , tiles[ i + 1 ] );
                path.Join( localPath );
            }

            return path;
        }

        public static Path<Tile> AStar( this TileMap map , Tile startTile , Tile endTile )
        {
            var complete = false;
            var searchRegion = new SearchRegion<Tile>( new TileEqualityComparer() );
            var closed = new List<Tile>();
            var open = new SimplePriorityQueue<Tile>();

            open.Enqueue( startTile , 0 );
            while ( open.Count > 0 )
            {
                var parentPriority = open.GetPriority( open.First );
                var parentTile = open.Dequeue();

                if ( complete || parentTile == endTile )
                    break;

                var adjacent = map.GetAdjacent( parentTile )
                                  .Where( a => !closed.Contains( a ) );
                foreach ( var a in adjacent )
                {
                    searchRegion.Add( a , parentTile );
                    if ( a == endTile )
                    {
                        complete = true;
                        break;
                    }

                    var priority = a.MoveCost + parentPriority + ManhattanDistance( a.MapIndex , endTile.MapIndex );
                    open.Enqueue( a , priority );
                }

                closed.Add( parentTile );
            }

            return searchRegion.GetPath(endTile);
        }

        private static float ManhattanDistance( Point a , Point b )
        {
            return Math.Abs( a.X - b.X ) + Math.Abs( a.Y - b.Y );
        }
    }
}
