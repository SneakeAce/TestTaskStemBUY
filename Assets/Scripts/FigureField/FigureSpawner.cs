using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FigureSpawner
{
    private const int SpawnStep = 1;
    private const int StartCount = 0;
    private const float SpecialFigureSpawnChance = 0.4f;

    private FigureSpawnerConfig _figureSpawnerConfig;

    private IFigureFactory<Figure, FigureConfig> _figureFactory;
    private List<FigureConfig> _figureConfigs;

    private Dictionary<FigureConfig, Queue<FigureType>> _figureTypesByConfig = new Dictionary<FigureConfig, Queue<FigureType>>();

    private Dictionary<FigureConfig, int> _figureSpawnCounts = new Dictionary<FigureConfig, int>();
    private Dictionary<FigureType, SpecialFigureConfig> _specialFigureConfigs = new Dictionary<FigureType, SpecialFigureConfig>();

    private List<FigureConfig> _availableConfigs = new List<FigureConfig>();
    private List<Figure> _spawnedFigures = new List<Figure>();

    private CoroutinePerformer _coroutinePerformer;

    private Coroutine _spawnFigureCoroutine;
    private Coroutine _respawnFigureCoroutine;

    private int _figureSameTypeCount;

    private float _delayBetweenSpawn;

    public FigureSpawner(IFigureFactory<Figure, FigureConfig> actionItemFactory, FigureSpawnerConfig config,
        CoroutinePerformer coroutinePerformer)
    {
        _figureFactory = actionItemFactory;

        _figureSpawnerConfig = config;

        _figureConfigs = new List<FigureConfig>(config.BaseFigureConfigs);
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
        AddSpecialConfigs();

        AddFigureConfigs();

        if (_spawnFigureCoroutine != null)
        {
            _coroutinePerformer.StopCoroutine(_spawnFigureCoroutine);
            _spawnFigureCoroutine = null;
        }

        _spawnFigureCoroutine = _coroutinePerformer.StartCoroutine(SpawnFigureJob());
    }

    private void AddSpecialConfigs()
    {
        if (_figureSpawnerConfig.SpecialFigureConfigs.Count == 0)
            throw new Exception("FigureSpawnerConfig.SpecialFigureConfigs is empty!");

        foreach (SpecialFigureConfig specialConfig in _figureSpawnerConfig.SpecialFigureConfigs)
            _specialFigureConfigs.Add(specialConfig.SpecialFigureType, specialConfig);
    }

    private void AddFigureConfigs()
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
    }

    private IEnumerator SpawnFigureJob()
    {
        int totalAvailableFigureForSpawn = _figureConfigs.Count * _figureSameTypeCount;

        _figureTypesByConfig = FigureTypesForSameType();

        while (totalAvailableFigureForSpawn > StartCount)
        {
            yield return new WaitForSeconds(_delayBetweenSpawn);

            FigureConfig config = _availableConfigs[UnityEngine.Random.Range(StartCount, _availableConfigs.Count)];

            FigureType currentFigureType = _figureTypesByConfig[config].Dequeue();

            Figure figure = _figureFactory.Create(
                config, 
                _figureSpawnerConfig.SpawnCoordinates, 
                _specialFigureConfigs,
                currentFigureType);

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

        _figureTypesByConfig.Clear();
    }

    private Dictionary<FigureConfig, Queue<FigureType>> FigureTypesForSameType()
    {
        Dictionary<FigureConfig, Queue<FigureType>> figureTypesForConfigs = new Dictionary<FigureConfig, Queue<FigureType>>(); 

        foreach (FigureConfig config in _figureConfigs)
        {
            Queue<FigureType> queue = new Queue<FigureType>();
            List<FigureType> availableFigureType = GetAllFigureType(config);

            FigureType baseFigureType = FigureType.BaseFigure;

            float currentRandomValue = UnityEngine.Random.value;

            for (int i = 0; i < _figureSameTypeCount; i++) 
            {
                if (availableFigureType.Count == 0)
                    break;

                if (currentRandomValue < SpecialFigureSpawnChance)
                {
                    FigureType specialFigureType = availableFigureType[UnityEngine.Random.Range(StartCount, availableFigureType.Count)];
                    queue.Enqueue(specialFigureType);
                }
                else
                {
                    queue.Enqueue(baseFigureType);
                }
            }

            figureTypesForConfigs.Add(config, queue);
        }

        return figureTypesForConfigs;
    }

    private List<FigureType> GetAllFigureType(FigureConfig config)
    {
        List<FigureType> result = Enum.GetValues(typeof(FigureType))
            .Cast<FigureType>()
            .Where(type => type != FigureType.BaseFigure && (type & config.AvailableFigureType) != 0)
            .ToList();

        if (result.Count == 0)
            return new List<FigureType>();

        return result;
    }

    private IEnumerator RespawnFigureJob(Dictionary<FigureConfig, int> figuresToSpawn)
    {
        _figureTypesByConfig = FigureTypesForSameType();

        foreach (var kvp in figuresToSpawn)
        {
            for (int i = 0; i < kvp.Value; i++)
            {
                yield return new WaitForSeconds(_delayBetweenSpawn);

                FigureType currentFigureType = _figureTypesByConfig[kvp.Key].Dequeue();

                Figure figure = _figureFactory.Create(
                    kvp.Key, 
                    _figureSpawnerConfig.SpawnCoordinates,
                    _specialFigureConfigs,
                    currentFigureType);

                _spawnedFigures.Add(figure);
            }
        }

        _figureTypesByConfig.Clear();
    }
}
