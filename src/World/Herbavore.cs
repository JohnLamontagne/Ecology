using System.Linq;
using Microsoft.Xna.Framework;

namespace Ecology.World
{
    public class Herbavore : Animal
    {
        private const float HUNGER_THRESHOLD = 75f;

        private const int REPRODUCE_COOLDOWN = 2;

        private double _lastReproduceTime;

        private Plant _target;


        public Herbavore(World world) :
            base(world)
        {
            this.DoEat = (gameTime) =>
            {
                // Find suitable plantlife to consume
                // This would of course be the closest plant (will add other qualifiers in future)
                if (_target == null)
                {
                    _target = (Plant) this.World.LivingThings.Where(l => l is Plant)
                        .OrderBy(l => Vector2.Distance(this.Position, l.Position)).FirstOrDefault();

                    if (_target != null)
                        this.MoveTo(_target.Position);
                }
                else if (Vector2.Distance(_target.Position, this.Position) <= 5f)
                {
                    // Consume the plant for its energy
                    this.Energy += _target.Energy;

                    _target.Kill();

                    _target = null;
                }
            };

            this.DoReproduce = (gameTime) =>
            {
                _lastReproduceTime += gameTime.ElapsedGameTime.TotalSeconds;

                if (_lastReproduceTime >= REPRODUCE_COOLDOWN)
                {
                    var newHerbavore = new Herbavore(this.World)
                    {

                        Name = this.Name,
                        Health = 100,
                        Energy = 100,
                        Speed = this.Speed,
                        MetabolicRate = this.MetabolicRate,
                        GrowthRate = this.GrowthRate,
                        Size = this.Size
                    };
                    newHerbavore.Position = new Vector2((float) (this.World.Random.NextDouble() * this.World.Size.X),
                        (float) (this.World.Random.NextDouble() * this.World.Size.Y));

                    newHerbavore.Color = this.Color;

                    world.LivingThings.Add(newHerbavore);

                    _lastReproduceTime = 0;
                }

            };
        }

        public override Needs CalculateNeeds()
        {
            Needs needs = Needs.None;

            if (this.Energy <= HUNGER_THRESHOLD)
            {
                needs |= Needs.Eat;
            }
            else if (this.Energy >= HUNGER_THRESHOLD * 4)
            {
                needs |= Needs.Produce;
            }
            else
            {
                // Always default to eating
                needs |= Needs.Eat;
            }

        
            return needs;
        }

    }
}
