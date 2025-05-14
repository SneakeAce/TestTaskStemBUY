using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureSpawner
{
    // Сделать интерфейс с дженериками

    private const int SpawnStep = 1;
    private const int StartCount = 0;

    private IFactory<Figure, FigureConfig> _figureFactory;
    private List<FigureConfig> _figureConfigs;

    private Dictionary<FigureConfig, int> _figureSpawnCounts = new Dictionary<FigureConfig, int>();
    private List<FigureConfig> _availableConfigs = new List<FigureConfig>();

    private CoroutinePerformer _coroutinePerformer;

    private Coroutine _spawnFigureCoroutine;

    private int _figureSameTypeCount;

    private float _delayBetweenSpawn;

    public FigureSpawner(IFactory<Figure, FigureConfig> actionItemFactory, FigureSpawnerConfig config,
        CoroutinePerformer coroutinePerformer)
    {
        _figureFactory = actionItemFactory;

        _figureConfigs = config.ActionItemConfigs;
        _figureSameTypeCount = config.FigureSameTypeCount;
        _delayBetweenSpawn = config.DelayBetweenSpawn;

        _coroutinePerformer = coroutinePerformer;
    }

    public void Start()
    {
        if (_figureConfigs.Count <= StartCount)
            throw new NullReferenceException("FigureConfigs is null or empty");

        _figureSpawnCounts.Clear();
        _availableConfigs.Clear();

        foreach (FigureConfig config in _figureConfigs)
        {
            if (config == null)
                continue;

            _figureSpawnCounts[config] = StartCount;
            _availableConfigs.Add(config);
        }

        if (_spawnFigureCoroutine != null)
        {
            _coroutinePerformer.StopCoroutine(_spawnFigureCoroutine);
            _spawnFigureCoroutine = null;
        }

        _spawnFigureCoroutine = _coroutinePerformer.StartCoroutine(SpawnFigureJob());
    }

    private IEnumerator SpawnFigureJob()
    {
        int totalAvailableFigureForSpawn = _figureConfigs.Count * _figureSameTypeCount; 

        while (totalAvailableFigureForSpawn > StartCount)
        {
            yield return new WaitForSeconds(_delayBetweenSpawn);

            FigureConfig config = _availableConfigs[UnityEngine.Random.Range(StartCount, _availableConfigs.Count)];

            Figure item = _figureFactory.Create(config, new Vector3(0, 5, 0));

            if (item == null)
                continue;

            _figureSpawnCounts[config] += SpawnStep;
            totalAvailableFigureForSpawn -= SpawnStep;

            if (_figureSpawnCounts[config] >= _figureSameTypeCount)
            {
                _availableConfigs.Remove(config);
            }
        }
    }
}
