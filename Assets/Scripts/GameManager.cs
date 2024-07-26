using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerControls playerControl;
    public GameObject pauseMenuUI;
    public GameObject gameOverUI;
    public GameObject winUI;
    public TextMeshProUGUI EnemyCounter;
    public PlayerMovement PlayerMovement;
    public static bool isGamePaused = false;
    public static bool isGameOver = false;
    public static bool isWin = false;
    public int enemyCount;

    private InputAction quit;
    private bool pauseButtonIsPressed = false;

    private void Awake()
    {
        playerControl = new PlayerControls();
        enemyCount = 0;
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
        gameOverUI.SetActive(false);

        isGameOver = false;
        isGamePaused = false;
        isWin = false;
        PlayerMovement.enabled = true;
    }

    private void Update()
    {
        if (isWin)
            return;
        if (enemyCount <= 0)
            Win();
        
        if (quit.ReadValue<float>() == 1 && !pauseButtonIsPressed && !isGameOver && !isWin)
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
    
    public void GameOver()
    {
        isGameOver = true;
        gameOverUI.SetActive(true);
        PlayerMovement.enabled = false;
        Time.timeScale = 0f;
    }

    public void Win()
    {
        isWin = true;
        winUI.SetActive(true);
        PlayerMovement.enabled = false;
        Time.timeScale = 0f;
    }
    
    public void RestartGame()
    {
        PlayerMovement.enabled = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Dungeon");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void UpdateCounter()
    {
        EnemyCounter.text = "Enemies Remaining: " + enemyCount;
    }
}