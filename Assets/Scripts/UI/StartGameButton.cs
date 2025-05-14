using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class StartGameButton : MonoBehaviour, IButton
{
    private const string FirstLevelSceneName = "FirstLevelScene";

    private FigureSpawner _figureSpawner;

    [Inject]
    private void Construct(FigureSpawner spawner)
    {
        _figureSpawner = spawner;
    }

    public void Click()
    {
        SceneManager.LoadScene(FirstLevelSceneName);

        _figureSpawner.Start();
        // Запуск скрипта, который начинает спавнить объекты.
    }
}
