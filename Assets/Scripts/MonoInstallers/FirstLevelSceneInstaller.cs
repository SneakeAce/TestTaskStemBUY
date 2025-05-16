using UnityEngine;
using Zenject;

public class FirstLevelSceneInstaller : MonoInstaller
{
    [SerializeField] private Camera _camera;
    [SerializeField] private ActionBar _bar;
    [SerializeField] private FigureSpawnerConfig _figureSpawnerConfig;
    [SerializeField] private ReassemblyFigureButton _reassemblyFigureButtonPrefab;
    [SerializeField] private ResultScreenController _showScreen;

    public override void InstallBindings()
    {
        BindFigureManagers();

        BindLevelInitializer();

        BindReassemblyFigureButton();

        BindClickHandler();

        BindShowScreen();
    }

    private void BindFigureManagers()
    {
        Container.Bind<FigureSpawnerConfig>()
            .FromInstance(_figureSpawnerConfig)
            .AsSingle();

        Container.Bind<IFactory<Figure, FigureConfig>>()
            .To<FigureFactory>()
            .AsSingle();

        Container.Bind<FigureSpawner>().AsSingle();
    }

    private void BindLevelInitializer()
    {

        Container.Bind<FirstLevelInitializer>()
            .AsSingle().NonLazy();
    }

    private void BindClickHandler()
    {
        Container.Bind<Camera>()
            .FromInstance(_camera)
            .AsSingle();

        Container.Bind<ActionBar>().FromInstance(_bar).AsSingle();

        Container.Bind<FigureClickHandler>().AsSingle().NonLazy();
    }

    private void BindReassemblyFigureButton()
    {
        Container.Bind<FigureFieldController>().AsSingle();

        Container.Bind<ReassemblyFigureButton>().FromInstance(_reassemblyFigureButtonPrefab).AsSingle();
    }

    private void BindShowScreen()
    {
        Container.Bind<ResultScreenController>().FromInstance(_showScreen).AsSingle();
    }
}
