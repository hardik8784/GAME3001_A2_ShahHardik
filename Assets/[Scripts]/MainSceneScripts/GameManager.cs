
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
            secondTimer--;
        }
    }

    public void Score()
    {
        score++;
        print("Score : " + score);
       
    }
    public void FailToClick()
    {
        score = 0;
    }
 
}
