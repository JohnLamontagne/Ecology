using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecology.World
{
    [Flags]
    public enum Needs
    {
        None = 0x0,
        Produce = 0x1,
        Eat = 0x2,
        Shelter = 0x4,
        Desire = 0x8
    }

    public static class NeedsExtensions
    {
        public static bool IsSet(this Needs needs, Needs flags)
        {
            return (needs & flags) == flags;
        }

        public static Needs Clear(this Needs needs, Needs flags)
        {
            return needs & (~flags);
        }
    }
}
