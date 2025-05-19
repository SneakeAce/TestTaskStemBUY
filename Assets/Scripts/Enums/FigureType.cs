using System;

[Flags]
public enum FigureType
{
    BaseFigure = 1 << 0,
    HeavyFigure = 1 << 1,
    StickyFigure = 1 << 2,
    FrozenFigure = 1 << 3,
}
