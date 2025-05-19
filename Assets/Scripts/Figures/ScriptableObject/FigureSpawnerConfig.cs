using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/FigureSpawnerConfig", fileName = "FigureSpawnerConfig")]
public class FigureSpawnerConfig : ScriptableObject
{
    [field: SerializeField] public List<FigureConfig> BaseFigureConfigs { get; private set; }
    [field: SerializeField] public List<SpecialFigureConfig> SpecialFigureConfigs { get; private set; }

    [field: SerializeField] public int FigureSameTypeCount { get; private set; }
    [field: SerializeField] public float DelayBetweenSpawn { get; private set; }
    [field: SerializeField] public Vector3 SpawnCoordinates { get; private set; }
}
