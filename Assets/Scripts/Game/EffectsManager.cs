using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    public static EffectsManager Instance { get; private set; }

    private List<Effect> effectsList;

    void Awake(){
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
        FillEffectsList();
    }

    public void RemoveAllEffects(){
        foreach(Effect effect in effectsList){
            effect.StopEffect();
        }
    }

    void FillEffectsList()
    {
        effectsList = new();
        foreach (Transform child in gameObject.transform)
        {
            Effect effect = child.GetComponent<Effect>();
            if (effect != null)
            {
                effectsList.Add(effect);
            }
        }
    }
}
