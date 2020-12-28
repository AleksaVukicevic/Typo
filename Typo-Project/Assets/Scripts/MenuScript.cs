using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bestScoreText;
    void Start()
    {
        if (PlayerPrefs.HasKey("bestScore"))
        {
            int best = PlayerPrefs.GetInt("bestScore");
            if (best > 1)
            {
                bestScoreText.text = $"Best     <b>{best}";
            }
        }
        else if(PlayGamesScript.IsAuthenticated())
        {
            long best = PlayGamesScript.LoadScoreFromLeaderboard();
            if (best > 1)
            {
                bestScoreText.text = $"Best     <b>{best}";
                PlayerPrefs.SetInt("bestScore",(int)best);
            }
        }
    }
}
