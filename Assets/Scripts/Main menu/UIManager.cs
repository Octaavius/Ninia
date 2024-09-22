using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } 

    public GameObject Settings;
    public GameObject SkinPanel;

    public bool OtherPanelIsOpened = false;

    void Awake(){
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void OpenSettings(){
        OtherPanelIsOpened = true;
        Settings.SetActive(true);
        PopUpAnimation(Settings);
    }

    public void CloseSettings() {
        OtherPanelIsOpened = false;
        Settings.SetActive(false);
    }

    public void OpenSkinPanel(){
        OtherPanelIsOpened = true;
        SkinPanel.SetActive(true);
        PopUpAnimation(SkinPanel);
    }

    public void CloseSkinPanel(){
        OtherPanelIsOpened = false;
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
