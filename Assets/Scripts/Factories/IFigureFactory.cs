using System.Collections.Generic;
using UnityEngine;

public interface IFigureFactory<T, TConfig> 
    where T : MonoBehaviour
    where TConfig : ScriptableObject
{
    T Create(TConfig config, Vector3 spawnPosition, 
        Dictionary<FigureType, SpecialFigureConfig> specialFigureConfigs, FigureType type = FigureType.BaseFigure);
}
