using UnityEngine;

[CreateAssetMenu(menuName = "Configs/FigureConfig/SpecialFigureConfig/StickyFigureConfig")]
public class StickyFigureConfig : SpecialFigureConfig
{
    [field: SerializeField] public int StuckFigureCount { get; private set; }
    [field: SerializeField] public float DistanceToStuckFigure { get; private set; }
    [field: SerializeField] public float SmoothingRatio { get; private set; }
    [field: SerializeField] public float Frequency { get; private set; }
}
