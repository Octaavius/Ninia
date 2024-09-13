using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject Settings;
    public GameObject SkinPanel;

    public void OpenSettings(){
        Settings.SetActive(true);
    }

    public void CloseSettings() {
        Settings.SetActive(false);
    }

    public void OpenSkinPanel(){
        SkinPanel.SetActive(true);
    }

    public void CloseSkinPanel(){
        SkinPanel.SetActive(false);
    }
}
