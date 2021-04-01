using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance;

    private int score;

    private void Awake()
    {
        instance = this;
    }

    public void IncreaseScore()
    {
        score++;
        Debug.Log(score);
    }

    public void UpdateHealth(int health)
    {

    }

    public void EndGame()
    {

    }
}
