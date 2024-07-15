using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagerGame : MonoBehaviour
{
    public FadeIn FadeInScript;
    public void ReturnToMenu() {
        StartCoroutine(LoadMenu());
    }
    IEnumerator LoadMenu(){
        FadeInScript.PlayFadeIn();
        yield return new WaitForSecondsRealtime(FadeInScript.animationDuration);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
