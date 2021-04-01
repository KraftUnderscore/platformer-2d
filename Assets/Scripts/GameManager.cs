using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance;

    [SerializeField] private int maxHealth;

    [SerializeField] private GameObject UI;
    [SerializeField] private TextMeshProUGUI scoreValue;

    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI soundVolume;

    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject winMenu;

    [SerializeField] private Transform heartsContainer;
    [SerializeField] private Sprite emptyHeart;
    private Sprite fullHeart;

    private Image[] hearts;
    private int score;
    private bool isPaused;

    private int currentLevel;
    private int currentHealth;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            DestroyImmediate(gameObject);

        isPaused = false;
        hearts = new Image[heartsContainer.childCount];
        for (int i = 0; i < hearts.Length; i++)
            hearts[i] = heartsContainer.GetChild(i).GetComponent<Image>();

        fullHeart = hearts[0].sprite;
        slider.minValue = 0f;
        slider.maxValue = 1f;
        slider.value = 1f;
    }
    private void Start()
    {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(UI.gameObject);

        currentLevel = 0;
        currentHealth = maxHealth;
        gameOverMenu.SetActive(false);
        menu.SetActive(false);
        winMenu.SetActive(false);
        scoreValue.text = score.ToString();
        soundVolume.text = "100%";

        SceneManager.LoadScene(1);
    }

    public void IncreaseScore()
    {
        score++;
        scoreValue.text = score.ToString();
    }

    public bool UpdateHealth()
    {
        hearts[--currentHealth].sprite = emptyHeart;
        return currentHealth != 0;
    }

    public void EndGame()
    {
        gameOverMenu.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void UpdateSlider()
    {
        AudioListener.volume = slider.value;
        soundVolume.text = ((int)(slider.value * 100f)).ToString() + "%";
    }

    public void MuteToggle()
    {
        AudioListener.pause = !AudioListener.pause;
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            if(!isPaused)
            {
                isPaused = true;
                Time.timeScale = 0f;
                menu.SetActive(true);
            }
            else
            {
                isPaused = false;
                Time.timeScale = 1f;
                menu.SetActive(false);
            }
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(1);
        currentHealth = maxHealth;
        menu.SetActive(false);
        gameOverMenu.SetActive(false);
        winMenu.SetActive(false);
        score = 0;
        currentLevel = 0;
        scoreValue.text = score.ToString();
        foreach (Image heart in hearts)
            heart.sprite = fullHeart;

        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1f;
        }
    }

    public void NextLevel()
    {
        if(currentLevel == 1)
        {
            Win();
        }
        else
        {
            currentLevel++;
            SceneManager.LoadScene(2);
        }
    }

    private void Win()
    {
        winMenu.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }
}
