using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevelButton : MonoBehaviour, IButton
{
    public void Click()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
