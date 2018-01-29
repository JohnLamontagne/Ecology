using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ecology.World
{
    public abstract class WorldObject
    {
        public Vector2 Position { get; set; }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
