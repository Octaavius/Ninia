using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerMenu : MonoBehaviour
{
    public SliceTransition sliceTransition; 
    
    public void PlayGame() {
        StartCoroutine(LoadGame());
    }

    IEnumerator LoadGame(){
        sliceTransition.PlayAnimation();
        yield return new WaitForSeconds(sliceTransition.getAnimationTime());
        SceneManager.LoadScene("Game");
    }
}
