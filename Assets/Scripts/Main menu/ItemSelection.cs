using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelection : MonoBehaviour
{
    [SerializeField] private List<GameObject> _items;
    [SerializeField] private List<GameObject> _equippedItems;
    [SerializeField] private List<RectTransform> _itemsPreviewImages;

    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _inactiveColor;
    
    private int[] _currentItems = {-1, -1, -1, -1, -1, -1, -1, -1};
    private int selectedItem = -1;
    private ItemType selectedItemType = ItemType.None;

    void Start()
    {
        LoadCurrentItems();
    } 

    public void SelectItem(int itemId)
    {
        if(itemId == selectedItem)
        { 
            ResetSelection();
        } 
        else if(itemId == _currentItems[0] || 
                itemId == _currentItems[1] || 
                itemId == _currentItems[2] ||
                itemId == _currentItems[3] ||
                itemId == _currentItems[4] ||
                itemId == _currentItems[5] ||
                itemId == _currentItems[6] ||
                itemId == _currentItems[7])
        {
            if(selectedItem == -1){
                int itemZoneId = 0;
                for(int i = 0; i < _currentItems.Length; i++)
                {
                    if(_currentItems[i] == itemId)
                    {
                        itemZoneId = i;
                        break;
                    }
                }
                ResetItemZone((ItemZone)itemZoneId);
                //ShowItemInfo();
            } 
            return;
        } 
        else 
        {
            ResetSelection();
            selectedItem = itemId;
            selectedItemType = ItemsManager.Instance.Items[itemId].Type;
            _items[itemId].transform.localScale = new Vector2(1.2f, 1.2f);
        }
    }

    public void OnClickEquippedItem(int itemZoneId)
    {
        if(selectedItem == -1)
        {
            if(_currentItems[itemZoneId] != -1)
            {
                ResetItemZone((ItemZone)itemZoneId);
                //ShowItemInfo();
            }
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
        if(itemZone == ItemZone.Face && selectedItemType == ItemType.Face)
        {
            return true;
        } 
        else if(itemZone == ItemZone.Head && selectedItemType == ItemType.Head)
        {
            return true;
        }
        else if((itemZone == ItemZone.RightHand || itemZone == ItemZone.LeftHand) && selectedItemType == ItemType.Hand)
        {
            return true;
        }
        else if((itemZone == ItemZone.RightWaist || itemZone == ItemZone.LeftWaist) && selectedItemType == ItemType.Waist)
        {
            return true;
        }
        else if((itemZone == ItemZone.RightLeg || itemZone == ItemZone.LeftLeg) && selectedItemType == ItemType.Leg)
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
        _currentItems[0] = PlayerPrefs.GetInt("Head", -1);
        _currentItems[1] = PlayerPrefs.GetInt("Face", -1);
        _currentItems[2] = PlayerPrefs.GetInt("LeftHand", -1);
        _currentItems[3] = PlayerPrefs.GetInt("RightHand", -1);
        _currentItems[4] = PlayerPrefs.GetInt("LeftWaist", -1);
        _currentItems[5] = PlayerPrefs.GetInt("RightWaist", -1);
        _currentItems[6] = PlayerPrefs.GetInt("LeftLeg", -1);
        _currentItems[7] = PlayerPrefs.GetInt("RightLeg", -1);
        
        for (int i = 0; i < 8; i++) 
        {
            SetNewItem((ItemZone) i, _currentItems[i]);
        }
    }

    void SetNewItem(ItemZone itemZone, int itemId)
    {
        if(itemId == -1) 
        {
            return;
        }
        if(_currentItems[(int)itemZone] != -1)
            ResetItemZone(itemZone); // inactivate old item, which is gonna be replaced

        PlayerPrefs.SetInt($"{itemZone}", itemId);
        _currentItems[(int) itemZone] = itemId;
        ChangeEquippedItemIcon(itemZone, itemId);
        _itemsPreviewImages[itemId].gameObject.SetActive(true);
        if(itemZone == ItemZone.LeftHand || itemZone == ItemZone.LeftLeg || itemZone == ItemZone.LeftLeg)
        {
            Vector2 position = _itemsPreviewImages[itemId].anchoredPosition;
            position.x = -position.x;
            _itemsPreviewImages[itemId].anchoredPosition = position;
            _itemsPreviewImages[itemId].localScale = new Vector2(-1, 1);
        }
        _items[itemId].GetComponent<Image>().color = _activeColor;
        ResetSelection();
    }

    void ResetSelection()
    {
        if(selectedItem != -1) 
        {
            _items[selectedItem].transform.localScale = new Vector2(1f, 1f);
        }
        selectedItem = -1;
        selectedItemType = ItemType.None;
    }

    void ResetItemZone(ItemZone itemZone)
    {
        int itemId = _currentItems[(int)itemZone]; 
        _items[itemId].GetComponent<Image>().color = _inactiveColor;
        _itemsPreviewImages[itemId].gameObject.SetActive(false);
        Vector2 position = _itemsPreviewImages[itemId].anchoredPosition;
        if(itemZone == ItemZone.LeftHand || itemZone == ItemZone.LeftLeg || itemZone == ItemZone.LeftLeg)
        {
            position.x = Mathf.Abs(position.x);
            _itemsPreviewImages[itemId].anchoredPosition = position;
        }    
        _itemsPreviewImages[itemId].localScale = new Vector2(1, 1);
        _currentItems[(int)itemZone] = -1;
        PlayerPrefs.SetInt($"{itemZone}", -1);
        ChangeEquippedItemIcon(itemZone, -1);
    }

    void ChangeEquippedItemIcon(ItemZone itemZone, int itemId)
    {
        if(itemId == -1)
        {
			SetDefaultIcon(itemZone);
        }
        else 
        {
        	Sprite newSprite = _items[itemId].transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
        	_equippedItems[(int)itemZone].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = newSprite; 
        }
    }

    void SetDefaultIcon(ItemZone itemZone)
    {
		_equippedItems[(int)itemZone].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = null;
    }

    void ShowItemInfo()
    {
        //show info about item
    } 

}
