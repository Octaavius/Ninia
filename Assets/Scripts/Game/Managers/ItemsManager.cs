using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public static ItemsManager Instance {get; private set;}
    [HideInInspector] public List<GameObject> Items;
    
    void Awake()
    {
    	if(Instance == null)
    	{
            Instance = this;
			DontDestroyOnLoad(gameObject);
    	}
    	else
    	{
    	    Destroy(gameObject);
    	}
		FillItemsList();
    }

	void FillItemsList()
    {
        Items = new();
        foreach (Transform child in gameObject.transform)
        {
            Items.Add(child.gameObject);
        }
    }

	public void ActivateItems(int[] itemsToActivate){
		foreach(int itemId in itemsToActivate)
		{
            if (itemId == -1) continue;
			Item[] listOfItemEffects = Items[itemId].GetComponents<Item>(); 
            foreach(Item itemEffect in listOfItemEffects)
                itemEffect.ApplyEffect(NinjaController.Instance);
        }
	}
}
