[System.Serializable]
public class PlayerData
{
    public int BestScore;
    public int Coins;
    public int Gems;
    public int[] UnlockedSkins; 
    public PlayerData (PlayerInfo playerInfo){
        BestScore = playerInfo.BestScore;
        Coins = playerInfo.Coins;
        Gems = playerInfo.Gems;
        UnlockedSkins = playerInfo.UnlockedSkins;
    }
    public PlayerData(){
        BestScore = 0;
        Coins = 0;
        Gems = 0;
        UnlockedSkins = new int[] {1, 0, 0, 0, 0, 0};
    }
}
