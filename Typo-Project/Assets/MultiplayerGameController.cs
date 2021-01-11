using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerGameController : MonoBehaviour
{
    public int health = 100;
    [SerializeField] private Slider healthSlider;
    public int enemyHealth = 100;
    [SerializeField] private Slider enemyHealthSlider;
    [SerializeField] private TextMeshProUGUI winLoseText;
    [Header("Refs")]
    [SerializeField] private WordPicker wordPicker;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private Image indicator;
    [SerializeField] private SceneController sceneController;
    [SerializeField] private BannerAdScript bannerAdScript;

    private PhotonNetworkingScript photonNetworking;
    [SerializeField] private GameObject endScreen;

    [Header("Other")]
    [SerializeField] private Color indicatorColor;
    [SerializeField] private float delay;
    [SerializeField] private bool gameOn;
    public bool gameStarted;

    public string word;

    private void Start()
    {
        photonNetworking = GetComponent<PhotonNetworkingScript>();

        bannerAdScript.gameObject.SetActive(true);
        inputField.enabled = false;
    }
    public void StartTheGame()
    {
        bannerAdScript.CloseAd();
        photonNetworking.roomMenu.SetActive(false);
        endScreen.SetActive(false);
        gameStarted = true;
        health = 100;
        enemyHealth = 100;
        healthSlider.value = 100;
        enemyHealthSlider.value = 100;
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        
        countdownText.gameObject.SetActive(true);
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
        gameOn = true;
        inputField.enabled = true;
        yield return new WaitForSeconds(1);
        countdownText.gameObject.SetActive(false);
    }
    public IEnumerator End()
    {
        gameStarted = false;
        gameOn = false;
        SoundsScript.ss.PlaySound("endBeeps");
        countdownText.gameObject.SetActive(true);
        countdownText.fontSize = 200;
        countdownText.text = "KO";
        inputField.enabled = false;
        yield return new WaitForSeconds(2.5f);
        endScreen.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(bannerAdScript.ShowBannerWhenInitializedEvenIfClosed());
        yield return new WaitForSeconds(0.5f);
        countdownText.gameObject.SetActive(false);
    }

    public void ContinueButton()
    {
        sceneController.LoadScene("Menu");
    }

    private void Update()
    {
        if (gameOn)
        {
            inputFocuser();
            skipDetector();

            if (inputField.text.ToLower() == word.ToLower())
            {
                //Send damage
                photonNetworking.SendDataDamage(word.Length);

                StartCoroutine(SetIndicatorColor(Color.green, 0f));
                SoundsScript.ss.PlaySound("correct");
                inputField.text = string.Empty;
                NextWord();
            }
        }
    }
    public void Damage(int value)
    {
        health -= value;
        healthSlider.value = health;
        if (health <= 0)
        {
            Dead();
        }
    }
    public void DamageEnemy(int value)
    {
        enemyHealth -= value;
        enemyHealthSlider.value = enemyHealth;
        if (enemyHealth <= 0)
        {
            Win();
        }
    }
    public void Win()
    {
        winLoseText.text = "You WIN";
        winLoseText.color = new Color(1, 0.65f, 0);

        StopAllCoroutines();
        gameOn = false;
        inputField.enabled = false;
        countdownText.gameObject.SetActive(false);

        StartCoroutine(End());
    }
    public void Dead()
    {
        winLoseText.text = "You LOSE";
        winLoseText.color = Color.red;
        StartCoroutine(End());
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

    public void ResetGameTexts()
    {
        inputField.text = "";
        wordPicker.wordText.text = "?";
    }
}
