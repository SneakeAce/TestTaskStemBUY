using UnityEngine;

[CreateAssetMenu(menuName = "Configs/FigureConfig/HeavyFigureConfig")]
public class HeavyFigureConfig : FigureConfig
{
    [field: SerializeField] public float Mass { get; private set; }
}
