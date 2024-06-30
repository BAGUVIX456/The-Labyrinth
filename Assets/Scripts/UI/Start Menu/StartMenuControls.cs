using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuControls : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI progressText;

    private void Start()
    {
        loadingScreen.SetActive(false);
    }

    public void StartButtonControl()
    {
        StartCoroutine(LoadScene("Dungeon"));
    }
    
    public void QuitButtonControl()
    {
        Application.Quit();
    }
    
    IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        
        loadingScreen.SetActive(true);
        
        while (!operation.isDone)
        {
            // float progress = Mathf.Clamp01(operation.progress / 0.9f);
            float progress = operation.progress;
            slider.value = progress;
            progressText.text = (int)(progress * 100f) + "%";
            
            yield return null;
        }
    }
}
