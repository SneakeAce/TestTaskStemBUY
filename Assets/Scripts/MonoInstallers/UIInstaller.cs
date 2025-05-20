using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private Camera _camera;

    [SerializeField] private ActionBar _bar;

    [SerializeField] private ResultScreenController _resultScreen;

    [SerializeField] private ReassemblyFigureButton _reassemblyFigureButtonPrefab;

    public override void InstallBindings()
    {
        BindResultScreenController();

        BindCamera();

        BindActionBar();

        BindButtons();
    }

    private void BindCamera()
    {
        Container.Bind<Camera>()
            .FromInstance(_camera)
            .AsSingle();
    }

    private void BindActionBar() => Container.Bind<ActionBar>().FromInstance(_bar).AsSingle();

    private void BindResultScreenController() => Container.Bind<ResultScreenController>().FromInstance(_resultScreen).AsSingle();

    private void BindButtons()
    {
        Container.Bind<ReassemblyFigureButton>().FromInstance(_reassemblyFigureButtonPrefab).AsSingle();
    }
    
}
