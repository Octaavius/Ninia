[System.Serializable]
public class PlayerData
{
    public int BestScore;
    public int Coins;
    public int Gems; 
    public PlayerData (Player player){
        BestScore = player.BestScore;
        Coins = player.Coins;
        Gems = player.Gems;
    }
    public PlayerData(){
        BestScore = 0;
        Coins = 0;
        Gems = 0;
    }
}
