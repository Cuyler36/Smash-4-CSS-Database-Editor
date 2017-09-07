using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smash_Character_Database_Editor
{
    public static class Extensions
    {
        // Clamp
        public static T Clamp<T>(this T Value, T Minimum, T Maximum) where T : IComparable<T>
        {
            if (Value.CompareTo(Minimum) < 0)
                return Minimum;
            else if (Value.CompareTo(Maximum) > 0)
                return Maximum;
            return Value;
        }
    }
}
