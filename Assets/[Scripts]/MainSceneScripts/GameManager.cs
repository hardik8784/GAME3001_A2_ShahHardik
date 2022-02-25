
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
    public GameObject CircleLock;


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
        Time.timeScale = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.UpdateUserInterfaceText("Hello, Welcome to the Lockpicking MiniGame");
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
        if (timers[level] == 0)
        {
            Destroy(CircleLock);
          
            UIManager.Instance.UpdateUserInterfaceText("You Lose");
            Debug.Log("Game Over");
            Time.timeScale = 0;
           
        }
    }

    public void Score()
    {
        score++;
        //print("Score : " + score);
        UIManager.Instance.UpdateScore(score);
        if (score == goals[level] && !(score ==goals[2]))
        {
            //Change to Next Level
                score = 0;
                level++;
                UIManager.Instance.UpdateScore(score);
                UIManager.Instance.UpdateLevel(level);
                UIManager.Instance.UpdateTimer(timers[level]);
                UIManager.Instance.UpdateGoal(goals[level]);
                secondTimer--;
            UIManager.Instance.UpdateUserInterfaceText("You Succesfuly completed Level " + level);
        }
        else if(level == 2)
        {
            if (score == goals[level])
            {
                UIManager.Instance.UpdateUserInterfaceText("You Won");
                Debug.Log("Win");
                Time.timeScale = 0;
            }
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
