using TMPro;
using UnityEngine;

public class ShowPlayerInfo : MonoBehaviour
{
    public TMP_Text ScoreText;
    public TMP_Text CoinsText;
    public TMP_Text GemsText;

    [SerializeField] private  PlayerInfo playerInfo;
    
    void Awake(){
        UpdateTexts();
    }

    public void UpdateTexts(){
        ScoreText.text = playerInfo.BestScore.ToString();
        CoinsText.text = playerInfo.Coins.ToString();
        GemsText.text = playerInfo.Gems.ToString();
    }
    //skins and achievments
}
