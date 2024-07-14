using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseScript : MonoBehaviour
{
    public GameObject PauseMenuUI;
    public TMP_Text countdownText;
    
    public void Pause() {
        Time.timeScale = 0f;
        PauseMenuUI.SetActive(true);
        GameManager.GameIsPaused = true;
    }
    public void Resume() {
        PauseMenuUI.SetActive(false);
        StartCoroutine(CountdownRoutine()); 
        
        GameManager.GameIsPaused = false;    
    }

    IEnumerator CountdownRoutine()
    {
        countdownText.gameObject.SetActive(true);

        yield return StartCoroutine(AnimateNumber("3"));
        yield return StartCoroutine(AnimateNumber("2"));
        yield return StartCoroutine(AnimateNumber("1"));

        Time.timeScale = 1f;

        countdownText.text = "";

        countdownText.gameObject.SetActive(false);
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
}
