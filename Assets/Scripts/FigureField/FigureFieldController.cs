using System;
using System.Collections.Generic;
using UnityEngine;

public class FigureFieldController
{
    private FigureSpawner _spawner;
    private ActionBar _actionBar;

    public FigureFieldController(FigureSpawner spawner, ActionBar actionBar)
    {
        _spawner = spawner;
        _actionBar = actionBar;
    }

    public event Action ShowVictoryScreen;
    public event Action ShowDefeatScreen;

    public void ReassembleFigure()
    {
        ClearFieldFigures();

        Dictionary<FigureConfig, int> missingFigureCounts = GetMissingFigureCounts();

        _spawner.RespawnFigure(missingFigureCounts);
    }

    public void AllActionBarSlotsFilled() => ShowDefeatScreen?.Invoke();

    public void RemoveSameTypeFigureFromField(FigureConfig currentFigureConfig)
    {
        _spawner.RemoveFigureConfigFromList(currentFigureConfig);

        if (_spawner.FigureConfigs.Count == 0)
            ShowVictoryScreen?.Invoke();
    }

    private void ClearFieldFigures()
    {
        foreach (var figure in _spawner.SpawnedFigures)
        {
            if (figure != null && figure.IsInSlot == false)
                figure.DestroyFigure();
        }

        _spawner.SpawnedFigures.Clear();
    }

    private Dictionary<FigureConfig, int> GetMissingFigureCounts()
    {      
        Dictionary<FigureConfig, int> currentCountsSameType = _actionBar.GetCurrentFigureCounts();

        List<FigureConfig> availableConfigs = _spawner.FigureConfigs;

        int sameTypeLimit = _spawner.FigureSameTypeCount;

        Dictionary<FigureConfig, int> missing = new();

        foreach (var config in availableConfigs)
        {
            int existing = currentCountsSameType.TryGetValue(config, out int count) ? count : 0;
            int toSpawn = sameTypeLimit - existing;

            if (toSpawn > 0)
                missing[config] = toSpawn;
        }

        return missing;
    }
}
