using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour, IButton
{
    private const string FirstLevelSceneName = "FirstLevelScene";

    public void Click()
    {
        SceneManager.LoadScene(FirstLevelSceneName);
    }
}
