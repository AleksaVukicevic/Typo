using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private WordPicker wordPicker;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private Image indicator;
    private ScoreController scoreController;

    [Header("Other")]
    [SerializeField] private Color indicatorColor;
    [SerializeField] private float delay;

    public string word;

    private void Start()
    {
        scoreController = GetComponent<ScoreController>();
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        inputField.enabled = false;
        countdownText.fontSize = 200;
        countdownText.text = "Get Ready";
        yield return new WaitForSeconds(2);
        countdownText.fontSize = 400;
        countdownText.text = "3";
        yield return new WaitForSeconds(1);
        countdownText.text = "2";
        yield return new WaitForSeconds(1);
        countdownText.text = "1";
        yield return new WaitForSeconds(1);
        countdownText.text = "GO";
        NextWord();
        scoreController.countTime = true;
        inputField.enabled = true;
        yield return new WaitForSeconds(1);
        countdownText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (scoreController.countTime)
        {
            inputFocuser();
            skipDetector();

            if (inputField.text.ToLower() == word.ToLower())
            {
                scoreController.AddScore(word.Length);

                StartCoroutine(SetIndicatorColor(Color.green, 0f));
                inputField.text = string.Empty;
                NextWord();
            }
        }
    }
    public void End()
    {
        countdownText.gameObject.SetActive(true);
        countdownText.fontSize = 200;
        countdownText.text = "Time out";
        inputField.enabled = false;
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
