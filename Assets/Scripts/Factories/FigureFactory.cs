using UnityEngine;
using Zenject;

public class FigureFactory : IFactory<Figure, FigureConfig>
{
    private IInstantiator _instantiator;

    public FigureFactory(IInstantiator instantiator)
    {
        _instantiator = instantiator;
    }

    public Figure Create(FigureConfig config, Vector3 spawnPosition)
    {
        Figure instance = _instantiator.InstantiatePrefabForComponent<Figure>(config.Prefab, 
            spawnPosition, Quaternion.identity, null);

        instance.SetComponents(config);

        return instance;
    }
}
