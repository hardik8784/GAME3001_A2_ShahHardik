using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }
    public TMP_Text ScoreText;
    public TMP_Text TargetText;
    public TMP_Text TimerText;
    public TMP_Text LevelText;
    public TMP_Text UserInterfaceText;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    public void UpdateScore(int score)
    {
        ScoreText.text = "Score: " + score;
    }
    public void UpdateGoal(int goal)
    {
        TargetText.text = "Goal: " + goal;
    }
    public void UpdateTimer(int timer)
    {
        TimerText.text = "Timer: " + timer;
    }
    public void UpdateLevel(int level)
    {
        LevelText.text = "Level " + level;
    }

    public void UpdateUserInterfaceText(string condition)
    {
        UserInterfaceText.text =  condition;
    }
}
