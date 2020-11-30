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
    [SerializeField] private GameObject endScreen;
    private ScoreController scoreController;
    [SerializeField] private SceneController sceneController;

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
        yield return new WaitForSeconds(1);
        SoundsScript.ss.PlaySound("beep");
        yield return new WaitForSeconds(1);
        SoundsScript.ss.PlaySound("beep");
        countdownText.fontSize = 400;
        countdownText.text = "3";
        yield return new WaitForSeconds(1);
        SoundsScript.ss.PlaySound("beep");
        countdownText.text = "2";
        yield return new WaitForSeconds(1);
        SoundsScript.ss.PlaySound("beep");
        countdownText.text = "1";
        yield return new WaitForSeconds(1);
        SoundsScript.ss.PlaySound("start");
        countdownText.text = "GO";
        NextWord();
        scoreController.countTime = true;
        inputField.enabled = true;
        yield return new WaitForSeconds(1);
        countdownText.gameObject.SetActive(false);
    }
    public IEnumerator End()
    {
        countdownText.gameObject.SetActive(true);
        countdownText.fontSize = 200;
        countdownText.text = "Time out";
        inputField.enabled = false;
        yield return new WaitForSeconds(2.5f);
        endScreen.SetActive(true);
        yield return new WaitForSeconds(1);
        countdownText.gameObject.SetActive(false);
    }

    public void ContinueButton()
    {
        sceneController.LoadScene("Menu");
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
                SoundsScript.ss.PlaySound("correct");
                inputField.text = string.Empty;
                NextWord();
            }
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
            SoundsScript.ss.PlaySound("error");
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
