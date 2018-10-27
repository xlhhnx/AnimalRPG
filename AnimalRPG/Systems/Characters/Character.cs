using AnimalRPG.Systems.Characters.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalRPG.Systems.Characters
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Dictionary<string , BaseStat> BaseStats;
        public Dictionary<string , ResourceStat> ResourceStats;

        public Character( int id , string name )
        {
            Id = id;
            Name = name;

            BaseStats = new Dictionary<string , BaseStat>();
            ResourceStats = new Dictionary<string , ResourceStat>();
        }

        public void AddBaseStats( params BaseStat[] stats )
        {
            foreach ( var s in stats )
            {
                if ( BaseStats.ContainsKey( s.Name ) )
                    throw new ArgumentException( $"Base Stat {s.Name}({s.Id}) is already in Character {Name}({Id}) thus cannot be added again." );
                else
                    BaseStats.Add( s.Name , s );
            }
        }

        public void AddResourceStats( params ResourceStat[] stats )
        {
            foreach ( var s in stats )
            {
                if ( ResourceStats.ContainsKey( s.Name ) )
                    throw new ArgumentException( $"Resouce Stat {s.Name}({s.Id}) is already in Character {Name}({Id}) thus cannot be added again." );
                else
                    ResourceStats.Add( s.Name , s );
            }
        }
    }
}
