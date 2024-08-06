using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootStrap : MonoBehaviour
{
    void Awake(){
        Debug.Log("Hello from bootstrap");
        SceneManagerScript.Instance.FadeOutScript.PlayFadeOut();
    }
}
