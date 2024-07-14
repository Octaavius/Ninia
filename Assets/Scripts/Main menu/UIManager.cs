using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject Settings;
    public void OpenSettings(){
        Settings.SetActive(true);
    }

    public void CloseSettings() {
        Settings.SetActive(false);
    }
}
