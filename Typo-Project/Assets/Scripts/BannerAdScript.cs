using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAdScript : MonoBehaviour
{
    public enum PositionEnum { top,bottom };
    public PositionEnum position;

    public string gameId = "3922655";
    public string placementId = "banner";
    public bool testMode = true;
    public bool closed;

    void Awake()
    {
        Advertisement.Initialize(gameId, testMode);
        StartCoroutine(ShowBannerWhenInitialized());
    }

    IEnumerator ShowBannerWhenInitialized()
    {
        while (!Advertisement.isInitialized)
        {
            yield return new WaitForSeconds(0.2f);
        }
        if (!closed)
        {
            if (position == PositionEnum.top)
            {
                Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
            }
            else
            {
                Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
            }
            Advertisement.Banner.Show(placementId);
        }
    }

    public void CloseAd()
    {
        closed = true;
        Advertisement.Banner.Hide(false);
    }

}
