using UnityEngine;
using Zenject;

public class ResultScreenController : MonoBehaviour
{
    [SerializeField] private GameObject _victoryScreen;
    [SerializeField] private GameObject _defeatScreen;

    private FigureFieldController _figureFieldController;

    [Inject]
    private void Construct(FigureFieldController figureFieldController)
    {
        _figureFieldController = figureFieldController;

        _victoryScreen.gameObject.SetActive(false);
        _defeatScreen.gameObject.SetActive(false);

        _figureFieldController.ShowVictoryScreen += OnShowVictoryScreen;
        _figureFieldController.ShowDefeatScreen += OnShowDefeatScreen;
    }

    private void OnShowVictoryScreen()
    {
        _victoryScreen.gameObject.SetActive(true);

        Time.timeScale = 0f;
    }

    private void OnShowDefeatScreen()
    {
        _defeatScreen.gameObject.SetActive(true);

        Time.timeScale = 0f;
    }

    private void OnDestroy()
    {
        _figureFieldController.ShowVictoryScreen -= OnShowVictoryScreen;
        _figureFieldController.ShowDefeatScreen -= OnShowDefeatScreen;
    }
}
