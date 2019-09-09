using System;

namespace Mimax.Cache
{
    [Flags]
    public enum RestrictionCategory
    {
        None = 0b_0000_0000_0000,
        Category1 = 0b_0000_0000_0001,
        Category2 = 0b_0000_0000_0010,
        Category3 = 0b_0000_0000_0100,
        Category4 = 0b_0000_0000_1000,
        Category5 = 0b_0000_0001_0000,
        Category6 = 0b_0000_0010_0000,
        Category7 = 0b_0000_0100_0000,
        Category8 = 0b_0000_1000_0000,
        Category9 = 0b_0001_0000_0000
    }
}