using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class StartGameButton : MonoBehaviour, IButton
{
    private const string FirstLevelSceneName = "FirstLevelScene";

    private Coroutine _loadSceneCoroutine;

    public void Click()
    {
        _loadSceneCoroutine = StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        yield return SceneManager.LoadSceneAsync(FirstLevelSceneName, LoadSceneMode.Additive);

        SceneManager.UnloadSceneAsync("MainMenuScene");
    }

    //private void OnDestroy()
    //{
    //    if (_loadSceneCoroutine != null)
    //    {
    //        StopCoroutine(_loadSceneCoroutine);
    //        _loadSceneCoroutine = null;
    //    }
    //}
}

