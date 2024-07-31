using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagerGame : MonoBehaviour
{
    public static SceneManagerGame Instance { get; private set; }
    
    void Awake(){
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public FadeIn FadeInScript;
    public void ReturnToMenu() {
        StartCoroutine(LoadMenu());
    }
    IEnumerator LoadMenu(){
        FadeInScript.PlayFadeIn();
        EffectsManager.Instance.RemoveAllEffects();
        yield return new WaitForSecondsRealtime(FadeInScript.animationDuration);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
