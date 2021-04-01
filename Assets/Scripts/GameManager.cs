using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance;
    [SerializeField] private TextMeshProUGUI scoreValue;

    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI soundVolume;

    [SerializeField] private GameObject menu;

    [SerializeField] private Transform heartsContainer;
    [SerializeField] private Sprite emptyHeart;
    private Sprite fullHeart;

    private Image[] hearts;
    private int score;
    private bool isPaused;

    private void Awake()
    {
        instance = this;
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
}
