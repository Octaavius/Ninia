using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField]
    private Animator transition; 
    [SerializeField]
    private float transitionTime = 1.4f;
    public void PlayGame() {
        StartCoroutine(LoadGame());
    }
    IEnumerator LoadGame(){
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene("Game");
    }
}
