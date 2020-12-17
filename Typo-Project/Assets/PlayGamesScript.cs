using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using TMPro;


public class PlayGamesScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI debugText;

    private void Start()
    {
        AuthenticateUser();
    }

    private void AuthenticateUser()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) =>
        {
            if (result == SignInStatus.Success)
            {
                debugText.text = $"Signed in. Status: {result}, future scores will be submitted";
            }
            else if (result == SignInStatus.Failed)
            {
                debugText.text = $"Error Signing in. Status: {result}, Scores won't be submitted.";
            }
        });

        //Social.localUser.Authenticate((bool success) =>
        //{
        //    if (success == true)
        //    {
        //        debugText.text = "Logged in";
        //    }
        //    else
        //    {
        //        debugText.text = "Error loging in";
        //    }
        //});

    }

    public void LogIn()
    {
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptAlways, (result) =>
        {
            if (result == SignInStatus.Success)
            {
                debugText.text = $"Signed in. Status: {result}";
            }
            else if (result == SignInStatus.Failed)
            {
                debugText.text = $"Error Signing in. Status: {result}";
            }
        });

        //Social.localUser.Authenticate((bool success) =>
        //{
        //    if (success == true)
        //    {
        //        debugText.text = "Logged in";
        //    }
        //    else
        //    {
        //        debugText.text = "Error loging in";
        //    }
        //});
    }

    public static void PostScoreToLeaderboard(long score)
    {
        Social.ReportScore(score, GPGSIds.leaderboard_high_score_60s, (bool success) =>
        {
            if (success)
            {
                print("Score posted");

            }
            else
            {
                Debug.LogError("Error posting score");
            }
        });
    }

    public void ShowLeaderboard()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_high_score_60s);
    }
}
