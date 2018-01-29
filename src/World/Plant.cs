using Ecology.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ecology.World
{
    public class Plant : LivingThing
    {
        private const int REPRODUCE_COOLDOWN = 5;
        private const double REPRODUCE_SUCC_CHANCE = .75f;

        private double _lastReproduceTime;

        public Color Color { get; set; }

        public Plant(World world)
            :base (world)
        {
            this.Color = Color.White;

            this.DoReproduce = (gameTime) =>
            {
                _lastReproduceTime += gameTime.ElapsedGameTime.TotalSeconds;

                if (_lastReproduceTime >= REPRODUCE_COOLDOWN)
                {
                    if (world.Random.NextDouble() <= REPRODUCE_SUCC_CHANCE)
                    {
                        var newPlant = new Plant(world)
                        {
                            Name = Name,
                            Energy = this.Energy
                        };

                        newPlant.Position = new Vector2((float)(this.World.Random.NextDouble() * this.World.Size.X),
                            (float)(this.World.Random.NextDouble() * this.World.Size.Y));

                        newPlant.Color = this.Color;

                        world.LivingThings.Add(newPlant);
                    }

                    _lastReproduceTime = 0;
                }
            };
        }

        public override void Update(GameTime gameTime)
        {
            this.DoReproduce(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.DrawCircle(this.Position, 5f, 100, this.Color, 5);
        }

        public override void Kill()
        {
            this.Alive = false;
        }
    }
}
