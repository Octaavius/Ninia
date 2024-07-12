using TMPro;
using UnityEngine;

public class ShowPlayerInfo : MonoBehaviour
{
    public TMP_Text ScoreText;
    public TMP_Text CoinsText;
    public TMP_Text GemsText;
    void Awake(){
        PlayerData data = SaveSystem.LoadPlayer();

        Debug.Log(data.BestScore.ToString());
        Debug.Log(data.Coins.ToString());
        Debug.Log(data.Gems.ToString());

        ScoreText.text = data.BestScore.ToString();
        CoinsText.text = data.Coins.ToString();
        GemsText.text = data.Gems.ToString();
    }
    //skins and achievments
}
