using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using UnityEngine;

public class VideoAdScript : MonoBehaviour
{
    public string gameId = "3922655";
    public string placementId;
    public bool testMode;

    public void ShowVideoAd()
    {
        StartCoroutine(ShowVideo());
    }
    private IEnumerator ShowVideo()
    {
        Advertisement.Initialize(gameId, testMode);

        while (!Advertisement.isInitialized)
        {
            yield return new WaitForSeconds(0.2f);
        }

        Advertisement.Show(placementId);
    }
}
