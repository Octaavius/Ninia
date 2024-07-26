using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseFps : MonoBehaviour
{
    void Start()
    {
        // Set the target frame rate to 120
        Application.targetFrameRate = 120;

        // Disable V-Sync for higher performance
        QualitySettings.vSyncCount = 0;
    }
}
