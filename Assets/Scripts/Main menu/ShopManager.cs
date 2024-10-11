using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private PlayerInfo playerInfo;
    
    [Header("Skins")]
    //list of skins
    public int[] SkinPricesInCoins;
    public int[] SkinPricesInGems;
    public GameObject[] LockedSkinButtons;
    public GameObject[] UnlockedSkinButtons;
    public Color selectedButtonColor;
    public Color selectButtonColor;
    
    private int[] UnlockedSkins;

    [Header("Upgrades")]
    //list of upgrades
    public int[] UpgradesLevelsPrice;
    public GameObject[] MinusButtons;
    public GameObject[] PlusButtons;
    public TMP_Text[] UpgradesLevelTexts;

    private int[] UpgradesLevels;
    private const int MAX_LEVEL = 5;

    //list of buttons


    void Awake() {
        //////////////////
        playerInfo.LoadPlayer();
        //////////////////

        UnlockedSkins = playerInfo.UnlockedSkins;
        SetSkinButtonTypes();
        SetSkinPrices();

        int currentSkin = PlayerPrefs.GetInt("CurrentSkin", 0);
        SelectSkin(currentSkin);
        
        UpgradesLevels = playerInfo.UpgradesLevels;
        SetUpgradesLevels();
        
        //2 load levels and prices to buttons
    }

    void SetSkinPrices(){
        for(int i = 0; i < LockedSkinButtons.Length; i++){
            Transform coinsTextObj = LockedSkinButtons[i].transform.GetChild(0).GetChild(0);
            Transform gemsTextObj = LockedSkinButtons[i].transform.GetChild(1).GetChild(0);

            TMP_Text coinsText = coinsTextObj.GetComponent<TMP_Text>();
            TMP_Text gemsText = gemsTextObj.GetComponent<TMP_Text>();

            coinsText.text = SkinPricesInCoins[i].ToString() + " coins";
            gemsText.text = SkinPricesInGems[i].ToString();
        }
    }

    void SetSkinButtonTypes(){
        for(int i = 0; i < UnlockedSkins.Length; i++){
            if (UnlockedSkins[i] == 0) {
                SetSkinButtonType(i, 0);
            } else if (UnlockedSkins[i] == 1) {
                SetSkinButtonType(i, 1);
            } else {
                return;
            }
        }
    }

    void SetSkinButtonType(int skinId, int buttonType){
        if(buttonType == 0){
            LockedSkinButtons[skinId].SetActive(true);
            UnlockedSkinButtons[skinId].SetActive(false);
        } else {
            UnlockedSkinButtons[skinId].SetActive(true);
            LockedSkinButtons[skinId].SetActive(false);
        }
    }

    public void UnlockSkinWithCoins(int skinId){
        int currentPlayerCoinBalance = playerInfo.Coins;
        int skinPrice = GetSkinPriceInCoins(skinId); 
        if(skinPrice <= currentPlayerCoinBalance){
            UnlockSkin(skinId);
            playerInfo.Coins -= skinPrice;
        } else {
            // show error message that you dont have enough money, or just do it in awake or in setSkinButtonTypes
        }
    }

    public void UnlockSkinWithGems(int skinId){
        int currentPlayerGemBalance = playerInfo.Gems;
        int skinPrice = GetSkinPriceInGems(skinId); 
        if(skinPrice <= currentPlayerGemBalance){
            UnlockSkin(skinId);
            playerInfo.Gems -= skinPrice;
        } else {
            // show error message that you dont have enough money, or just do it in awake or in setSkinButtonTypes
        }
    }

    int GetSkinPriceInCoins(int skinId){
        return SkinPricesInCoins[skinId];
    }

    int GetSkinPriceInGems(int skinId){
        return SkinPricesInGems[skinId];
    }

    void UnlockSkin(int skinId){
        playerInfo.UnlockedSkins[skinId] = 1;
        playerInfo.SaveData();
        SetSkinButtonType(skinId, 1);
        SelectSkin(skinId);
    }

    public void SelectSkin(int skinId){
        GameObject newSkinButton = UnlockedSkinButtons[skinId];
        
        int prevSkinId = PlayerPrefs.GetInt("CurrentSkin", 0);
        if(prevSkinId != skinId) {
            //change color
            GameObject prevButton = UnlockedSkinButtons[prevSkinId];
            Image prevButtonImage = prevButton.GetComponent<Image>();
            prevButtonImage.color = selectButtonColor;
            //change text
            Transform prevButtonTextObj = prevButton.transform.GetChild(0); 
            TMP_Text prevButtonText = prevButtonTextObj.GetComponent<TMP_Text>();
            prevButtonText.text = "Select";
            prevButtonText.color = Color.black;
        }
        
        Image newSkinButtonImage = newSkinButton.GetComponent<Image>();
        newSkinButtonImage.color = selectedButtonColor;
        //change text
        Transform newSkinButtonObj = newSkinButton.transform.GetChild(0); 
        TMP_Text newSkinButtonText = newSkinButtonObj.GetComponent<TMP_Text>();
        newSkinButtonText.text = "Selected";
        newSkinButtonText.color = Color.white;


        PlayerPrefs.SetInt("CurrentSkin", skinId);
    }

    void SetUpgradesLevels(){
        for(int i = 0; i < UpgradesLevels.Length; i++){
            int upgradeLevel = UpgradesLevels[i];
            UpgradesLevelTexts[i].text = upgradeLevel.ToString();
            if(upgradeLevel == 0){
                //set minus to unavaliable
            } else if (upgradeLevel == MAX_LEVEL) {
                //set plus to unavaliable
            }
        }
    }

    //FUNCTION AND BUTTON WITH THIS FUNCTION ONLY FOR TESTS 
    public void Reset() {
        playerInfo.UnlockedSkins = new int[] {1, 0, 0, 0, 0, 0};
        UnlockedSkins = playerInfo.UnlockedSkins;
        playerInfo.UpgradesLevels = new int[] {0, 0, 0, 0, 0, 0};
        UpgradesLevels = playerInfo.UpgradesLevels;
        playerInfo.SaveData();
        SelectSkin(0);
        SetSkinButtonTypes();
        SetUpgradesLevels();

    }

    public void Add10000CoinsAnd100Gems(){
        playerInfo.Coins += 10000;
        playerInfo.Gems += 100;
        playerInfo.SaveData();
    }
}
