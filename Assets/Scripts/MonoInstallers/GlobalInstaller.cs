using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    // оепедекюрэ!!!!
    [SerializeField] private CoroutinePerformer _coroutinePerformerPrefab;
    [SerializeField] private StartGameButton _startButtonPrefab;

    public override void InstallBindings()
    {
        BindPlayerInput();
        BindCoroutinePerformer();

        BindStartButton();
    }

    private void BindPlayerInput()
    {
        Container.Bind<PlayerInput>().AsSingle();
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
