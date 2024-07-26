using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{   
    [Header("Text fields")]
    public TMP_Text countdownText;
    public TMP_Text scoreText;
    [Header("Buttons")]
    public Button continueButton;
    public Button settingsButton;
    public Button restartButton;
    public Button exitToMenuButton;
    public Button pauseButton;
    public Button backFromSettingsButton;

    // Settings menu
    [Header("UI Panels")]
    public GameObject UnifiedMenuUI;
    public GameObject SettingsUI;

    private bool cantPause = false;

    // Enum to track menu state
    private enum MenuState { None, Pause, EndGame }
    private MenuState currentMenuState = MenuState.None;

    void Start()
    {
        // Initially hide the menu
        SetMenuState(MenuState.None);

        // Assign button click listeners
        continueButton.onClick.AddListener(OnContinueButtonClicked);
        restartButton.onClick.AddListener(OnRestartButtonClicked);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        exitToMenuButton.onClick.AddListener(OnExitToMenuButtonClicked);
        pauseButton.onClick.AddListener(OnPauseButtonClicked);
        backFromSettingsButton.onClick.AddListener(CloseSettings);
    }
    public void Pause()
    {
        if (cantPause) return;
        Time.timeScale = 0f;
        ShowPauseMenu();
        GameManager.Instance.GameIsPaused = true;
    }

    public void Resume()
    {
        SetMenuState(MenuState.None);
        StartCoroutine(CountdownRoutine());

        GameManager.Instance.GameIsPaused = false;
    }

    IEnumerator CountdownRoutine()
    {
        cantPause = true;
        countdownText.gameObject.SetActive(true);

        yield return StartCoroutine(AnimateNumber("3"));
        yield return StartCoroutine(AnimateNumber("2"));
        yield return StartCoroutine(AnimateNumber("1"));

        Time.timeScale = 1f;

        countdownText.text = "";

        countdownText.gameObject.SetActive(false);
        cantPause = false;
    }

    IEnumerator AnimateNumber(string number)
    {
        countdownText.text = number;

        // Scale up using LeanTween with unscaled time
        LeanTween.scale(countdownText.gameObject, new Vector3(1.5f, 1.5f, 1.5f), 0.3f).setEase(LeanTweenType.easeOutBack).setIgnoreTimeScale(true);

        // Wait for half a second in real time
        yield return new WaitForSecondsRealtime(0.3f);

        // Scale down using LeanTween with unscaled time
        LeanTween.scale(countdownText.gameObject, Vector3.one, 0.3f).setEase(LeanTweenType.easeInBack).setIgnoreTimeScale(true);

        // Wait for another half second in real time
        yield return new WaitForSecondsRealtime(0.3f);
    }

    public void ShowPauseMenu()
    {
        SetMenuState(MenuState.Pause);
    }

    public void ShowEndGameMenu(int score)
    {
        SetMenuState(MenuState.EndGame);
        scoreText.text = "Score: " + score;
    }

    private void SetMenuState(MenuState state)
    {
        Debug.Log(state);

        currentMenuState = state;

        switch (state)
        {
            case MenuState.Pause:
                UnifiedMenuUI.SetActive(true);
                scoreText.gameObject.SetActive(false);
                continueButton.gameObject.SetActive(true);
                restartButton.gameObject.SetActive(false);
                break;

            case MenuState.EndGame:
                UnifiedMenuUI.SetActive(true);
                scoreText.gameObject.SetActive(true);
                continueButton.gameObject.SetActive(false);
                restartButton.gameObject.SetActive(true);
                break;

            case MenuState.None:
            default:
                UnifiedMenuUI.SetActive(false);
                break;
        }
    }

    // Button action methods
    public void OnPauseButtonClicked()
    {
        Pause();
    }
    public void OnContinueButtonClicked()
    {
        Resume();
    }
    
    public void OnRestartButtonClicked()
    {
        SetMenuState(MenuState.None);
        GameManager.Instance.RestartGame();
        // Restart the game logic
        // For example, reload the current scene
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void OnSettingsButtonClicked()
    {
        SettingsUI.SetActive(true);
    }
    
    public void OnExitToMenuButtonClicked()
    {
        // Exit to main menu logic
        // For example, load the main menu scene
        
        //DO WE NEED THIS?
        //GM.EndGame();
        SceneManagerGame.Instance.ReturnToMenu();
    }

    public void CloseSettings()
    {
        SettingsUI.SetActive(false);
    }
}
