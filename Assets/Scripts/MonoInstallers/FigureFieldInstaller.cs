using UnityEngine;
using Zenject;

public class FigureFieldInstaller : MonoInstaller
{
    [SerializeField] private FigureSpawnerConfig _figureSpawnerConfig;

    public override void InstallBindings()
    {
        BindFigureFactory();

        BindFigureSpawner();

        BindClickHandler();

        BindFigureFieldController();

        BindFirstLevelInitializer();
    }

    private void BindFigureFactory()
    {
        Container.Bind<IFigureFactory<Figure, FigureConfig>>()
            .To<FigureFactory>()
            .AsSingle();
    }

    private void BindFigureSpawner()
    {
        Container.Bind<FigureSpawnerConfig>()
            .FromInstance(_figureSpawnerConfig)
            .AsSingle();

        Container.Bind<FigureSpawner>().AsSingle();
    }

    private void BindClickHandler() => Container.Bind<FigureClickHandler>().AsSingle().NonLazy();

    private void BindFigureFieldController() => Container.Bind<FigureFieldController>().AsSingle();

    private void BindFirstLevelInitializer() => Container.Bind<FirstLevelInitializer>().AsSingle().NonLazy();
}
