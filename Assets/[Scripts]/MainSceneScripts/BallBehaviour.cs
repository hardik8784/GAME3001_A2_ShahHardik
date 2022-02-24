using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public float speed = 10.0f;
    public float circleRadius = 1;
    bool clockwise = true;
    float angle = 270;
    public GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
        circleRadius = Mathf.Abs(ball.transform.localPosition.y);
        angle = angle * Mathf.PI / 180;
    }

    // Update is called once per frame
    void Update()
    {
        angle += clockwise ? -speed * Time.deltaTime : speed * Time.deltaTime;
        ball.transform.localPosition = new Vector3(circleRadius * Mathf.Cos(angle), circleRadius * Mathf.Sin(angle), 0);
    }
}
