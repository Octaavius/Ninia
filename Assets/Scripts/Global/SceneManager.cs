using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField]
    private SliceTransition sliceTransition; 
    [SerializeField]
    private float transitionTime = 1.4f;
    public void ReturnToMenu() {
        StartCoroutine(LoadMenu());
    }
    IEnumerator LoadMenu(){
        //transition.SetTrigger("ExitToMenu");
        yield return new WaitForSecondsRealtime(transitionTime);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayGame() {
        StartCoroutine(LoadGame());
    }
    IEnumerator LoadGame(){
        sliceTransition.PlayAnimation();
        yield return new WaitForSeconds(sliceTransition.getAnimationTime());
        SceneManager.LoadScene("Game");
    }
}
