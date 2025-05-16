using System;

[Flags]
public enum AnimalType
{
    None = 1 << 0,
    Lion = 1 << 1,
    Chicken = 1 << 2,
    ButterFly = 1 << 3,
    Elephant = 1 << 4,
    Wolf = 1 << 5,
    Fox = 1 << 6,
    Stag = 1 << 7
}
