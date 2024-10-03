using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootStrap : MonoBehaviour
{
    public GameObject[] NinjaSkins;
    public RectTransform[] Buffs;
    public RectTransform BuffsStartingPoint;

    void Start(){
        StartCoroutine(StartFunction());
        LoadSkin();
        LoadBuffs();
    }

    void LoadBuffs(){
        RectTransform currentButtonPoint = BuffsStartingPoint;
        for (int i = 0; i < 3; i++) {
            int buffId = PlayerPrefs.GetInt("Buff " + i, 0);
            Debug.Log(buffId);
            if (buffId != 0) {
                Buffs[buffId].gameObject.SetActive(true);
                Buffs[buffId].anchoredPosition = currentButtonPoint.anchoredPosition;
                currentButtonPoint.anchoredPosition += new Vector2(0, 200);
            }
        }
    }

    void LoadSkin(){
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
