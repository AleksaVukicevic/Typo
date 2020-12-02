using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bestScoreText;
    void Start()
    {
        bestScoreText.text = $"Best     <b>{PlayerPrefs.GetInt("bestScore")}";
    }
}
