using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI signedInText;
    [SerializeField] private Image signedInImage;
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


    public void OpenOptionsMenu()
    {   
        if (PlayGamesScript.IsAuthenticated())
        {
            signedInImage.color = Color.green;
            signedInText.text = "Sign out";
        }
        else
        {
            signedInImage.color = Color.red;
            signedInText.text = "Sign in";
        }
    }

    public void OpenPrivacyPolicy()
    {
        Application.OpenURL("https://typogame.wordpress.com/privacy-policy/");
    }

}
