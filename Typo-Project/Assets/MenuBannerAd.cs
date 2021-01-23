using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using TMPro;

public class MenuBannerAd : MonoBehaviour
{
    public string gameId = "3922655";
    public string placementId;
    public bool testMode;

    //DEBUG
    //public TextMeshProUGUI debug;

    void Awake()
    {        
        StartCoroutine(ShowBanner());
    }
    private IEnumerator ShowBanner()
    {
        //debug.text = "Initializeing ads";
        Advertisement.Initialize(gameId, testMode);

        while (!Advertisement.isInitialized)
        {
            yield return new WaitForSeconds(0.2f);
            //debug.text = "Waiting for initialization";
        }
        //debug.text = "Initialized";

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        //debug.text = "Set Position to BOTTOM_CENTER";

        Advertisement.Banner.Show(placementId);
        //debug.text = "Showed placement";
    }
}


// LOGCAT FOR AD STUFF
// >> "adb logcat -v time UnityAds:V *:S"

// NO NEED TO LOOK HERE
// FOR THE POSITION 


//public enum PositionEnum { top, bottom };
//public PositionEnum position;


//if (position == PositionEnum.top)
//{
//    Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
//}
//else if(position == PositionEnum.bottom)
//{
//    Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
//}
