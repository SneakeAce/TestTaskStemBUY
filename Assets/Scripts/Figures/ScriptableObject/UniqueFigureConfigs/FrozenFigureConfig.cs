using UnityEngine;

[CreateAssetMenu(menuName = "Configs/FigureConfig/SpecialFigureConfig/FrozenFigureConfig")]
public class FrozenFigureConfig : SpecialFigureConfig
{
    [field: SerializeField] public int FigureToUnfrozenCount { get; private set; }
}
