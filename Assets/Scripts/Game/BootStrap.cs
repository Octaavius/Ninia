using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootStrap : MonoBehaviour
{
    void Awake(){
        Debug.Log("Hello from bootstrap");
        StartCoroutine(StartFunction());
    }

    IEnumerator StartFunction(){
        while (SceneManagerScript.Instance == null) {
            yield return null; // Wait until the next frame
        }
        SceneManagerScript.Instance.FadeOutScript.PlayFadeOut();
    }
}
