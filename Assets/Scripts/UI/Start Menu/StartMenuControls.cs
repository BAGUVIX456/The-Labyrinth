using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuControls : MonoBehaviour
{
    public void StartButtonControl()
    {
        SceneManager.LoadScene("Dungeon");
    }

    public void QuitButtonControl()
    {
        Application.Quit();
    }
}
