using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Ecology.World
{
    class Carnivore : Herbavore
    {
        private const float HUNGER_THRESHOLD = 75f;

        private Animal _target;

        public Carnivore(World world) : base(world)
        {
            this.DoEat = (gameTime) =>
            {
                // Find suitable plantlife to consume
                // This would of course be the closest plant (will add other qualifiers in future)
                if (_target == null)
                {
                    _target = (Animal)this.World.LivingThings.Where(l => l is Animal)
                        .OrderBy(l => Vector2.Distance(this.Position, l.Position)).FirstOrDefault();

                    if (_target != null)
                        this.MoveTo(_target.Position);
                }
                else if (Vector2.Distance(_target.Position, this.Position) <= 1f)
                {
                    // Consume the plant for its energy
                    this.Energy += _target.Energy;

                    _target.Kill();

                    _target = null;
                }
            };
        }


        public override Needs CalculateNeeds()
        {
            Needs needs = Needs.Desire;


            if (this.Energy <= HUNGER_THRESHOLD)
            {
                needs |= Needs.Eat;
            }


            return needs;
        }
    }
}
