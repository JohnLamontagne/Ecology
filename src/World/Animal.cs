using System;
using Ecology.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ecology.World
{
    public abstract class Animal : LivingThing
    {
        private const int GROWTH_THRESHHOLD = 75;
      
        private Vector2 _targetPosition;
        private bool _moving;

        public Color Color { get; set; }

        public float Speed { get; set; }

        public float MetabolicRate { get; set; }

        public bool HasShelter { get; set; }

        public Vector2 Size { get; set; }

        protected Animal(World world)
            :base (world)
        {
        }

        public Action<GameTime> DoEat { get; set; }

        public Action<GameTime> DoFindShelter { get; set; }

        public Action<GameTime> DoDesire { get; set; }

        public abstract Needs CalculateNeeds();

        public void MoveTo(Vector2 position)
        {
            _targetPosition = position;
            _moving = true;
        }

        private void ProcessMovement(GameTime gameTime)
        {
           if (!_moving)
                return;

            Vector2 delta = _targetPosition - this.Position;

            delta.Normalize();

            this.Position += delta * (float)gameTime.ElapsedGameTime.TotalMilliseconds * this.Speed;

            if (this.Position == _targetPosition)
            {
                _moving = false;
            }
        }

        public virtual void Grow()
        {
            this.Size = new Vector2(this.Size.X + this.GrowthRate, this.Size.Y + this.GrowthRate);
        }

        public override void Update(GameTime gameTime)
        {
            this.Energy -= this.MetabolicRate;

            if (this.Energy <= 0)
            {
                this.Kill();
                return;
            }

            if (this.Energy >= GROWTH_THRESHHOLD)
                this.Grow();

            // Determine what state we're in based on needs met
            Needs needs = this.CalculateNeeds();

            this.ProcessMovement(gameTime);

            if (needs.IsSet(Needs.Eat))
            {
                this.DoEat?.Invoke(gameTime);

                return;
            }

            if (needs.IsSet(Needs.Shelter))
            {
                this.DoFindShelter?.Invoke(gameTime);

                return;
            }

            if (needs.IsSet(Needs.Produce))
            {
                this.DoReproduce?.Invoke(gameTime);

                return;
            }

            if (needs.IsSet(Needs.Desire))
            {
                this.DoDesire?.Invoke(gameTime);
            }
        }

        public override void Kill()
        {
            this.Alive = false;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.DrawRectangle(this.Position, this.Size, this.Color, this.Size.X);
        }
    }
}
