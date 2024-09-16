using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootStrap : MonoBehaviour
{
    public GameObject[] NinjaSkins;

    void Awake(){
        StartCoroutine(StartFunction());
        int currentSkin = PlayerPrefs.GetInt("CurrentSkin", 0);
        NinjaSkins[currentSkin].SetActive(true);
    }

    IEnumerator StartFunction(){
        while (SceneManagerScript.Instance == null) {
            yield return null; // Wait until the next frame
        }
        SceneManagerScript.Instance.FadeOutScript.PlayFadeOut();
    }
}
