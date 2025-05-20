using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FigureFactory : IFigureFactory<Figure, FigureConfig>
{
    private IInstantiator _instantiator;

    public FigureFactory(IInstantiator instantiator)
    {
        _instantiator = instantiator;
    }

    public Figure Create(FigureConfig config, Vector3 spawnPosition, 
        Dictionary<FigureType, SpecialFigureConfig> specialFigureConfigs, FigureType type = FigureType.BaseFigure)
    {
        SpecialFigureConfig specialConfig = null;

        if (specialFigureConfigs.TryGetValue(type, out SpecialFigureConfig specialFigureConfig))
            specialConfig = specialFigureConfig;

        Figure instance = _instantiator.InstantiatePrefabForComponent<Figure>(config.Prefab, 
            spawnPosition, Quaternion.identity, null);

        instance.SetSpecialConfig(specialConfig);
        instance.SetComponents(config);

        return instance;
    }
}
