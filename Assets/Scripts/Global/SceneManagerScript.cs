using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(FadeIn))]
[RequireComponent(typeof(FadeOut))]

public class SceneManagerScript : MonoBehaviour
{
    public static SceneManagerScript Instance { get; private set; }
    
    FadeIn FadeInScript; 
    [HideInInspector] public FadeOut FadeOutScript; 
    
    [HideInInspector] public string sceneName = "Arcade";

    void Awake(){
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
        FadeInScript = GetComponent<FadeIn>();
        FadeOutScript = GetComponent<FadeOut>();
    }

    public void ReturnToMenu() {
        StartCoroutine(LoadMenu());
    }
    
    public void PlayGame() {
        SceneManager.LoadScene(sceneName);
    }
    
    IEnumerator LoadMenu(){
        FadeInScript.PlayFadeIn();
        BuffsManager.Instance.RemoveAllBuffs();
        yield return new WaitForSecondsRealtime(FadeInScript.animationDuration);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
