using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private PlayerInput _playerInput;

    public override void InstallBindings()
    {
        BindPlayerInput();
    }

    private void BindPlayerInput()
    {
        Container.Bind<PlayerInput>()
            .FromInstance(_playerInput)
            .AsSingle();
    }
}
