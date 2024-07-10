using UnityEngine;

[System.Serializable]
public class PlayerData : MonoBehaviour
{
    public int BestScore;
    public int Coins;
    public int Gems; 
    public PlayerData (Player player){
        BestScore = player.BestScore;
        Coins = player.Coins;
        Gems = player.Gems;
    }
}
