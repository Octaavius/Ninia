using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootStrap : MonoBehaviour
{
    public List<GameObject> NinjaSkins;
    private List<RectTransform> Buffs;
    [SerializeField] private List<GameObject> _itemsImages;
    [SerializeField] private RectTransform _buffsStartingPoint;

    void Awake()
    {
        Buffs = BuffsManager.Instance.BuffsTransform;
    }

    void Start()
    {
        StartCoroutine(StartFunction());
        LoadSkin();
        LoadBuffs();
        LoadItems();
    }

    void LoadItems()
    {
        int[] currentItems = {
            PlayerPrefs.GetInt("Head", -1),
            PlayerPrefs.GetInt("Face", -1),
            PlayerPrefs.GetInt("RightHand", -1),
            PlayerPrefs.GetInt("LeftHand", -1),
            PlayerPrefs.GetInt("RightWaist", -1),
            PlayerPrefs.GetInt("LeftWaist", -1),
            PlayerPrefs.GetInt("RightLeg", -1),
            PlayerPrefs.GetInt("LeftLeg", -1)
        };

        for(int i = 0; i < 8; i++){
            int itemId = currentItems[i];
            if(itemId != -1)
            {
                _itemsImages[itemId].SetActive(true);
                if(i == 3 || i == 5 || i == 7)
                {
                    Vector3 position = _itemsImages[itemId].transform.position;
                    position.x = -position.x;
                    _itemsImages[itemId].transform.position = position;

                    Vector3 scale = _itemsImages[itemId].transform.localScale;
                    scale.x = -scale.x;
                    _itemsImages[itemId].transform.localScale = scale;
                }

            }
        }
        ItemsManager.Instance.ActivateItems(currentItems);
    }

    void LoadBuffs()
    {
        RectTransform currentButtonPoint = _buffsStartingPoint;
        for (int i = 0; i < 3; i++) 
        {
            int buffId = PlayerPrefs.GetInt("Buff " + i, 0);
            if (buffId != 0) 
            {
                Buffs[buffId].gameObject.SetActive(true);
                Buffs[buffId].anchoredPosition = currentButtonPoint.anchoredPosition;
                currentButtonPoint.anchoredPosition += new Vector2(0, 200);
            }
        }
    }

    void LoadSkin()
    {
        int currentSkin = PlayerPrefs.GetInt("CurrentSkin", 0);
        NinjaSkins[currentSkin].SetActive(true);
    }

    IEnumerator StartFunction()
    {
        while (SceneManagerScript.Instance == null) 
        {
            yield return null; // Wait until the next frame
        }
        SceneManagerScript.Instance.FadeOutScript.PlayFadeOut();
    }
}
