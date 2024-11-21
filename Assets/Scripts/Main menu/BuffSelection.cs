using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffSelection : MonoBehaviour
{
    private int[] currentBuffs = {0, 0, 0};
    private int selectedBuff = -1;
    
    [SerializeField] private GameObject[] buffs;  
    [SerializeField] private GameObject[] deckBuffs; 

    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;

    void Start()
    {
        LoadCurrentBuffs();
    } 

    void LoadCurrentBuffs()
    {
        for (int i = 0; i < 3; i++) 
        {
            currentBuffs[i] = PlayerPrefs.GetInt("Buff " + i, 0);
            SetNewBuff(i, currentBuffs[i]);
        }
    }

    void SetNewBuff(int buffPosition, int buffId)
    {
        if(buffId == 0) {
            return; // no buff at the start of the game, as new players dont have any buffs
        }
        InactivateBuff(buffPosition);
        PlayerPrefs.SetInt("Buff " + buffPosition, buffId);
        currentBuffs[buffPosition] = buffId;
        ChangeDeckBuffIcon(buffPosition, buffId);
        buffs[buffId].transform.localScale = new Vector2(1f, 1f);
        buffs[buffId].GetComponent<Image>().color = activeColor;
        selectedBuff = -1;
    }

    void InactivateBuff(int buffPosition)
    {
        if(currentBuffs[buffPosition] != 0)
            buffs[currentBuffs[buffPosition]].GetComponent<Image>().color = inactiveColor;
    }

    void ChangeDeckBuffIcon(int buffPosition, int buffId)
    {
        Sprite newSprite = buffs[buffId].transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
        deckBuffs[buffPosition].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = newSprite;
    }

    public void SelectBuff(int buffId)
    {
        if(buffId == selectedBuff)
        { 
            selectedBuff = -1; //remove shake also  
            buffs[buffId].transform.localScale = new Vector2(1f, 1f);
        } 
        else if(buffId == currentBuffs[0] || buffId == currentBuffs[1] || buffId == currentBuffs[2])
        {
            return;
        } 
        else 
        {
            if(selectedBuff != -1) 
            {
                buffs[selectedBuff].transform.localScale = new Vector2(1f, 1f);
            }
            selectedBuff = buffId;
            //set icon on fire or change sprite or something else
            //make all other icons shake

            buffs[buffId].transform.localScale = new Vector2(1.2f, 1.2f);
        }
    }

    void ShowBuffInfo()
    {
        //show info about buff
    } 

    public void OnClickDeckBuff(int buffPosition)
    {
        if(selectedBuff == -1)
        {
            ShowBuffInfo();
        } 
        else 
        {
            SetNewBuff(buffPosition, selectedBuff);
        }
    }
}
