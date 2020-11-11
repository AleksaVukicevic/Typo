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

            SetIndicatorColor(Color.green);
            inputField.text = string.Empty;
            Invoke("NextWord", delay);
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
            SetIndicatorColor(Color.red);
            inputField.text = string.Empty;
            Invoke("NextWord", delay);
        }
    }

    private void SetIndicatorColor(Color color)
    {
        indicator.color = color;
    }
    private void NextWord()
    {
        word = wordPicker.Pick();
        SetIndicatorColor(indicatorColor);
    }
}
