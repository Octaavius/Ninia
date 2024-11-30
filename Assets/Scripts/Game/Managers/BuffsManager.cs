using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffsManager : MonoBehaviour
{
    public static BuffsManager Instance { get; private set; }

    public List<RectTransform> BuffsTransform;

    private List<Buff> BuffsList;

    void Awake(){
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
        FillBuffsList();
    }

    public void RemoveAllBuffs(){
        foreach(Buff buff in BuffsList){
            buff.StopBuff();
            buff.ResetCooldown();
        }
    }

    void FillBuffsList()
    {
        BuffsList = new();
        foreach (Transform child in gameObject.transform)
        {
            Buff buff = child.GetComponent<Buff>();
            if (buff != null)
            {
                BuffsList.Add(buff);
            }
        }
    }
}
