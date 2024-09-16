using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public int BestScore{ get; set; }
    public int Coins{ get; set; }
    public int Gems{ get; set; }
    public int[] UnlockedSkins { get; set;}
    public int[] UpgradesLevels { get; set; }

    void Awake() {
        LoadPlayer();
    }
    
    public void SaveData() {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer() {
        PlayerData data = SaveSystem.LoadPlayer();

        BestScore = data.BestScore;
        Coins = data.Coins;
        Gems = data.Gems;
        
        UnlockedSkins = data.UnlockedSkins;
        if(UnlockedSkins == null)
            UnlockedSkins = new int[] {1, 0, 0, 0, 0, 0};
        UpgradesLevels = data.UpgradesLevels;
        if(UpgradesLevels == null) 
            UpgradesLevels = new int[] {0, 0, 0, 0, 0, 0};
    }

    public void AddMoney(int coins, int gems)
    {
        Coins += coins;
        Gems += gems;
    }

}
