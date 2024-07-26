using System.Collections;
using UnityEngine;
using TMPro;

public class PauseScript : MonoBehaviour
{
    public GameObject PauseMenuUI;
    public GameObject SettingsUI;
    public TMP_Text countdownText;

    private bool cantPause = false;
    
    public void Pause() {
        if(cantPause) return;
        Time.timeScale = 0f;
        PauseMenuUI.SetActive(true);
        GameManager.Instance.GameIsPaused = true;
    }
    public void Resume() {
        PauseMenuUI.SetActive(false);
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

    public void OpenSettings(){
        SettingsUI.SetActive(true);
    }

    public void CloseSettings() {
        SettingsUI.SetActive(false);
    }
}
