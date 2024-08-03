using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneSetUp : MonoBehaviour
{
    public RectTransform ShopPanel;
    public RectTransform LevelPanel;    

    void Start()
    {
        SetPanelsPosition();   
    }
    
    void SetPanelsPosition()
    {
        float screenWidth = Display.main.systemWidth;

        ShopPanel.offsetMin = new Vector2(-screenWidth, 0);
        ShopPanel.offsetMax = new Vector2(-screenWidth, 0);

        LevelPanel.offsetMin = new Vector2(screenWidth, 0);
        LevelPanel.offsetMax = new Vector2(screenWidth, 0);
    }
}
