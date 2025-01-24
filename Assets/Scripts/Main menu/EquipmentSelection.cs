using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public enum ItemZone{
    Head,
    Face,
    LeftHand,
    RightHand,
    LeftWaist,
    RightWaist,
    LeftLeg,
    RightLeg,
    None
}

public enum ItemType{
    Head,
    Face,
    Hand,
    Waist,
    Leg,
    None
}

public class EquipmentSelection : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] private GameObject _slotsParent;
    [SerializeField] private List<Sprite> _slotDefaultImages; 
    [SerializeField] private List<ItemImages> _itemsImages;
    [Header("Info Panel Settings")]
    [SerializeField] private GameObject _infoPanel;
    [SerializeField] private Image _imageForInfoPanel;
    [SerializeField] private TMP_Text _itemDescription;
    [Header("Buttons Settings")]
    [SerializeField] private Transform _buttonsParent;
    [SerializeField] private GameObject _buttonPrefab; 
    [SerializeField] private RectTransform _startingPoint;
    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _inactiveColor;
    [SerializeField] private float _xOffset;
    [SerializeField] private float _yOffset;
    
    [System.Serializable]
    public class ItemImages
    {
        public int Id;
        public ItemType ItemType;
        public Sprite ButtonImage;
        public GameObject PreviewImageInEquipmentMenu;
        public GameObject PreviewImageInMainMenu;
        public string ItemDescription; 
    }
    private List<GameObject> _slots;
    private List<GameObject> _buttons = new();

    private int[] _unlockedItemsId = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16};
    //private int[] _unlockedItemsId = {0, 1, 2, 3, 4, 5};
    private int[] _currentItems = {-1, -1, -1, -1, -1, -1, -1, -1};
    private int _selectedItem = -1;
    private ItemType _selectedItemType = ItemType.None;
    private ItemZone _currentInfoItemZone;

    void Awake()
    {
        _buttons = new();
        LoadChildrenOfParentToList(_slotsParent, ref _slots);
        SetUpButtons();
    }

    void SetUpButtons()
    {
        for (int i = 0; i < _unlockedItemsId.Length; i++)
        {
            GameObject buttonObject = InstantiateButton(i);
            _buttons.Add(buttonObject);
            int id = _unlockedItemsId[i];
            ItemImages itemImages = _itemsImages.FirstOrDefault(itemImages => itemImages.Id == id);
            Button button = buttonObject.GetComponent<Button>();
            button.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = itemImages.ButtonImage;
            int index = id;
            button.onClick.AddListener(() => SelectItem(index));
        }

        for (int i = 0; i < _slots.Count; i++)
        {
            GameObject slot = _slots[i];
            Button button = slot.GetComponent<Button>();
            int index = i; // Capture the current value of i
            button.onClick.AddListener(delegate { OnClickSlot(index); });
        }
    }

    GameObject InstantiateButton(int id)
    {
        float xPosition = (id % 5) * _xOffset;
        float yPosition = -(id / 5) * _yOffset;

        GameObject newButton = Instantiate(_buttonPrefab, _buttonsParent);

        RectTransform rectTransform = newButton.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = _startingPoint.anchoredPosition + new Vector2(xPosition, yPosition);

        return newButton;
    }

    void LoadChildrenOfParentToList(GameObject parent, ref List<GameObject> list)
    {
        list = new();
        foreach (Transform child in parent.transform)
        {
            if (child != null)
            {
                list.Add(child.gameObject);
            }
        }
    }

    void Start()
    {
        LoadCurrentItems();
    } 

    public void SelectItem(int itemId)
    {
        if(itemId == _selectedItem)
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
            if(_selectedItem == -1){
                int itemZoneId = 0;
                for(int i = 0; i < _currentItems.Length; i++)
                {
                    if(_currentItems[i] == itemId)
                    {
                        itemZoneId = i;
                        break;
                    }
                }
                //ResetItemSlot((ItemZone)itemZoneId);
                ShowItemInfo((ItemZone)itemZoneId);
            } 
            return;
        } 
        else 
        {
            ResetSelection();
            _selectedItem = itemId;
            ItemImages itemImages = _itemsImages.FirstOrDefault(itemImages => itemImages.Id == itemId); 
            _selectedItemType = itemImages.ItemType;
            GameObject button = GetButtonWithItem(itemId);
            button.transform.localScale = new Vector2(1.2f, 1.2f);
        }
    }

    GameObject GetButtonWithItem(int itemId)
    {
        for(int i = 0; i < _unlockedItemsId.Length; i++)
        {
            if(_unlockedItemsId[i] == itemId) return _buttons[i];
        }
        return null;
    }

    public void OnClickSlot(int itemZoneId)
    {
        if(_selectedItem == -1)
        {
            if(_currentItems[itemZoneId] != -1)
            {
                //ResetItemSlot((ItemZone)itemZoneId);
                ShowItemInfo((ItemZone)itemZoneId);
            }
        } 
        else 
        {
            bool appropriateZone = CheckZone((ItemZone)itemZoneId);
            if(appropriateZone)
            {
                SetNewItem((ItemZone)itemZoneId, _selectedItem);
            }
        }
    }

    bool CheckZone(ItemZone itemZone)
    {
        if(itemZone == ItemZone.Face && _selectedItemType == ItemType.Face)
        {
            return true;
        } 
        else if(itemZone == ItemZone.Head && _selectedItemType == ItemType.Head)
        {
            return true;
        }
        else if((itemZone == ItemZone.RightHand || itemZone == ItemZone.LeftHand) && _selectedItemType == ItemType.Hand)
        {
            return true;
        }
        else if((itemZone == ItemZone.RightWaist || itemZone == ItemZone.LeftWaist) && _selectedItemType == ItemType.Waist)
        {
            return true;
        }
        else if((itemZone == ItemZone.RightLeg || itemZone == ItemZone.LeftLeg) && _selectedItemType == ItemType.Leg)
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
        if(_currentItems[(int)itemZone] != -1)
            ResetItemSlot(itemZone); // inactivate old item, which is gonna be replaced
        
        ChangeSlotIcon(itemZone, itemId);
        
        if(itemId == -1) 
        {
            return;
        }

        PlayerPrefs.SetInt($"{itemZone}", itemId);
        _currentItems[(int) itemZone] = itemId;
        ShowPreviewImage(itemZone, itemId);
        
        GameObject button = GetButtonWithItem(itemId);
        button.GetComponent<Image>().color = _activeColor;
        ResetSelection();
    }

    void ShowPreviewImage(ItemZone itemZone, int itemId)
    {
        ItemImages itemImages = _itemsImages.FirstOrDefault(itemImages => itemImages.Id == itemId);
        itemImages.PreviewImageInEquipmentMenu.SetActive(true);
        itemImages.PreviewImageInMainMenu.SetActive(true);

        if(itemZone == ItemZone.LeftHand || itemZone == ItemZone.LeftLeg)
        {
            Vector2 position = itemImages.PreviewImageInEquipmentMenu.GetComponent<RectTransform>().anchoredPosition;
            position.x = -position.x;
            itemImages.PreviewImageInEquipmentMenu.GetComponent<RectTransform>().anchoredPosition = position;
            itemImages.PreviewImageInEquipmentMenu.transform.localScale = new Vector2(-1, 1);

            position = itemImages.PreviewImageInMainMenu.GetComponent<RectTransform>().anchoredPosition;
            position.x = -position.x;
            itemImages.PreviewImageInMainMenu.GetComponent<RectTransform>().anchoredPosition = position;
            itemImages.PreviewImageInMainMenu.transform.localScale = new Vector2(-1, 1);
        }
    }

    void HidePreviewImage(ItemZone itemZone)
    {
        int itemId = _currentItems[(int)itemZone]; 
        ItemImages itemImages = _itemsImages.FirstOrDefault(itemImages => itemImages.Id == itemId);
        itemImages.PreviewImageInEquipmentMenu.SetActive(false);
        itemImages.PreviewImageInMainMenu.SetActive(false);

        Vector2 position = itemImages.PreviewImageInEquipmentMenu.GetComponent<RectTransform>().anchoredPosition;
        if(itemZone == ItemZone.LeftHand || itemZone == ItemZone.LeftLeg)
        {
            position.x = Mathf.Abs(position.x);
            itemImages.PreviewImageInEquipmentMenu.GetComponent<RectTransform>().anchoredPosition = position;
        }    
        itemImages.PreviewImageInEquipmentMenu.transform.localScale = new Vector2(1, 1);

        position = itemImages.PreviewImageInMainMenu.GetComponent<RectTransform>().anchoredPosition;
        if(itemZone == ItemZone.LeftHand || itemZone == ItemZone.LeftLeg || itemZone == ItemZone.LeftLeg)
        {
            position.x = Mathf.Abs(position.x);
            itemImages.PreviewImageInMainMenu.GetComponent<RectTransform>().anchoredPosition = position;
        }    
        itemImages.PreviewImageInMainMenu.transform.localScale = new Vector2(1, 1);
    }

    void ResetSelection()
    {
        if(_selectedItem != -1) 
        {
            GameObject button = GetButtonWithItem(_selectedItem);
            button.transform.localScale = new Vector2(1f, 1f);
        }
        _selectedItem = -1;
        _selectedItemType = ItemType.None;
    }

    public void ResetItemSlotForButton()
    {
        ResetItemSlot(_currentInfoItemZone);
        HideItemInfo();
    }
    
    void ResetItemSlot(ItemZone itemZone)
    {
        int itemId = _currentItems[(int)itemZone]; 
        GameObject button = GetButtonWithItem(itemId);
        button.GetComponent<Image>().color = _inactiveColor;
        HidePreviewImage(itemZone);
        _currentItems[(int)itemZone] = -1;
        PlayerPrefs.SetInt($"{itemZone}", -1);
        ChangeSlotIcon(itemZone, -1);
    }

    void ChangeSlotIcon(ItemZone itemZone, int itemId)
    {
        if(itemId == -1)
        {
			SetDefaultIcon(itemZone);
        }
        else 
        {
            GameObject button = GetButtonWithItem(itemId);
        	Sprite newSprite = button.transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
        	_slots[(int)itemZone].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = newSprite; 
        }
    }

    void SetDefaultIcon(ItemZone itemZone)
    {
		_slots[(int)itemZone].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = _slotDefaultImages[(int)itemZone];
    }

    void ShowItemInfo(ItemZone itemZone)
    {
        _currentInfoItemZone = itemZone;
        _infoPanel.SetActive(true);
        int itemId = _currentItems[(int)itemZone]; 
        _imageForInfoPanel.sprite = _itemsImages[itemId].ButtonImage;
        _itemDescription.text = _itemsImages[itemId].ItemDescription;
    }

    public void HideItemInfo()
    {
        _currentInfoItemZone = ItemZone.None;
        _infoPanel.SetActive(false);
    }
}
