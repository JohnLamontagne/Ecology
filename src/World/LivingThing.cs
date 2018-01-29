using System;
using Microsoft.Xna.Framework;

namespace Ecology.World
{
    public abstract class LivingThing : WorldObject
    {
        public string Name { get; set; }

        public float Health { get; set; }

        public float Energy { get; set; }

        public float GrowthRate { get; set; }

        public bool Alive { get; protected set; }

        public Action<GameTime> DoReproduce { get; set; }

        protected World World;

        protected LivingThing(World world)
        {
            this.Alive = true;
            this.World = world;
        }

        public abstract void Kill();
    }
}
