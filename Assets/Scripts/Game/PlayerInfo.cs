using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public int BestScore{ get; set; }
    public int Coins{ get; set; }
    public int Gems{ get; set; }
    public int[] UnlockedSkins {get; set;}

    void Awake() {
        LoadPlayer();
    }
    
    public void SaveData() {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer() {
        PlayerData data = SaveSystem.LoadPlayer();

        UnlockedSkins = data.UnlockedSkins;
        BestScore = data.BestScore;
        Coins = data.Coins;
        Gems = data.Gems;
    }

    public void AddMoney(int coins, int gems)
    {
        Coins += coins;
        Gems += gems;
    }

}
