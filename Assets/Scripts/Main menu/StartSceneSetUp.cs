using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneSetUp : MonoBehaviour
{
    [Header("Character")]
    public GameObject[] NinjaSkinObjects;
    private GameObject currentSkinObject;
    [Header("Panels")]
    public RectTransform ShopPanel;
    public RectTransform LevelPanel;  
    [Header("Buttons")]
    public RectTransform ShopButton;
    public RectTransform MainButton;
    public RectTransform LevelButton;

    private float screenWidth;

    void Start()
    {
        SceneManagerScript.Instance.FadeOutScript.PlayFadeOut();
        IncreaseFps();

        screenWidth = Display.main.systemWidth;
        SetSkin();
        SetPanelsPosition();
        SetButtonsPosition();
    }
    
    public void SetSkin()
    {
        if(currentSkinObject != null) currentSkinObject.SetActive(false);
        int skinId = PlayerPrefs.GetInt("CurrentSkin", 0);
        NinjaSkinObjects[skinId].SetActive(true);
        currentSkinObject = NinjaSkinObjects[skinId];
    }

    void SetPanelsPosition()
    {
        ShopPanel.offsetMin = new Vector2(-screenWidth, 0);
        ShopPanel.offsetMax = new Vector2(-screenWidth, 0);

        LevelPanel.offsetMin = new Vector2(screenWidth, 0);
        LevelPanel.offsetMax = new Vector2(screenWidth, 0);
    }

    void SetButtonsPosition(){
        ShopButton.offsetMin = new Vector2(0, 0);
        ShopButton.offsetMax = new Vector2(-2 * screenWidth / 3, 180);
        
        MainButton.offsetMin = new Vector2(screenWidth / 3, 0);
        MainButton.offsetMax = new Vector2(-screenWidth / 3, 180);
        
        LevelButton.offsetMin = new Vector2(2 * screenWidth / 3, 0);
        LevelButton.offsetMax = new Vector2(0, 180);
    }

    void IncreaseFps(){
        Application.targetFrameRate = 120;
        QualitySettings.vSyncCount = 0;
    }
}
