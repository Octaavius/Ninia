using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public static ItemsManager Instance {get; private set;}
    public List<IItem> Items;
    
    void Awake()
    {
    	if(Instance == null)
    	{
            Instance = this;
    	}
    	else
    	{
    	    Destroy(gameObject);
    	}
    }
}
