using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public PlayerControls playerControl;
    public GameObject pauseMenuUI;
    public PlayerMovement PlayerMovement;
    public static bool isGamePaused = false;

    private InputAction quit;
    private bool pauseButtonIsPressed = false;

    private void Awake()
    {
        playerControl = new PlayerControls();
    }

    private void OnEnable()
    {
        quit = playerControl.Player.Pause;
        quit.Enable();
    }

    private void OnDisable()
    {
        quit.Disable();
    }

    private void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    private void Update()
    {
        if (quit.ReadValue<float>() == 1 && !pauseButtonIsPressed)
        {
            pauseButtonIsPressed = true;

            if (isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        else if (quit.ReadValue<float>() == 0)
        {
            pauseButtonIsPressed = false;
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        PlayerMovement.enabled = true;
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        PlayerMovement.enabled = false;
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
