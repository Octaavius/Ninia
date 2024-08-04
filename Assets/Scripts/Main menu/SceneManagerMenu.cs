using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerMenu : MonoBehaviour
{
    public static SceneManagerMenu Instance { get; private set; }

    public bool transitionStarted = false;

    void Awake(){
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }
   
    public SliceTransition sliceTransition; 
    
    public void PlayGame() {
        transitionStarted = true;
        StartCoroutine(LoadGame());
    }

    IEnumerator LoadGame(){
        sliceTransition.PlayAnimation();
        yield return new WaitForSeconds(sliceTransition.getAnimationTime());
        SceneManager.LoadScene("Game");
    }
}
