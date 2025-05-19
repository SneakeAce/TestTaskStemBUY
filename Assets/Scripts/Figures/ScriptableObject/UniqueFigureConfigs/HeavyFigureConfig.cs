using UnityEngine;

[CreateAssetMenu(menuName = "Configs/FigureConfig/SpecialFigureConfig/HeavyFigureConfig")]
public class HeavyFigureConfig : SpecialFigureConfig
{
    [field: SerializeField] public float Mass { get; private set; }
    [field: SerializeField] public float GravityScale { get; private set; }
}
