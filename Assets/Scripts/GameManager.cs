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

    [SerializeField] private GameObject UI;
    [SerializeField] private TextMeshProUGUI scoreValue;

    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI soundVolume;

    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject gameOverMenu;

    [SerializeField] private Transform heartsContainer;
    [SerializeField] private Sprite emptyHeart;
    private Sprite fullHeart;

    private Image[] hearts;
    private int score;
    private bool isPaused;

    private int currentLevel;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(PlayerController.instance);
        DontDestroyOnLoad(BulletPool.instance);
        DontDestroyOnLoad(UI);

        instance = this;
        isPaused = false;
        hearts = new Image[heartsContainer.childCount];
        for (int i = 0; i < hearts.Length; i++)
            hearts[i] = heartsContainer.GetChild(i).GetComponent<Image>();

        fullHeart = hearts[0].sprite;
        slider.minValue = 0f;
        slider.maxValue = 1f;
        slider.value = 1f;
        SceneManager.LoadScene(1);
    }
    private void Start()
    {
        currentLevel = 0;
        gameOverMenu.SetActive(false);
        menu.SetActive(false);
        scoreValue.text = score.ToString();
        soundVolume.text = "100%";
    }

    public void IncreaseScore()
    {
        score++;
        scoreValue.text = score.ToString();
    }

    public void UpdateHealth(int health)
    {
        hearts[health].sprite = emptyHeart;
    }

    public void EndGame()
    {
        gameOverMenu.SetActive(true);
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
        PlayerController.instance.ResetPlayer();
        menu.SetActive(false);
        gameOverMenu.SetActive(false);
        score = 0;
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
            PlayerController.instance.transform.position = Vector2.zero;
        }
    }

    private void Win()
    {

    }
}
