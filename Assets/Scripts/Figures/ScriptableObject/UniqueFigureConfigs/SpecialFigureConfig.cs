using UnityEngine;

public class SpecialFigureConfig : ScriptableObject
{
    [field: SerializeField] public FigureType SpecialFigureType { get; private set; }
}
