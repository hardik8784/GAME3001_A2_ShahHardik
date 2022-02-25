
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public int score = 0;
    public int[] timers;
    public int[] goals;
    public float[] validDistance;
    public float[] validSpawn;
    public int level = 0;
    private float secondTimer = 0;


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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        secondTimer += Time.deltaTime;
        if (secondTimer > 1)
        {
            timers[level]--;
            UIManager.Instance.UpdateTimer(timers[level]);
            secondTimer--;
        }
        Setup();

      
    }

    public void Score()
    {
        score++;
        //print("Score : " + score);
        UIManager.Instance.UpdateScore(score);
        if (score == goals[level] && !(score == goals[2]))
        {
            if(goals[level] == goals[2])
            {
                Time.timeScale = 0;
            }
            //Change to Next Level
            score = 0;
            UIManager.Instance.UpdateScore(score);
            level++;
            UIManager.Instance.UpdateLevel(level);
            UIManager.Instance.UpdateTimer(timers[level]);
            UIManager.Instance.UpdateGoal(goals[level]);
            secondTimer--;       
        }
    }

    public void FailToClick()
    {
        score = 0;
        UIManager.Instance.UpdateScore(score);
    }
    public void Setup()
    {
        UIManager.Instance.UpdateGoal(goals[level]);
        UIManager.Instance.UpdateLevel(level + 1);
    }
}
