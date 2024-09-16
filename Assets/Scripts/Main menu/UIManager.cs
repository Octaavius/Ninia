using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject Settings;
    public GameObject SkinPanel;

    public void OpenSettings(){
        Settings.SetActive(true);
        PopUpAnimation(Settings);
    }

    public void CloseSettings() {
        Settings.SetActive(false);
    }

    public void OpenSkinPanel(){
        SkinPanel.SetActive(true);
        PopUpAnimation(SkinPanel);
    }

    public void CloseSkinPanel(){
        SkinPanel.SetActive(false);
    }

    void PopUpAnimation(GameObject gameObject){
        RectTransform rt = gameObject.GetComponent<RectTransform>();
        rt.localScale = new Vector3(0f, 0f, 0f); 
        LeanTween.scale(rt, new Vector3(1.1f, 1.1f, 1f), 0.2f)
                 .setEase(LeanTweenType.easeInOutQuad)
                 .setOnComplete(() => {
                     LeanTween.scale(rt, new Vector3(1f, 1f, 1f), 0.05f)
                              .setEase(LeanTweenType.easeInOutQuad);
                 });
    }
}
