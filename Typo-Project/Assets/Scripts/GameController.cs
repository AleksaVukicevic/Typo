using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private WordPicker wordPicker;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Image indicator;

    [Header("Other")]
    [SerializeField] private Color indicatorColor;
    [SerializeField] private float delay;

    public string word;

    private void Start()
    {
        NextWord();
    }

    private void Update()
    {
        inputFocuser();
        skipDetector();

        if (inputField.text.ToLower() == word.ToLower())
        {
            print("Add score");

            StartCoroutine(SetIndicatorColor(Color.green,0f));
            inputField.text = string.Empty;
            NextWord();
        }
    }
    private void inputFocuser()
    {
        if (inputField.IsActive() == true && inputField.isFocused == false)
        {
            inputField.ActivateInputField();
        }
    }

    private void skipDetector()
    {
        if (inputField.text.Contains(" "))
        {
            StartCoroutine(SetIndicatorColor(Color.red, 0f));
            inputField.text = string.Empty;
            Invoke("NextWord", delay);
        }
    }
    IEnumerator SetIndicatorColor(Color color, float time)
    {
        yield return new WaitForSeconds(time);
        indicator.color = color;
    }
    private void NextWord()
    {
        word = wordPicker.Pick();
        StartCoroutine(SetIndicatorColor(indicatorColor, 0.5f));

    }
}
