using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureSpawner
{
    private const int SpawnStep = 1;
    private const int StartCount = 0;

    private IFactory<Figure, FigureConfig> _figureFactory;
    private List<FigureConfig> _figureConfigs;

    private Dictionary<FigureConfig, int> _figureSpawnCounts = new Dictionary<FigureConfig, int>();

    private List<FigureConfig> _availableConfigs = new List<FigureConfig>();
    private List<Figure> _spawnedFigures = new List<Figure>();

    private CoroutinePerformer _coroutinePerformer;

    private Coroutine _spawnFigureCoroutine;
    private Coroutine _respawnFigureCoroutine;

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

    public List<FigureConfig> FigureConfigs { get => _figureConfigs; }
    public List<Figure> SpawnedFigures { get =>  _spawnedFigures; }

    public int FigureSameTypeCount {get => _figureSameTypeCount; }

    public event Action<Figure> RemovedFromField;

    public void RespawnFigure(Dictionary<FigureConfig, int> figuresToSpawn)
    {
        if (_respawnFigureCoroutine != null)
        {
            _coroutinePerformer.StopCoroutine(RespawnFigureJob(figuresToSpawn));
            _respawnFigureCoroutine = null;
        }

        _respawnFigureCoroutine = _coroutinePerformer.StartCoroutine(RespawnFigureJob(figuresToSpawn));
    }

    public void RemoveFigureConfigFromList(FigureConfig config)
    {
        if (_figureConfigs.Contains(config) && _figureConfigs.Count > 0)
            _figureConfigs.Remove(config);
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

            Figure figure = _figureFactory.Create(config, new Vector3(UnityEngine.Random.Range(-1f, 1f), 5, 0));

            if (figure == null)
                continue;

            _spawnedFigures.Add(figure);

            _figureSpawnCounts[config] += SpawnStep;
            totalAvailableFigureForSpawn -= SpawnStep;

            if (_figureSpawnCounts[config] >= _figureSameTypeCount)
            {
                _availableConfigs.Remove(config);
            }
        }
    }

    private IEnumerator RespawnFigureJob(Dictionary<FigureConfig, int> figuresToSpawn)
    {
        foreach (var kvp in figuresToSpawn)
        {
            for (int i = 0; i < kvp.Value; i++)
            {
                yield return new WaitForSeconds(_delayBetweenSpawn);

                Figure figure = _figureFactory.Create(kvp.Key, new Vector3(UnityEngine.Random.Range(-1f, 1f), 5, 0));

                _spawnedFigures.Add(figure);
            }
        }
    }
}
