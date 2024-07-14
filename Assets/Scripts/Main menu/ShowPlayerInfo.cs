using TMPro;
using UnityEngine;

public class ShowPlayerInfo : MonoBehaviour
{
    public TMP_Text ScoreText;
    public TMP_Text CoinsText;
    public TMP_Text GemsText;
    void Awake(){
        PlayerData data = SaveSystem.LoadPlayer();

        ScoreText.text = data.BestScore.ToString();
        CoinsText.text = data.Coins.ToString();
        GemsText.text = data.Gems.ToString();
    }
    //skins and achievments
}
