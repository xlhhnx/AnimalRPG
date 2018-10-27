using AnimalRPG.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace AnimalRPG.Systems.Maps
{
    public class Terrain
    {
        public Image Image { get; set; }
        public string Name { get; set; }
        public float MoveCost { get; set; }
        public float DefenseRating { get; set; }

        public void Draw( SpriteBatch spriteBatch )
        {
            Image.Draw( spriteBatch );
        }
    }
}
