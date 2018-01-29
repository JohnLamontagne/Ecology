using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ecology.World
{
    public class World
    {
        public List<LivingThing> LivingThings { get; }

        public Vector2 Size { get; set; }

        public Random Random { get; set; }

        public World()
        {
            this.LivingThings = new List<LivingThing>();

            this.Size = new Vector2(800, 600);

            this.Random = new Random();

            this.GeneratePlants(100);
            this.GenerateAnimals(10);
        }

        private void GenerateAnimals(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var animal = new Herbavore(this)
                {
                    Name = "Zebra",
                    Health = 100,
                    Energy = 100,
                    Speed = .2f,
                    MetabolicRate = 5f,
                    GrowthRate = .0f,
                    Size  = new Vector2(10, 10)
                };

                animal.Position = new Vector2((float)(this.Random.NextDouble() * this.Size.X),
                    (float)(this.Random.NextDouble() * this.Size.Y));

                animal.Color = new Color((int)((float)this.Random.NextDouble() * 255), (int)((float)this.Random.NextDouble() * 255), (int)((float)this.Random.NextDouble() * 255));

                this.LivingThings.Add(animal);
            }
        }

        private void GeneratePlants(int amount)
        { 
            for (int i = 0; i < amount; i++)
            {
                var wildOnion = new Plant(this)
                {
                    Health = 100,
                    Name = "Wild Onion",
                    Energy = 100
                };

                wildOnion.Position = new Vector2((float) (this.Random.NextDouble() * this.Size.X),
                    (float) (this.Random.NextDouble() * this.Size.Y));

                wildOnion.Color= new Color((int)((float)this.Random.NextDouble() * 255), (int)((float)this.Random.NextDouble() * 255), (int)((float)this.Random.NextDouble() * 255));

                this.LivingThings.Add(wildOnion);
            }
        }

        public void Update(GameTime gameTime)
        {
            int livingThings = this.LivingThings.Count;
            for (int i = 0; i < livingThings; i++)
            {
                LivingThings[i].Update(gameTime);
            }

            // Remove living things that are dead
            for (int i = this.LivingThings.Count - 1; i >= 0; i--)
            {
                if (!this.LivingThings[i].Alive)
                    this.LivingThings.RemoveAt(i);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var livingThing in this.LivingThings)
            {

                livingThing.Draw(spriteBatch, gameTime);
            }
              
        }
    }
}
