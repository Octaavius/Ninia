using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffSelection : MonoBehaviour
{
    private int[] CurrentBuffs = {0, 0, 0};
    private int SelectedBuff = -1;
    
    [SerializeField] private GameObject[] Buffs;  
    [SerializeField] private GameObject[] DeckBuffs; 

    [SerializeField] private Color ActiveColor;
    [SerializeField] private Color InactiveColor;

    void Start(){
        LoadCurrentBuffs();
    } 

    void LoadCurrentBuffs(){
        for (int i = 0; i < 3; i++) {
            CurrentBuffs[i] = PlayerPrefs.GetInt("Buff " + i, 0);
            //CurrentBuffs[i] = 0;
            SetNewBuff(i, CurrentBuffs[i]);
        }
    }

    void SetNewBuff(int buffPosition, int buffId){
        if(buffId == 0) {
            return; // no buff at the start of the game, as new players dont have any buffs
        }
        if(CurrentBuffs[buffPosition] != 0)
            Buffs[CurrentBuffs[buffPosition]].GetComponent<Image>().color = InactiveColor;
        PlayerPrefs.SetInt("Buff " + buffPosition, buffId);
        CurrentBuffs[buffPosition] = buffId;
        ChangeDeckBuffIcon(buffPosition, buffId);
        Buffs[buffId].transform.localScale = new Vector2(1f, 1f);
        Buffs[buffId].GetComponent<Image>().color = ActiveColor;
        //make icon grey in buff list
        SelectedBuff = -1;
    }

    void ChangeDeckBuffIcon(int buffPosition, int buffId){
        Sprite newSprite = Buffs[buffId].transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
        DeckBuffs[buffPosition].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = newSprite;
    }

    public void SelectBuff(int buffId){
        if(buffId == SelectedBuff){ 
            SelectedBuff = -1; //remove shake also  
            Buffs[buffId].transform.localScale = new Vector2(1f, 1f);
        } else if(buffId == CurrentBuffs[0] || buffId == CurrentBuffs[1] || buffId == CurrentBuffs[2]){
            return;
        } else {
            if(SelectedBuff != -1) {
                Buffs[SelectedBuff].transform.localScale = new Vector2(1f, 1f);
            }
            SelectedBuff = buffId;
            //set icon on fire or change sprite or something else
            //make all other icons shake

            Buffs[buffId].transform.localScale = new Vector2(1.2f, 1.2f);
        }
    }

    void ShowBuffInfo(){
        //show info about buff
    } 

    public void DeckBuffAction(int buffPosition){
        if(SelectedBuff == -1){
            ShowBuffInfo();
        } else {
            SetNewBuff(buffPosition, SelectedBuff);
        }
    }

}
