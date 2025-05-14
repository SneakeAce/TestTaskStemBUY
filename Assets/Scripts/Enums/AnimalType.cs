using System;

[Flags]
public enum AnimalType
{
    Lion = 1 << 0,
    Chicken = 1 << 1,
    ButterFly = 1 << 2,
}
