using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SplashTextScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI splashText;
    void Start()
    {
        splashText.text = SplashTextList.lines[Random.Range(0, SplashTextList.lines.Length)];
    }
}
