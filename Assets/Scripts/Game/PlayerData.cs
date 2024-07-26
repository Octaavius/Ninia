[System.Serializable]
public class PlayerData
{
    public int BestScore;
    public int Coins;
    public int Gems; 
    public PlayerData (PlayerInfo playerInfo){
        BestScore = playerInfo.BestScore;
        Coins = playerInfo.Coins;
        Gems = playerInfo.Gems;
    }
    public PlayerData(){
        BestScore = 0;
        Coins = 0;
        Gems = 0;
    }
}
