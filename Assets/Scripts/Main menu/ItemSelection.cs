using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemZone{
    Head,
    Face,
    Hand1,
    Hand2,
    Waist1,
    Waist2,
    Leg1,
    Leg2,
    None
}

public class ItemSelection : MonoBehaviour
{
    [SerializeField] private GameObject[] items;
    [SerializeField] private GameObject[] equippedItems;

    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;
    
    private int[] currentItems = {0, 0, 0, 0, 0, 0, 0, 0};
    private int selectedItem = -1;
    private ItemZone selectedItemZone = ItemZone.None;
    private bool activeItemIsSelected = false;

    void Start()
    {
        LoadCurrentItems();
    } 

    public void SelectItem(int itemId)
    {
        if(itemId == selectedItem)
        { 
            ResetSelection();
            items[itemId].transform.localScale = new Vector2(1f, 1f);
            activeItemIsSelected = false;
        } 
        else if(itemId == currentItems[0] || 
                itemId == currentItems[1] || 
                itemId == currentItems[2] ||
                itemId == currentItems[3] ||
                itemId == currentItems[4] ||
                itemId == currentItems[5] ||
                itemId == currentItems[6] ||
                itemId == currentItems[7])
        {
            if(selectedItem == -1){
                int itemZoneId = 0;
                for(int i = 0; i < currentItems.Length; i++)
                {
                    if(currentItems[i] == itemId)
                    {
                        itemZoneId = i;
                        break;
                    }
                }
                InactivateItem((ItemZone)itemZoneId);
                //ShowItemInfo();
            } 
            activeItemIsSelected = true;
            return;
        } 
        else 
        {
            if(selectedItem != -1) 
            {
                items[selectedItem].transform.localScale = new Vector2(1f, 1f);
            }
            selectedItem = itemId;
            items[itemId].transform.localScale = new Vector2(1.2f, 1.2f);
            activeItemIsSelected = false;
        }
    }

    public void SetSelectedItemZone(int itemZoneId)
    {
        Debug.Log(activeItemIsSelected);
        if(!activeItemIsSelected)
            selectedItemZone = (ItemZone)itemZoneId;
    }

    public void OnClickEquippedItem(int itemZoneId)
    {
        if(selectedItem == -1)
        {
            InactivateItem((ItemZone)itemZoneId);
            //ShowItemInfo();
        } 
        else 
        {
            bool appropriateZone = CheckZone((ItemZone)itemZoneId);
            if(appropriateZone)
            {
                SetNewItem((ItemZone)itemZoneId, selectedItem);
            }
        }
    }

    bool CheckZone(ItemZone itemZone)
    {
        if(itemZone == ItemZone.Face && selectedItemZone == ItemZone.Face)
        {
            return true;
        } 
        else if(itemZone == ItemZone.Head && selectedItemZone == ItemZone.Head)
        {
            return true;
        }
        else if((itemZone == ItemZone.Hand1 || itemZone == ItemZone.Hand2) && (selectedItemZone == ItemZone.Hand1 || selectedItemZone == ItemZone.Hand2))
        {
            return true;
        }
        else if((itemZone == ItemZone.Waist1 || itemZone == ItemZone.Waist2) && (selectedItemZone == ItemZone.Waist1 || selectedItemZone == ItemZone.Waist2))
        {
            return true;
        }
        else if((itemZone == ItemZone.Leg1 || itemZone == ItemZone.Leg2) && (selectedItemZone == ItemZone.Leg1 || selectedItemZone == ItemZone.Leg2))
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    void LoadCurrentItems()
    {
        currentItems[0] = PlayerPrefs.GetInt("Head", 0);
        currentItems[1] = PlayerPrefs.GetInt("Face", 0);
        currentItems[2] = PlayerPrefs.GetInt("Hand1", 0);
        currentItems[3] = PlayerPrefs.GetInt("Hand2", 0);
        currentItems[4] = PlayerPrefs.GetInt("Waist1", 0);
        currentItems[5] = PlayerPrefs.GetInt("Waist2", 0);
        currentItems[6] = PlayerPrefs.GetInt("Leg1", 0);
        currentItems[7] = PlayerPrefs.GetInt("Leg2", 0);
        
        for (int i = 0; i < 8; i++) 
        {
            SetNewItem((ItemZone) i, currentItems[i]);
        }
    }

    void SetNewItem(ItemZone itemZone, int itemId)
    {
        if(itemId == 0) 
        {
            return;
        }
        InactivateItem(itemZone);

        PlayerPrefs.SetInt($"{itemZone}", itemId);
        currentItems[(int) itemZone] = itemId;
        ChangeEquippedItemIcon(itemZone, itemId);
        items[itemId].transform.localScale = new Vector2(1f, 1f);
        items[itemId].GetComponent<Image>().color = activeColor;
        ResetSelection();
    }

    void ResetSelection()
    {
        selectedItem = -1;
        selectedItemZone = ItemZone.None;
    }

    void InactivateItem(ItemZone itemZone)
    {
        if(currentItems[(int)itemZone] != 0)
        {
            items[currentItems[(int)itemZone]].GetComponent<Image>().color = inactiveColor;
            currentItems[(int)itemZone] = 0;
            PlayerPrefs.SetInt($"{itemZone}", 0);
            ChangeEquippedItemIcon(itemZone, 0);
        }
    }

    void ChangeEquippedItemIcon(ItemZone itemZone, int itemId)
    {
        if(itemId == 0)
        {
			SetDefaultIcon(itemZone);
        }
        else 
        {
        	Sprite newSprite = items[itemId].transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
        	equippedItems[(int)itemZone].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = newSprite; 
        }
    }

    void SetDefaultIcon(ItemZone itemZone)
    {
		equippedItems[(int)itemZone].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = null;
    }

    void ShowItemInfo()
    {
        //show info about item
    } 

}
