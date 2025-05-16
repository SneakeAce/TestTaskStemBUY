using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/FigureSpawnerConfig", fileName = "FigureSpawnerConfig")]
public class FigureSpawnerConfig : ScriptableObject
{
    [field: SerializeField] public List<FigureConfig> ActionItemConfigs { get; private set; }
    [field: SerializeField] public int FigureSameTypeCount { get; private set; }
    [field: SerializeField] public float DelayBetweenSpawn { get; private set; }
}
