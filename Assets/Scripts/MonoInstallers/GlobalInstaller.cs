using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    // оепедекюрэ!!!!
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private FigureSpawnerConfig _figureSpawnerConfig;
    [SerializeField] private CoroutinePerformer _coroutinePerformerPrefab;
    [SerializeField] private StartGameButton _startButtonPrefab;

    public override void InstallBindings()
    {
        BindPlayerInput();
        BindCoroutinePerformer();

        BindFigureManagers();

        BindStartButton();
    }

    private void BindPlayerInput()
    {
        Container.Bind<PlayerInput>()
            .FromInstance(_playerInput)
            .AsSingle();
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

    private void BindCoroutinePerformer()
    {
        Container.Bind<CoroutinePerformer>().FromComponentInNewPrefab(_coroutinePerformerPrefab).AsSingle();
    }

    private void BindStartButton()
    {
        Container.Bind<StartGameButton>().FromComponentInNewPrefab(_startButtonPrefab).AsSingle();
    }
}
