using System;

[Flags]
public enum ShapeType
{
    None = 1 << 0,
    Square = 1 << 1,
    Circle = 1 << 2,
    Triangle = 1 << 4,
}
