using System;

[Flags]
public enum ShapeType
{
    Square = 1 << 0,
    Circle = 1 << 1,
    Triangle = 1 << 2,
}
