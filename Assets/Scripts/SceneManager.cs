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
    public void ReturnToMenu() {
        SceneManager.LoadScene("MainMenu");
    }
    public void PlayGame() {
	Debug.Log("change the scene");
        StartCoroutine(LoadGame());
    }
    IEnumerator LoadGame(){
        transition.SetTrigger("Start");
        Debug.Log("change the scene2");
        SceneManager.LoadScene("Game");
        yield return new WaitForSeconds(transitionTime);
        Debug.Log("change the scene3");
        
    }
}
