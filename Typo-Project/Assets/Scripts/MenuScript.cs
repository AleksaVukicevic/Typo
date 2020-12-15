using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bestScoreText;
    void Start()
    {
        int best = PlayerPrefs.GetInt("bestScore");
        if(best > 1)
        {
            bestScoreText.text = $"Best     <b>{best}";
            //PlayGamesScript.PostScoreToLeaderboard(best);
        }
        
    }
}
