using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public static ItemsManager Instance {get; private set;}
    [HideInInspector] public List<Item> Items;
    
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
            Item item = child.GetComponent<Item>();
            if (item != null)
            {
                Items.Add(item);
            }
        }
    }

	public void ActivateItems(int[] itemsToActivate){
		foreach(int itemId in itemsToActivate)
		{
            if (itemId == -1) continue;
			Items[itemId].ApplyEffect(NinjaController.Instance.gameObject.GetComponent<Creature>());
        }
	}
}
